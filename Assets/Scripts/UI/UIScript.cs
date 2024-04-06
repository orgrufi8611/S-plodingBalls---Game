using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class UIScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] TextMeshProUGUI multiplayer;
    [SerializeField] GameObject healthBar;
    [SerializeField] GameObject pause;
    [SerializeField] GameLogic gameLogic;
    [SerializeField] GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        multiplayer.text = "";
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Score: " + gameLogic.score;
        if(gameLogic.multiplayer != 1)
        {
            multiplayer.text = "x" + gameLogic.multiplayer;
        }
        else
        {
            multiplayer.text = "";
        }
    }

    public void PauseButton()
    {
        Time.timeScale = 0;
        gameLogic.active = false;
        pauseMenu.gameObject.SetActive(true);
        pause.SetActive(false);
        healthBar.SetActive(false);
    }

    public void Resume()
    {
        pause.SetActive(true);
        healthBar.SetActive(true);

    }
}
