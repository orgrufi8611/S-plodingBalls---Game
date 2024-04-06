using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public bool active;
    public float score;
    public float multiplayer;
    public int combo;
    public float life;
    public float gameSpeedEffector;
    [SerializeField] HealthBar healthBar;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject UI;
    public bool newHighScore;
    public float highScore;
    bool gameOver;
    
    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        newHighScore = false;
        if (PlayerPrefs.HasKey("highScore"))
        {
            highScore = PlayerPrefs.GetFloat("highScore");
        }
        else
        {
            PlayerPrefs.SetFloat("highScore", 0);
        }

        gameSpeedEffector = 1;
        active = true;
        score = 0;
        multiplayer = 1;
        combo = 0;
        healthBar.SetMaxHealth(life);
        gameOverScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        healthBar.SetHealth(life);
        ComboMultiplayer();
        if(life<= 0 && !gameOver)
        {
            GameOver();
        }
    }

    //increase score multiply with combo climb
    void ComboMultiplayer()
    {
        if (combo > 3)
        {
            multiplayer = 2;
        }
        else if (combo > 5)
        {
            combo = 3;
        }
        else if (combo > 10)
        {
            multiplayer = 5;
        }
        else
        {
            multiplayer = 1;
        }
    }

    public void ReduceLife(float hp)
    {
        life -= hp;
    }

    public void AddPoints(int points)
    {
        score += points * multiplayer;
    }

    public void GameOver()
    {
        gameOver = true;
        active = false;
        Time.timeScale = 0;
        UI.SetActive(false);
        gameOverScreen.SetActive(true);
        if (highScore < score)
        {
            PlayerPrefs.SetFloat("highScore", score);
            newHighScore = true;
        }
        gameOverScreen.GetComponent<GameOverUIScript>().GameOverActive();
    }
}
