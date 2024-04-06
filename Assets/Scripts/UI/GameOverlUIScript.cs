using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUIScript : MonoBehaviour
{
    [SerializeField] Button retry;
    [SerializeField] Button exit;
    [SerializeField] TextMeshProUGUI highScore;
    [SerializeField] TextMeshProUGUI msgScore;
    [SerializeField] TextMeshProUGUI Endtitle;
    [SerializeField] GameLogic gameLogic;

    public void GameOverActive()
    {
        highScore.text = "High Score:" + gameLogic.highScore;
        if (gameLogic.newHighScore)
        {
            msgScore.text = "New High Score";
        }
        else
        {
            msgScore.text = "Score:" + gameLogic.score;
        }
    }

    public void Exit()
    {
        Time.timeScale = 1;
        GameObject.Find("SceneController").GetComponent<SceneController>().ImidiateLoad(SceneController.sceneNames[0]);
    }

    public void Retry()
    {
        Time.timeScale = 1.0f;
        GameObject.Find("SceneController").GetComponent<SceneController>().ResetScene();
    }

}
