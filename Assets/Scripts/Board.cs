using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    [SerializeField] List<Tile> _boardPositions;
    [SerializeField] Piece _availablePiece;



    private void Awake()
    {
        foreach (var position in _boardPositions)
        {
            position.onClick.AddListener(() =>
            {
                position.GetComponent<Tile>().DrawTile(_availablePiece);
                Debug.Log("Clicked on the button");
            });
        }
    }
}
