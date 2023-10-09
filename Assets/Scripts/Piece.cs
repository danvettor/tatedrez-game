using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Piece", menuName = "ScriptableObjects/Piece")]
public class Piece : ScriptableObject
{
    public Sprite PieceWhite;
    public Sprite PieceBlack;
    public PieceType Type;

}
