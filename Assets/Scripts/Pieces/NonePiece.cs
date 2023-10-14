using UnityEngine;

public class NonePiece : Piece
{
    public NonePiece(Vector2Int pos)
    {
        Type = PieceType.NONE;
        Color = PlayerColor.NONE;
        Pos = pos;
    }

    public override bool IsValidMove(Vector2Int posToMove)
    {
        return false;
    }
}