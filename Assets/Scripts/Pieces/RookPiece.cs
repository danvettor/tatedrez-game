using System;
using System.Collections;
using UnityEngine;

public partial class RookPiece : Piece
{
    public RookPiece(PlayerColor color, Vector2Int pos)
    {
        Type = PieceType.ROOK;
        Color = color;
        Pos = pos;
    }

    public override bool IsValidMove(Vector2Int to)
    {
        var xDist = Math.Abs(to.x - Pos.x);
        var yDist = Math.Abs(to.y - Pos.y);
        return (xDist == 0 && yDist > 0) || (xDist > 0 && yDist == 0);
    }
   


}
