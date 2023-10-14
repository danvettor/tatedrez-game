using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BoardTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void PieceShouldBeEqualWherePositioned()
    {
        var board = new Board();
        board.PlacePiece(new Vector2Int(0,0), PieceType.BISHOP, PlayerColor.BLACK);
        var piece = board.GetPiece(0, 0);
        Assert.AreEqual(piece.Type, PieceType.BISHOP);
        Assert.AreEqual(piece.Color, PlayerColor.BLACK);

    }

    [Test]
    public void PieceShouldNotBePlacedIfOccupied()
    {
        var board = new Board();
        board.PlacePiece(new Vector2Int(0, 0), PieceType.BISHOP, PlayerColor.BLACK);
        board.PlacePiece(new Vector2Int(0, 0), PieceType.KNIGHT, PlayerColor.BLACK);

        var piece = board.GetPiece(0, 0);
        Assert.AreNotEqual(piece.Type, PieceType.KNIGHT);

    }

    [Test]
    public void PieceShouldBeMovedToNewEmptyPosition_AndLetPreviousPositionEmpty()
    {
        var board = new Board();
        board.PlacePiece(new Vector2Int(0, 0), PieceType.BISHOP, PlayerColor.BLACK);
        var piece = board.GetPiece(0, 0);

        board.MovePiece(piece ,new Vector2Int(0, 1));

        Assert.AreEqual(board.GetPiece(0, 0).Type, PieceType.NONE);
        Assert.AreEqual(board.GetPiece(0, 1).Type, PieceType.BISHOP);

    }

    [Test]
    public void HorizontalMovement_MovementShouldBeValid_AsTheresNoPieceInBetweenPositions()
    {
        var board = new Board();
        board.PlacePiece(new Vector2Int(0, 0), PieceType.ROOK, PlayerColor.BLACK);

        var hasPieceInBetween = board.HasPieceInBetween(new Vector2Int(0, 0), new Vector2Int(0, 2));

        Assert.IsFalse(hasPieceInBetween);
    }

    [Test]
    public void HorizontalMovement_ShouldNotBeValid_AsTheresPieceInBetweenPositions()
    {
        var board = new Board();
        board.PlacePiece(new Vector2Int(0, 0), PieceType.ROOK, PlayerColor.BLACK);
        board.PlacePiece(new Vector2Int(0, 1), PieceType.ROOK, PlayerColor.WHITE);


        var hasPieceInBetween = board.HasPieceInBetween(new Vector2Int(0, 0), new Vector2Int(0, 2));

        Assert.IsTrue(hasPieceInBetween);
    }


}
