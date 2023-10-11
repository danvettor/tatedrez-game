using UnityEngine;

public interface IPiece
{
     PieceType Type { get;  set; }
     PlayerColor Color { get;  set; }
     Vector2Int Pos { get;  set; }

     bool IsValidMove(Vector2Int posToMove);
  
}