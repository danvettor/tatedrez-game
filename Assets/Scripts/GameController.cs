using System.Collections.Generic;
using UnityEngine;
public class GameController
{
    private PlayerColor _colorTurn;

    private Board _board;
    private BoardView _boardView;

    Player _blackPlayer;
    Player _whitePlayer;
    Dictionary<PlayerColor, Player> _players;

    public GameController(Board board, BoardView boardView)
    {
        _board = board;
        _boardView = boardView;
        _whitePlayer = new Player(PlayerColor.WHITE);
        _blackPlayer = new Player(PlayerColor.BLACK);

        _players = new Dictionary<PlayerColor, Player>()
        {
            { PlayerColor.BLACK, _blackPlayer },
            { PlayerColor.WHITE, _whitePlayer },
        };

        _colorTurn = PlayerColor.BLACK;
    }

    public void StartNewGame()
    {
        _boardView.CreateInitialBoard(_board, this);
    }

    public void Play(Vector2Int pos)
    {
        Debug.Log($"PlayPos X:{pos.x} Y:{pos.y}");
        //TODO: temporario antes de selecionar peça; obviamente vai dar erro quando acabarem as peças.
        var pieceToPlay = _players[_colorTurn].GetNextPiece();
       
        Play(pos, pieceToPlay, _colorTurn);
        _colorTurn = _colorTurn == PlayerColor.BLACK ? PlayerColor.WHITE : PlayerColor.BLACK;
    }
    private void Play(Vector2Int pos, PieceType type, PlayerColor color)
    {
        _board.PlacePiece(pos, type, color);
    }



}