using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private PlayerColor _playerColor;
    [SerializeField] private Button _rook;
    [SerializeField] private Button _bishop;
    [SerializeField] private Button _knight;

    Dictionary<PieceType, Button> _buttonByType;
    public Action<PieceType> OnPieceSelected;

    public bool HasPieceToPlace { get => _buttonByType.Count > 0; }
    private int _placedPiecesCount;
    public PlayerColor PlayerColor { get { return _playerColor; } }
    void Awake()
    {
        _placedPiecesCount = 0;
        _buttonByType = new Dictionary<PieceType, Button>
        {
            {PieceType.ROOK, _rook },
            {PieceType.BISHOP, _bishop},
            {PieceType.KNIGHT, _knight},
        };

        _rook.onClick.AddListener(() => {
            OnButtonClick(PieceType.ROOK);
        });
        _knight.onClick.AddListener(() => {
            OnButtonClick(PieceType.KNIGHT);
        });
        _bishop.onClick.AddListener(() => {
            OnButtonClick(PieceType.BISHOP);
        });
    }
    public void HighlightUI(PieceType choosenPiece)
    {
        foreach (var piece in _buttonByType)
        {
            _buttonByType[piece.Key].image.color = piece.Key == choosenPiece ? Color.red : Color.white;
        }
    }

    public void DeselectUI()
    {
        foreach (var piece in _buttonByType)
        {
            _buttonByType[piece.Key].image.color =  Color.white;
        }
    }
    public void OnPiecePlaced(PieceType typePlaced)
    {
        _buttonByType[typePlaced].transform.parent.gameObject.SetActive(false);
        _buttonByType.Remove(typePlaced);
        _placedPiecesCount++;
    }
    private void OnButtonClick(PieceType type)
    {
        OnPieceSelected?.Invoke(type);
    }

}
