using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightPiece: Piece
{
    public KnightPiece(PieceType type, PlayerColor color, Vector2Int pos)
    {
        Type = type;
        Color = color;
        Pos = pos;
    }
    public override bool IsValidMove(Vector2Int to)
    {
        var xDist = Math.Abs(to.x - Pos.x);
        var yDist = Math.Abs(to.y - Pos.y);

        return  (xDist == 2 && yDist == 1) || (xDist == 1 && yDist == 2);
    }

   
}
