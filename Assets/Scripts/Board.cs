using System;
using System.Collections.Generic;
using UnityEngine;

public class Board
{
    private int[,] _piecesOnBoard;
    private int[,] _colorsOnBoard;

    public int Size { get; private set; }
    public Action<Board> OnBoardUpdate;
    public Board()
    {
        _piecesOnBoard = new int[3, 3] { { -1, -1, -1 }, { -1, -1, -1 }, { -1, -1, -1 } };
        _colorsOnBoard = new int[3, 3] { { -1, -1, -1 }, { -1, -1, -1 }, { -1, -1, -1 } };
        Size = 3;
    }

    public void PlacePiece(Vector2Int pos, PieceType type, PlayerColor color)
    {
        if (IsEmpty(pos.x, pos.y))
        {
            _piecesOnBoard[pos.x, pos.y] = (int)type;
            _colorsOnBoard[pos.x, pos.y] = (int)color;

            OnBoardUpdate?.Invoke(this);
        }
    }

    public void MovePiece(IPiece piece, Vector2Int to)
    {
        _piecesOnBoard[piece.Pos.x, piece.Pos.y] = -1;
        _piecesOnBoard[to.x, to.y] = (int)piece.Type;
        _colorsOnBoard[piece.Pos.x, piece.Pos.y] = -1;
        _colorsOnBoard[to.x, to.y] = (int)piece.Color;

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
            return _piecesOnBoard[1, 1] != -1;
        if (to.y == from.y) // checking row to allow vertical move
            return !(_piecesOnBoard[1, to.y] == -1);
        if (to.x == from.x) // checking column to allow horizontal move
            return !(_piecesOnBoard[to.x, 1] == -1);

        return false;
    }
    public IPiece GetPiece(int x, int y)
    {
        var type = (PieceType)_piecesOnBoard[x, y];
        var color = (PlayerColor)_colorsOnBoard[x, y];
        return PieceFactory.CreatePiece(type, color, new Vector2Int(x, y));
    }
    public bool IsEmpty(int x, int y)
    {
        return _piecesOnBoard[x, y] == -1 && _colorsOnBoard[x, y] == -1;
    }

    public (bool, PlayerColor) HasWinner()
    {
        // check rows
        for (int i = 0; i < Size; i++)
        {
            if (_colorsOnBoard[i, 0] == _colorsOnBoard[i, 1] &&
                _colorsOnBoard[i, 1] == _colorsOnBoard[i, 2] &&
                _colorsOnBoard[i, 0] != -1)
            {
                var winnerColor = (PlayerColor)_colorsOnBoard[i, 0];
                return (true, winnerColor);
            }
        }
        //check columns
        for (int j = 0; j < Size; j++)
        {
            if (_colorsOnBoard[0, j] == _colorsOnBoard[1, j] &&
                _colorsOnBoard[1, j] == _colorsOnBoard[2, j] &&
                _colorsOnBoard[0, j] != -1)
            {
                var winnerColor = (PlayerColor)_colorsOnBoard[j, 0];
                return (true, winnerColor);
            }
        }

        //check diagonal
        for (int k = 0; k < Size; k++)
        {
            if (_colorsOnBoard[0, 0] == _colorsOnBoard[1, 1] &&
                _colorsOnBoard[1, 1] == _colorsOnBoard[2, 2] &&
                _colorsOnBoard[1, 1] != -1)
            {
                var winnerColor = (PlayerColor)_colorsOnBoard[1, 1];
                return (true, winnerColor);
            }
        }
        return (false, PlayerColor.NONE);
    }
    public List<Vector2Int> ValidMoves(IPiece piece)
    {
        var possibleMoves = piece.AllPossibleMoves();
        var filteredPossibleMoves = new List<Vector2Int>();
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                foreach (var move in possibleMoves)
                {
                    var isValid = IsEmpty(move.x, move.y) &&
                        !HasPieceInBetween(piece.Pos, move);

                    if (isValid)
                        filteredPossibleMoves.Add(move);
                }
            }
        }
        return filteredPossibleMoves;
    }
    public bool HasValidMove(PlayerColor color)
    {
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                if (_colorsOnBoard[i, j] == (int)color)
                {
                    var type = (PieceType)_piecesOnBoard[i, j];
                    var piece = PieceFactory.CreatePiece(type, color, new Vector2Int(i, j));
                    var possibleMoves = piece.AllPossibleMoves();

                    foreach (var move in possibleMoves)
                    {
                        var isValid = IsEmpty(move.x, move.y) &&
                            !HasPieceInBetween(piece.Pos, move);

                        if (isValid)
                            return true;
                    }
                }
            }
        }

        return false;
    }

    public void ClearBoardForWinner(PlayerColor winnerColor)
    {
        int winner = (int)winnerColor;
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                if (_colorsOnBoard[i, j] != winner)
                {
                    _colorsOnBoard[i, j] = -1;
                    _piecesOnBoard[i, j] = -1;
                }
            }
        }

        OnBoardUpdate?.Invoke(this);
    }
}