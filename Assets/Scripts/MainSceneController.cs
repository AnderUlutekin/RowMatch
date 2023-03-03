using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneController : MonoBehaviour
{

    [SerializeField]
    private GameObject popupMenu;
    [SerializeField]
    private GameObject particlesPanel;
    [SerializeField]
    private Text highScoreText;

    public bool isPreviousSceneLevel = false;

    public Text[] highScores;
    public Text[] levelNames;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("NewHighScore"))
        {
            PlayerPrefs.SetInt("NewHighScore", 0);
        }
        if (!PlayerPrefs.HasKey("PreviousSceneLevel"))
        {
            PlayerPrefs.SetInt("PreviousSceneLevel", 0);
        }
        if (PlayerPrefs.GetInt("NewHighScore") == 1)
        {
            StartCoroutine(HighScoreScene());
        }
        else
        {
            particlesPanel.SetActive(false);
        }
        if (PlayerPrefs.GetInt("PreviousSceneLevel") == 1)
        {
            EnterPopup();
        }
        else
        {
            ExitPopup();
        }
        //PlayerPrefs.DeleteAll();
    }

    public void EnterPopup()
    {
        HighScores();
        LevelNames();
        popupMenu.SetActive(true);
        PlayerPrefs.SetInt("PreviousSceneLevel", 0);
    }

    public void ExitPopup()
    {
        popupMenu.SetActive(false);
    }

    private void HighScores()
    {
        for (int i = 0; i < highScores.Length; i++)
        {
            if (!PlayerPrefs.HasKey("Highscore" + (i+1)))
            {
                highScores[i].text = "No Score";
            }
            else
            {
                highScores[i].text = "High Score: " + PlayerPrefs.GetInt("Highscore" + (i+1)).ToString();
                PlayerPrefs.SetInt("levelsUnlocked", i+2);
            }
        }
    }

    private void LevelNames()
    {
        for (int i = 0; i < levelNames.Length; i++)
        {
            levelNames[i].text = "Level " + (i + 1);
        }
    }

    IEnumerator HighScoreScene()
    {
        highScoreText.text = PlayerPrefs.GetInt("LastHighScore").ToString();
        particlesPanel.SetActive(true);
        yield return new WaitForSeconds(10);
        particlesPanel.SetActive(false);
        PlayerPrefs.SetInt("NewHighScore", 0);
    }

} // class

