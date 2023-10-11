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
            return;
        Draw(piece.Color, piece.Type);        
    }

    private void Draw(PlayerColor playerColor, PieceType pieceType)
    {
        if (playerColor == PlayerColor.EMPTY)
            return;
        var piece = _availablePieces.Find((p) => p.Type == pieceType);
        _targetImage.sprite = piece.GetSprite(playerColor);
    }


}
