using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AlertUI : MonoBehaviour
{
    [SerializeField] TMP_Text _alertText;

    [SerializeField] GameObject _blackKing;

    [SerializeField] GameObject _whiteKing;
    [SerializeField] Animator _animator;

    public void OnTurn(PlayerColor turnColor)
    {
        if(turnColor == PlayerColor.BLACK)
        {
            _blackKing.SetActive(true);
            _whiteKing.SetActive(false);

            _alertText.color = Color.black;
            
        }
        else if (turnColor == PlayerColor.WHITE)
        {
            _blackKing.SetActive(false);
            _whiteKing.SetActive(true);
            _alertText.color = Color.white;
        }

        _alertText.text = turnColor + " Turn";
        _animator.SetTrigger("AlertIn");
    }
    public void OnEndGame(PlayerColor winnerColor)
    {
        _blackKing.SetActive(winnerColor == PlayerColor.BLACK);
        _whiteKing.SetActive(winnerColor == PlayerColor.WHITE);
        _alertText.text = winnerColor + " WIN";
        _animator.SetTrigger("PopIn");
    }

    internal void OnTiedGame()
    {
        _blackKing.SetActive(false);
        _whiteKing.SetActive(false);
        _alertText.text =  "TIE";
        _animator.SetTrigger("PopIn");
    }
}
