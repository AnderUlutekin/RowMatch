using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{

    public void EnterLevel1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void EnterLevel2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void EnterLevel3()
    {
        SceneManager.LoadScene("Level3");
    }

    public void EnterLevel4()
    {
        SceneManager.LoadScene("Level4");
    }

    public void EnterLevel5()
    {
        SceneManager.LoadScene("Level5");
    }

    public void EnterLevel6()
    {
        SceneManager.LoadScene("Level6");
    }

    public void EnterLevel7()
    {
        SceneManager.LoadScene("Level7");
    }

    public void EnterLevel8()
    {
        SceneManager.LoadScene("Level8");
    }

    public void EnterLevel9()
    {
        SceneManager.LoadScene("Level9");
    }

    public void EnterLevel10()
    {
        SceneManager.LoadScene("Level10");
    }

    public void ExitLevel()
    {
        SceneManager.LoadScene("MainScene");
    }
}
