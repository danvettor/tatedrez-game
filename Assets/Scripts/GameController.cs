using System;
using System.Collections.Generic;
using UnityEngine;
public class GameController
{
    private PlayerColor _colorTurn;
    private GameState _currentState;
    private Board _board;
    private BoardView _boardView;

    Player _blackPlayer;
    Player _whitePlayer;

    IPiece _currentPiece;

 
    public GameController(Board board, BoardView boardView, List<PieceTemplate> availablePieces)
    {
        _board = board;
        _boardView = boardView;
        _whitePlayer = new Player(PlayerColor.WHITE);
        _blackPlayer = new Player(PlayerColor.BLACK);
    }

    public void StartNewGame()
    {
        _colorTurn = PlayerColor.BLACK;
        _currentState = GameState.PLACE_PIECES;
        _boardView.CreateInitialBoard(_board, this);
    }

    public void OnSelectedPiece(PieceType typeSelected, PlayerColor color)
    {
        _currentPiece = PieceFactory.CreatePiece(typeSelected,color, Vector2Int.one*-1);
    }
    public void Play(Vector2Int pos)
    {
        //TODO: temporario antes de selecionar peça; obviamente vai dar erro quando acabarem as peças.

        if (_currentState == GameState.PLACE_PIECES)
        {
            if (!_board.IsEmpty(pos.x, pos.y))
                return;
            if (_currentPiece == null)
                return;
            _board.PlacePiece(pos, _currentPiece.Type, _colorTurn);
            if (!_whitePlayer.HasPiecesToPlace && !_blackPlayer.HasPiecesToPlace)
            {
                _currentState = GameState.DYNAMIC;
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
                if(IsValidMove(from: _currentPiece.Pos, to: pos))
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
            //endgame
        }
        _colorTurn = _colorTurn == PlayerColor.BLACK ? PlayerColor.WHITE : PlayerColor.BLACK;
        Debug.Log("TURN: " + _colorTurn);
    }


}