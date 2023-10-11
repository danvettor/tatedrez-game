using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BishopPiece : IPiece
{
    public BishopPiece(PieceType type, PlayerColor color, Vector2Int pos)
    {
        Type = type;
        Color = color;
        Pos = pos;
    }

    public PieceType Type { get; set; }
    public PlayerColor Color { get; set; }
    public Vector2Int Pos { get; set; }
    public  bool IsValidMove(Vector2Int posToMove)
    {
        return true;
    }
}
