using System;
using System.Collections.Generic;
using UnityEngine;

public class Board
{
    public int[,] PiecesOnBoard { get; private set; }
    public int[,] ColorOnBoard { get; private set; }

    public int Size { get; private set; }

    public Action<Board> OnBoardUpdate;
    public Board()
    {
        PiecesOnBoard = new int[3, 3] { { -1, -1, -1 }, { -1, -1, -1 }, { -1, -1, -1 } };
        ColorOnBoard = new int[3, 3] { { -1, -1, -1 }, { -1, -1, -1 }, { -1, -1, -1 } };
        Size = 3;
    }

    public void PlacePiece(Vector2Int pos, PieceType type, PlayerColor color)
    {
        //from - to
        PiecesOnBoard[pos.x, pos.y] = (int)type;
        ColorOnBoard[pos.x, pos.y] = (int)color;

        OnBoardUpdate?.Invoke(this);
    }
    public List<Vector2> PossibleMoves(PieceType pieceType, Vector2 pos)
    {
        return null;
    }
    public bool HasWinner()
    {
        return false;
    }
}
