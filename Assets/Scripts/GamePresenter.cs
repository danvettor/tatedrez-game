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

    private int _tieCounter;

    //gamecontroller
    private PlayerColor _colorTurn;
    private GameState _currentState;


    IPiece _currentPiece;

    private void Awake()
    {
        _board = new Board();

        _blackUI.OnPieceSelected = (type) => OnSelectedPiece(type, _blackUI.PlayerColor);
        _whiteUI.OnPieceSelected = (type) => OnSelectedPiece(type, _whiteUI.PlayerColor);

        StartNewGame();
    }
    private void StartNewGame()
    {
        _colorTurn = PlayerColor.WHITE;
        _currentState = GameState.PLACE_PIECES;
        _currentActiveUI = _whiteUI;
        _boardView.CreateInitialBoard(_board, onTileClicked: Play);
        _alertUI.OnTurn(_colorTurn);
    }

    public void OnSelectedPiece(PieceType typeSelected, PlayerColor color)
    {
        if (color == _colorTurn)
        {
            _currentPiece = PieceFactory.CreatePiece(typeSelected, color, Vector2Int.one * -1);
            _currentActiveUI.HighlightUI(_currentPiece.Type);
        }
        else
        {
            ResetPieceSelection();
        }
    }
    public void Play(Vector2Int pos)
    {
        if (_currentState == GameState.PLACE_PIECES)
        {
            if (!_board.IsEmpty(pos.x, pos.y))
                return;
            if (_currentPiece == null)
                return;
            _board.PlacePiece(pos, _currentPiece.Type, _colorTurn);
            _currentActiveUI.OnPiecePlaced(_currentPiece.Type);
            if (!_blackUI.HasPieceToPlace && !_whiteUI.HasPieceToPlace)
            {
                _currentState = GameState.DYNAMIC;
                ResetPieceSelection();
            }
            ResetPieceSelection();
            Turn();
        }
        else if (_currentState == GameState.DYNAMIC)
        {
            //TODO: Check if there any move for the currentPlayer
            if (!_board.HasValidMove(_colorTurn))
            {
                _tieCounter++;
                Turn();
                return;
            }
            _tieCounter = 0;

            if (_currentPiece == null)
            {
                _currentPiece = _board.GetPiece(pos.x, pos.y);
                if (_currentPiece == null)
                    return;

                if (_currentPiece.Color == _colorTurn)
                {
                    var validMoves = _board.ValidMoves(_currentPiece);
                    _boardView.HighlightPossibleMoves(validMoves);
                    _boardView.HighlightTile(_currentPiece.Pos);
                }
                else
                    ResetPieceSelection();

            }
            else
            {
                if (IsValidMove(from: _currentPiece.Pos, to: pos))
                {
                    //check for winner
                    _board.MovePiece(_currentPiece, pos);
                    Debug.Log($"VALID MOVE FROM {_currentPiece.Color} {_currentPiece.Type}!!");
                    ResetPieceSelection();
                    Turn();
                }
                else
                {
                    ResetPieceSelection();
                }
            }

        }
    }

    private void ResetPieceSelection()
    {
        _currentActiveUI.DeselectUI();
        _boardView.DeselectMoveHighlights();
        _boardView.DeselectTile();
        _currentPiece = null;
    }
    private bool IsValidMove(Vector2Int from, Vector2Int to)
    {
        return _board.IsEmpty(to.x, to.y) &&
                !_board.HasPieceInBetween(from, to) &&
                _currentPiece.IsValidMove(to);
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
            _alertUI.OnTurn(_colorTurn);
            Debug.Log("TURN: " + _colorTurn);
        }
       
    }

    private void OnGameEnded()
    {
        SceneManager.LoadScene("Menu");
    }

}
