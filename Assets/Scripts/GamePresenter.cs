using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePresenter : MonoBehaviour
{
    private Board _board;
    [SerializeField] BoardView _boardView;
    [SerializeField] PlayerUI _blackUI;
    [SerializeField] PlayerUI _whiteUI;

    [SerializeField] TMP_Text _turnHUD;

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
        _turnHUD.text = "TURN: " + _colorTurn;
        _currentState = GameState.PLACE_PIECES;
        _boardView.CreateInitialBoard(_board, onTileClicked: Play);
    }

    public void OnSelectedPiece(PieceType typeSelected, PlayerColor color)
    {
        if (color == _colorTurn)
            _currentPiece = PieceFactory.CreatePiece(typeSelected, color, Vector2Int.one * -1);
        else
            ResetPieceSelection();
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
            (_colorTurn == PlayerColor.BLACK ? _blackUI : _whiteUI).OnPiecePlaced(_currentPiece.Type);
            if (!_blackUI.HasPieceToPlace && !_whiteUI.HasPieceToPlace)
            {
                _currentState = GameState.DYNAMIC;
                ResetPieceSelection();
            }
            Turn();
        }
        else if (_currentState == GameState.DYNAMIC)
        {
            //TODO: Check if there any move for the currentPlayer
            if (_currentPiece == null)
            {
                _currentPiece = _board.GetPiece(pos.x, pos.y);
                if (_currentPiece == null)
                    return;

                if (_currentPiece.Color == _colorTurn)
                    _boardView.HighlightTile(_currentPiece.Pos);
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
        var (hasWinner, winnerColor) = _board.HasWinner();

        if (hasWinner)
        {
            Debug.Log($"Winner is {winnerColor}");
            _board.ClearBoardForWinner( winnerColor);
            //endgame
        }
        _colorTurn = _colorTurn == PlayerColor.BLACK ? PlayerColor.WHITE : PlayerColor.BLACK;
        _turnHUD.text = _turnHUD.text + _colorTurn;

        _turnHUD.text = "TURN: " + _colorTurn;

        Debug.Log("TURN: " + _colorTurn);
    }

}
