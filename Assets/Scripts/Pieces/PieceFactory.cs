using System;
using UnityEngine;

public class PieceFactory
{
    public static IPiece CreatePiece(PieceType type, PlayerColor color, Vector2Int pos)
    {
        switch (type)
        {
            case PieceType.BISHOP:
                return new BishopPiece(color, pos);
            case PieceType.ROOK:
                return new RookPiece( color, pos);
            case PieceType.KNIGHT:
                return new KnightPiece(color, pos);
            default:
                return new NonePiece(pos);
        }
    }
}
