using System.Collections.Generic;
using UnityEngine;

public abstract class Piece : IPiece
{
    public PieceType Type { get; set; }
    public PlayerColor Color { get; set; }
    public Vector2Int Pos { get; set; }
    public List<Vector2Int> AllPossibleMoves()
    {
        var possibleMoves = new List<Vector2Int>();
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                var moveTo = new Vector2Int(i, j);
                if (IsValidMove(moveTo))
                    possibleMoves.Add(moveTo);

            }
        }

        return possibleMoves;
    }
    public abstract bool IsValidMove(Vector2Int posToMove);
}


