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

    public void MovePiece(IPiece piece, Vector2Int to)
    {
        PiecesOnBoard[piece.Pos.x, piece.Pos.y] = -1;
        PiecesOnBoard[to.x, to.y] = (int) piece.Type;
        ColorOnBoard[piece.Pos.x, piece.Pos.y] = -1;
        ColorOnBoard[to.x, to.y] = (int)piece.Color;

        OnBoardUpdate?.Invoke(this);
    }

    public bool HasPieceInBetween(Vector2Int from, Vector2Int to)
    {
        // check for piece next do another (nothing in between obviously)
        if (Mathf.FloorToInt((to - from).magnitude) == 1) 
            return false;

        var xDist = Math.Abs(to.x - from.x);
        var yDist = Math.Abs(to.y - from.y);

        if (xDist == 2 && yDist == 2) //check for central piece
            return PiecesOnBoard[1, 1] != -1;
        if (to.y == from.y) // checking row to allow vertical move
            return !(PiecesOnBoard[1, 0] == -1 || PiecesOnBoard[1, 1] == -1 || PiecesOnBoard[1, 2] == -1);
        if (to.x == from.x) // checking column to allow horizontal move
            return !(PiecesOnBoard[0, 1] == -1 || PiecesOnBoard[1, 1] == -1 || PiecesOnBoard[2, 1] == -1);
       
        return false;
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
