using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardView : MonoBehaviour
{
    [SerializeField] List<Tile> _boardTiles;

    public void CreateInitialBoard(Board board, GameController gameController)
    {
        board.OnBoardUpdate = UpdateBoard;
        var tileIndexes = 0;
        for (int i = 0; i < board.Size; i++)
        {
            for (int j = 0; j < board.Size; j++)
            {
                var color = (PlayerColor)board.ColorOnBoard[i,j];
                var type = (PieceType)board.PiecesOnBoard[i,j];
                _boardTiles[tileIndexes].Draw(color, type);
                _boardTiles[tileIndexes].Pos = new Vector2Int(i, j);
                _boardTiles[tileIndexes].OnTileCliked += (pos) => gameController.Play(pos);
                tileIndexes++;
            }
        }
    }

    private void UpdateBoard(Board board)
    {
        var tileIndexes = 0;
        for (int i = 0; i < board.Size; i++)
        {
            for (int j = 0; j < board.Size; j++)
            {
                var color = (PlayerColor)board.ColorOnBoard[i, j];
                var type = (PieceType)board.PiecesOnBoard[i, j];
                _boardTiles[tileIndexes].Draw(color, type);
                tileIndexes++;
            }
        }
    }



}
