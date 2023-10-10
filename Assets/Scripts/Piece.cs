using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Piece", menuName = "ScriptableObjects/Piece")]
public class Piece : ScriptableObject
{
    [SerializeField] private Sprite PieceWhite;
    [SerializeField] private Sprite PieceBlack;
    [SerializeField] private PieceType _type;
    public PieceType Type => _type;

    public Sprite GetSprite(PlayerColor playerColor)
    {
        return playerColor == PlayerColor.BLACK ? PieceBlack : PieceWhite;
    }
}
