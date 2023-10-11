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

    public GameController(Board board, BoardView boardView)
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

    public void Play(Vector2Int pos)
    {
        //TODO: temporario antes de selecionar peça; obviamente vai dar erro quando acabarem as peças.
        if (!_board.IsEmpty(pos.x, pos.y))
            return;

        if (_currentState == GameState.PLACE_PIECES)
        {

            var typeToPlay = (_colorTurn == PlayerColor.BLACK ? _blackPlayer : _whitePlayer).GetNextPiece();
            _board.PlacePiece(pos, typeToPlay, _colorTurn);
            if (!_whitePlayer.HasPiecesToPlace && !_blackPlayer.HasPiecesToPlace)
            {
                _currentState = GameState.DYNAMIC;
            }
        }
        else if (_currentState == GameState.DYNAMIC)
        {
            Debug.Log("STATE CHANGE!!");
        }
        _colorTurn = _colorTurn == PlayerColor.BLACK ? PlayerColor.WHITE : PlayerColor.BLACK;
    }

}