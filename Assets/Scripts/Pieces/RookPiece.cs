using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RookPiece : IPiece
{
    public RookPiece(PieceType type, PlayerColor color, Vector2Int pos)
    {
        Type = type;
        Color = color;
        Pos = pos;
    }

    public PieceType Type { get; set; }
    public PlayerColor Color { get; set; }
    public Vector2Int Pos { get; set; }

    public bool IsValidMove(Vector2Int to)
    {
        var xDist = Math.Abs(to.x - Pos.x);
        var yDist = Math.Abs(to.y - Pos.y);
        return (xDist == 0 && yDist > 0) || (xDist > 0 && yDist == 0);
    }


}
