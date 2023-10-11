using System;
using UnityEngine;

public class PieceFactory
{
    public static IPiece CreatePiece(PieceType type, PlayerColor color, Vector2Int pos)
    {
        switch (type)
        {
            case PieceType.BISHOP:
                return new BishopPiece(type, color, pos);
            case PieceType.ROOK:
                return new RookPiece(type, color, pos);
            case PieceType.KNIGHT:
                return new KnightPiece(type, color, pos);
            default:
                return null;
        }
    }
}
