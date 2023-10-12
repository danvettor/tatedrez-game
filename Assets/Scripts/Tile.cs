using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Tile : MonoBehaviour
{
    [SerializeField] Image _targetImage;
    [SerializeField] List<PieceTemplate> _availablePieces;

    public Vector2Int Pos;
    private Action<Vector2Int> _onTileClicked;
    public Action<Vector2Int> OnTileCliked
    {
        get => _onTileClicked;
        set
        {
            _onTileClicked = value;
            GetComponent<Button>().onClick.AddListener(() => _onTileClicked.Invoke(Pos));
        }
    }

    public void Draw(IPiece piece)
    {
        if (piece == null)
        {
            _targetImage.sprite = null;
            return;
        }
       
        var pieceTemplate = _availablePieces.Find((p) => p.Type == piece.Type);
        _targetImage.sprite = pieceTemplate.GetSprite(piece.Color);
    }

}
