using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUnlockHandler : MonoBehaviour
{

    [SerializeField]
    Button[] buttons;

    int unlockedLevelsNumber;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("levelsUnlocked"))
        {
            PlayerPrefs.SetInt("levelsUnlocked", 1);
        }

        unlockedLevelsNumber = PlayerPrefs.GetInt("levelsUnlocked");

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
    }

    private void Update()
    {
        unlockedLevelsNumber = PlayerPrefs.GetInt("levelsUnlocked");
        for (int i = 0; i < unlockedLevelsNumber; i++)
        {
            if (unlockedLevelsNumber <= buttons.Length)
                buttons[i].interactable = true;
        }
    }


}
