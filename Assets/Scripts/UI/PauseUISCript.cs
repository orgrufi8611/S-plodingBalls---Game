using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseUISCript : MonoBehaviour
{
    [SerializeField] GameObject UI;
    [SerializeField] GameLogic gameLogic;
    [SerializeField] Slider sfx;
    [SerializeField] Slider bgm;

    private void Start()
    {
        sfx.value = VolumeSettings.SFXVolume;
        bgm.value = VolumeSettings.BGMVolume;
    }
    private void Update()
    {
        
    }

    public void Retry()
    {
        Time.timeScale = 1;
        GameObject.Find("SceneController").GetComponent<SceneController>().ResetScene();
    }

    public void ExitButton()
    {
        Time.timeScale = 1;
        GameObject.Find("SceneController").GetComponent<SceneController>().ImidiateLoad(SceneController.sceneNames[0]);
    }

    public void ResumeButton() 
    {
        Time.timeScale = 1;
        gameLogic.active = true;
        UI.GetComponent<UIScript>().Resume();
        gameObject.SetActive(false);
    }

    public void SetBGMVolume()
    {
        float value = bgm.value;
        GameObject.Find("SoundController").GetComponent<VolumeSettings>().ChangeVolumeBGM(value);
    }

    public void SetSFXVolume()
    {
        float value = sfx.value;
        GameObject.Find("SoundController").GetComponent<VolumeSettings>().ChangeVolumeSFX(value);
    }

}
