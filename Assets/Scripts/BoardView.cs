using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardView : MonoBehaviour
{
    [SerializeField] List<Tile> _boardTiles;
    [SerializeField] List<Image> _tileHighlights;
    [SerializeField] List<Image> _possibleMovesHighlight;

    Image _highlightedTile;
    public void CreateInitialBoard(Board board, Action<Vector2Int> onTileClicked)
    {
        board.OnBoardUpdate = UpdateBoard;
        var tileIndexes = 0;
        for (int i = 0; i < board.Size; i++)
        {
            for (int j = 0; j < board.Size; j++)
            {
                var piece = board.GetPiece(i,j);
                _boardTiles[tileIndexes].Draw(piece);
                _boardTiles[tileIndexes].Pos = new Vector2Int(i, j);
                _boardTiles[tileIndexes].OnTileCliked = onTileClicked;
                tileIndexes++;
            }
        }
    }

    public void HighlightPossibleMoves(List<Vector2Int> positions)
    {
        var index = 0;
        DeselectMoveHighlights();
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                foreach(var pos in positions)
                {
                    if (i == pos.x && j == pos.y)
                    {
                        _possibleMovesHighlight[index].color = Color.green;
                    }
                }
                
                index++;

            }
        }
    }
    public void DeselectMoveHighlights()
    {
        foreach (var tile in _possibleMovesHighlight)
        {
            tile.color = Color.clear;
        }
    }

    public void HighlightTile(Vector2Int pos)
    {
        var index = 0;
        bool exitLoop = false;
        for (int i = 0; i < 3 ; i++)
        {
            if (exitLoop)
                break;
            for (int j = 0; j <3; j++)
            {
                if (i == pos.x && j == pos.y)
                {
                    exitLoop = true;
                    break;
                }
                index++;

            }
        }
        Debug.Log($"Index {index} pos{ pos.x} {pos.y}");
        _highlightedTile = _tileHighlights[index];
        _highlightedTile.color = Color.red;
    }

    public void DeselectTile()
    {
        if(_highlightedTile != null)
        {
            _highlightedTile.color = Color.clear;
            _highlightedTile = null;
        }
    }

    private void UpdateBoard(Board board)
    {
        var tileIndexes = 0;
        for (int i = 0; i < board.Size; i++)
        {
            for (int j = 0; j < board.Size; j++)
            {
                var piece = board.GetPiece(i, j);
                _boardTiles[tileIndexes].Draw(piece);
                tileIndexes++;
            }
        }
    }



}
