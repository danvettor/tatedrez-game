using System.Collections.Generic;

public class Player
{
    PlayerColor _color;
    List<PieceType> _pieces;

    public Player(PlayerColor color)
    {
        _color = color;
        _pieces = new List<PieceType>() { PieceType.KNIGHT, PieceType.BISHOP, PieceType.ROOK };
    }

    public PieceType GetNextPiece()
    {
        var lastPieceIndex = _pieces.Count - 1;
        var pieceType = _pieces[lastPieceIndex];
        _pieces.RemoveAt(lastPieceIndex);
        return pieceType;
    }
}