using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LevelManager : MonoBehaviour
{
    public List<Button> levelButton;
    void Start()
    {
        int saveIndex = PlayerPrefs.GetInt("Level");
        for (int i = 0; i < levelButton.Count; i++)
        {
            if (i <= saveIndex)
            {
                levelButton[i].interactable = true;
            }
            else
            {
                levelButton[i].interactable = false;
            }
        }
    }

    public void LevelSelect()
    {
        int level = int.Parse(EventSystem.current.currentSelectedGameObject.name);
        SceneManager.LoadScene(level);
    }
}
