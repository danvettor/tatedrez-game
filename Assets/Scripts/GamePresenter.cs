using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePresenter : MonoBehaviour
{
    private Board _board;
    [SerializeField] BoardView _boardView;
    [SerializeField] PlayerUI _blackUI;
    [SerializeField] PlayerUI _whiteUI;
    [SerializeField] AlertUI _alertUI;

    [SerializeField] GameObject _overlay;

    private PlayerUI _currentActiveUI;

    private Player _blackPlayer;
    private Player _whitePlayer;
    private Player _currentPlayer;

    private int _tieCounter;
    private PlayerColor _colorTurn;
    private GamePhase _currentGamePhase;


    IPiece _selectedPiece;

    private void Awake()
    {
        _board = new Board();

        _blackUI.OnPieceSelected = (type) => OnSelectedPiece(type, _blackUI.PlayerColor);
        _whiteUI.OnPieceSelected = (type) => OnSelectedPiece(type, _whiteUI.PlayerColor);
        _blackPlayer = new Player(PlayerColor.BLACK);
        _whitePlayer = new Player(PlayerColor.WHITE);

        StartNewGame();
    }
    private void StartNewGame()
    {
        _colorTurn = PlayerColor.WHITE;
        _currentPlayer = _whitePlayer;
        _currentGamePhase = GamePhase.PLACE_PIECES;
        _currentActiveUI = _whiteUI;
        _boardView.CreateInitialBoard(_board, onTileClicked: Play);
        _alertUI.OnTurn(_colorTurn);
    }

    public void OnSelectedPiece(PieceType typeSelected, PlayerColor color)
    {
        if (color == _colorTurn)
        {
            _selectedPiece = PieceFactory.CreatePiece(typeSelected, color, Vector2Int.one * -1);
            _currentActiveUI.HighlightUI(_selectedPiece.Type);
        }
        else
        {
            ResetPieceSelection();
        }
    }
    public void Play(Vector2Int pos)
    {
        if (_currentGamePhase == GamePhase.PLACE_PIECES)
            PlacePiecePlay(pos);
        else if (_currentGamePhase == GamePhase.DYNAMIC)
            DynamicPlay(pos);
    }

    private void PlacePiecePlay(Vector2Int pos)
    {
        var isPiecePlaced = _board.PlacePiece(pos, _selectedPiece.Type, _colorTurn);

        if (isPiecePlaced)
        {
            //WIP: refactoring to change UI to a controller/presenter player class
            _currentActiveUI.OnPiecePlaced(_selectedPiece.Type);
            _currentPlayer.OnPiecePlaced(_selectedPiece.Type);
            if (!_blackUI.HasPieceToPlace && !_whiteUI.HasPieceToPlace)
            {
                _currentGamePhase = GamePhase.DYNAMIC;
                ResetPieceSelection();
            }
            ResetPieceSelection();
            Turn();
        }

    }

    private void DynamicPlay(Vector2Int pos)
    {
        //TODO: Check if there any move for the currentPlayer
        if (!_board.HasValidMove(_colorTurn))
        {
            _tieCounter++;
            Turn();
            return;
        }
        _tieCounter = 0;

        if (_selectedPiece.Type == PieceType.NONE)
        {
            _selectedPiece = _board.GetPiece(pos.x, pos.y);
            if (_selectedPiece.Type == PieceType.NONE)
                return;

            if (_selectedPiece.Color == _colorTurn)
            {
                var validMoves = _board.ValidMoves(_selectedPiece);
                _boardView.HighlightPossibleMoves(validMoves);
                _boardView.HighlightTile(_selectedPiece.Pos);
            }
            else
                ResetPieceSelection();

        }
        else
        {
            if (IsValidMove(from: _selectedPiece.Pos, to: pos))
            {
                //check for winner
                _board.MovePiece(_selectedPiece, pos);
                Debug.Log($"VALID MOVE FROM {_selectedPiece.Color} {_selectedPiece.Type}!!");
                ResetPieceSelection();
                Turn();
            }
            else
            {
                ResetPieceSelection();
            }
        }
    }


    private void ResetPieceSelection()
    {
        _currentActiveUI.DeselectUI();
        _boardView.DeselectMoveHighlights();
        _boardView.DeselectTile();
        _selectedPiece = PieceFactory.CreatePiece(PieceType.NONE,PlayerColor.NONE,Vector2Int.one*-1);
    }
    private bool IsValidMove(Vector2Int from, Vector2Int to)
    {
        return _board.IsEmpty(to.x, to.y) &&
                !_board.HasPieceInBetween(from, to) &&
                _selectedPiece.IsValidMove(to);
    }

    private void Turn()
    {
        if (_tieCounter == 2)
        {
            _alertUI.OnTiedGame();
            _overlay.SetActive(true);

            Invoke("OnGameEnded", 3.0f);
        }
        var (hasWinner, winnerColor) = _board.HasWinner();

        if (hasWinner)
        {
            Debug.Log($"Winner is {winnerColor}");
            _board.ClearBoardForWinner( winnerColor);
            _alertUI.OnEndGame(winnerColor);
            _overlay.SetActive(true);

            Invoke("OnGameEnded",3.0f);
        }
        else
        {
            _colorTurn = _colorTurn == PlayerColor.BLACK ? PlayerColor.WHITE : PlayerColor.BLACK;
            _currentActiveUI = _colorTurn == PlayerColor.BLACK ? _blackUI : _whiteUI;
            _currentPlayer = _colorTurn == PlayerColor.BLACK ? _blackPlayer : _whitePlayer;

            _alertUI.OnTurn(_colorTurn);
            Debug.Log("TURN: " + _colorTurn);
        }
       
    }

    private void OnGameEnded()
    {
        SceneManager.LoadScene("Menu");
    }

}
