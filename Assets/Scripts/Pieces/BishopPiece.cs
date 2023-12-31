using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BishopPiece : Piece
{
    public BishopPiece(PieceType type, PlayerColor color, Vector2Int pos)
    {
        Type = type;
        Color = color;
        Pos = pos;
    }
    public override bool IsValidMove(Vector2Int posToMove)
    {
        return Math.Abs(posToMove.x - Pos.x) == Math.Abs(posToMove.y - Pos.y);
    }
}
