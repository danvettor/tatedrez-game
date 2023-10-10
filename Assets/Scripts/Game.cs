using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private Board _board;
    [SerializeField] BoardView _boardView;
    private GameController _gameController;
    private void Awake()
    {
        NewGame();
    }
    private void NewGame()
    {
        _board = new Board();
        _gameController = new GameController(_board, _boardView);
        _gameController.StartNewGame();
    }

}
public enum PlayerColor
{
    EMPTY = -1,
    BLACK = 1,
    WHITE
}
