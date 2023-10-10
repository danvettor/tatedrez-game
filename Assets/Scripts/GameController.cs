using UnityEngine;
public class GameController
{
    private PlayerColor colorTurn;

    private Board _board;
    private BoardView _boardView;


    public GameController(Board board, BoardView boardView)
    {
        _board = board;
        _boardView = boardView;
    }

    public void StartNewGame()
    {
        _boardView.CreateInitialBoard(_board, this);
    }

    public void Play(Vector2Int pos)
    {
        Debug.Log($"PlayPos X:{pos.x} Y:{pos.y}");
            
        Play(pos, PieceType.KNIGHT, PlayerColor.BLACK);
    }
    private void Play(Vector2Int pos, PieceType type, PlayerColor color)
    {
        _board.PlacePiece(pos, type, color);
    }



}