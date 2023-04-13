using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager: MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance { get => instance; }


    [SerializeField] private GameObject startGame;

    [SerializeField] private GameObject endGame;

    [SerializeField] private Text textWhoWin;

    

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        startGame.SetActive(true);

        endGame.SetActive(false);
    }

    public void StartGame()
    {
        startGame.SetActive(false);
        GameManager.Instance.IsPause = false;
    }

    public bool EndGame()
    {
        endGame.SetActive(true);
        if (endGame.activeSelf) return true;
        else return false;
    }

    public void SetWinner(ref ColorType color) 
    {

        textWhoWin.text = color.ToString();
    }

}
