using System;
using System.Collections.Generic;
using UnityEngine;

public class Board
{
    private int[,] PiecesOnBoard;
    private int[,] ColorOnBoard;

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
        if (IsEmpty(pos.x, pos.y))
        {
            PiecesOnBoard[pos.x, pos.y] = (int)type;
            ColorOnBoard[pos.x, pos.y] = (int)color;

            OnBoardUpdate?.Invoke(this);
        }
    }

    public IPiece GetPiece(int x, int y)
    {
        var type = (PieceType) PiecesOnBoard[x, y];
        var color = (PlayerColor) ColorOnBoard[x, y];
        return PieceFactory.CreatePiece(type, color, new Vector2Int(x,y));
    }
    public bool IsEmpty(int x, int y)
    {
        return PiecesOnBoard[x, y] == -1 && ColorOnBoard[x, y] == -1;
    }
}
