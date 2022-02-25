using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject levelCompletedPanel, gameOverPanel, soundButton, restartButton, backToMenuButton, muteImage, unMuteImage;
    public TextMeshProUGUI levelCompletedText, levelText;

    [HideInInspector]
    public bool canSpawn = false;

    void Start()
    {

        if (Time.time == Time.timeSinceLevelLoad)
            SceneManager.LoadScene(PlayerPrefs.GetInt("Level", 0));
        else
            PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex);



        SoundCheck();
        levelText.text = "Level " + (SceneManager.GetActiveScene().buildIndex).ToString();
        levelCompletedPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        if (PlayerPrefs.GetInt("Audio", 0) == 0)
        {
            PlayerPrefs.SetInt("Audio", 1);
        }
    }

    public void LevelCompletedActivation()
    {
        levelCompletedPanel.SetActive(true);
        levelText.enabled = false;
        soundButton.SetActive(false);
        restartButton.SetActive(false);
        backToMenuButton.SetActive(false);
        FindObjectOfType<SoundManager>().LevelCompletedSound();
        levelCompletedText.text = "Level " + (SceneManager.GetActiveScene().buildIndex).ToString() + " Passed";
    }

    public void NextLevelButton()
    {
        if (SceneManager.sceneCountInBuildSettings != SceneManager.GetActiveScene().buildIndex + 1)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else
            SceneManager.LoadScene("Level01");
            //Debug.Log("THERE ARE NO MORE SCENES!");
    }
    public void BackToMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void GameOverPanelActivation()
    {
        gameOverPanel.SetActive(true);
        levelCompletedPanel.SetActive(false);
        levelText.enabled = false;
        soundButton.SetActive(false);
        restartButton.SetActive(false);
        backToMenuButton.SetActive(false);
    }

    public void SoundCheck()
    {
        if (PlayerPrefs.GetInt("Audio", 0) == 0)
        {
            muteImage.SetActive(false);
            unMuteImage.SetActive(true);
            FindObjectOfType<SoundManager>().soundIsOn = true;
            FindObjectOfType<SoundManager>().PlayBackgroundMusic();
        }
        else
        {
            unMuteImage.SetActive(false);
            muteImage.SetActive(true);
            FindObjectOfType<SoundManager>().soundIsOn = false;
            FindObjectOfType<SoundManager>().StopBackgroundMusic();
        }
    }

    public void RestartButton()
    {
        FindObjectOfType<SoundManager>().ButtonClickSound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SoundButton()
    {
        FindObjectOfType<SoundManager>().ButtonClickSound();
        if (PlayerPrefs.GetInt("Audio", 0) == 0)
            PlayerPrefs.SetInt("Audio", 1);
        else
            PlayerPrefs.SetInt("Audio", 0);
        SoundCheck();
    }
}
