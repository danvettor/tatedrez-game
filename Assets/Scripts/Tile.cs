using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    [SerializeField] Image _targetImage;

    Piece _piece;
    Vector2 pos;

    public void DrawTile(Piece piece)
    {
        _piece = piece;
        _targetImage.sprite = _piece.PieceBlack;
    }
}
