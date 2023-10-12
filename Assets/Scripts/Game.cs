using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private Board _board;
    [SerializeField] BoardView _boardView;
    [SerializeField]
    List<PieceTemplate> _availablePieces;
    [SerializeField] PiecesUI _blackUI;
    [SerializeField] PiecesUI _whiteUI;
    private GameController _gameController;
    private void Awake()
    {
        NewGame();
    }
    private void NewGame()
    {
        _board = new Board();
        _gameController = new GameController(_board, _boardView, _availablePieces);
        _gameController.StartNewGame();
        _blackUI.OnPieceSelected = (type) => _gameController.OnSelectedPiece(type, _blackUI.PlayerColor);
        _whiteUI.OnPieceSelected = (type) => _gameController.OnSelectedPiece(type, _whiteUI.PlayerColor);

        //onpiece placed faz algo. talvez juntas o Game com Game controller naquele esquema de Game Presenter
    }

}
public enum PlayerColor
{
    EMPTY = -1,
    BLACK = 1,
    WHITE
}
