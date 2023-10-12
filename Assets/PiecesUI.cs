using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PiecesUI : MonoBehaviour
{
    [SerializeField] private PlayerColor _playerColor;
    [SerializeField] private Button _rook;
    [SerializeField] private Button _bishop;
    [SerializeField] private Button _knight;

    public Action<PieceType> OnPieceSelected;
    public PieceType SelectedPiece { get; private set; }

    public PlayerColor PlayerColor { get { return _playerColor; } }
    void Awake()
    {
        _rook.onClick.AddListener(() => SelectedPiece = PieceType.ROOK);
        _knight.onClick.AddListener(() => SelectedPiece = PieceType.KNIGHT);
        _bishop.onClick.AddListener(() => SelectedPiece = PieceType.BISHOP);

    }

}
