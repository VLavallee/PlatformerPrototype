using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int livesAmount;
    [SerializeField] int coinsAmount;

    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI coinsText;

    private void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if(numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddToCoins()
    {
        coinsAmount++;
        if(coinsAmount > 4)
        {
            coinsAmount = 0;
            AddToLives();
        }
        coinsText.text = coinsAmount.ToString();
    }

    private void AddToLives()
    {
        livesAmount++;
        livesText.text = livesAmount.ToString();
    }

    private void RemoveFromLife()
    {
        livesAmount--;
        livesText.text = livesAmount.ToString();
    }

    public void ProcessRestart()
    {
        RemoveFromLife();
        if(livesAmount >= 0)
        {
            var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }
        else
        {
            livesAmount = 0;
            Debug.Log("Game Over"); // add a gameOver scene at this point
        }
    }
}
