using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{

    public int level_number;
    public int grid_width;
    public int grid_height;
    public int move_count;
    public string[] grid;

    string filePath;
    public string fileName;

    private Board board;
    private int totMoves;

    public Text inGameMoves;
    public Text inGameScore;
    public Text inGameHighScore;
    public int score;
    public int highScore;

    void Awake()
    {
        board = FindObjectOfType<Board>();
        filePath = Application.dataPath + "/Levels/" + fileName;
        ReadFromFile();
        InitializeBoard();
        totMoves = 0;
        score = 0;
        if (!PlayerPrefs.HasKey("Highscore" + level_number))
        {
            highScore = 0;
        }
        else
        {
            highScore = PlayerPrefs.GetInt("Highscore" + level_number);
        }
        inGameHighScore.text = highScore.ToString();
        inGameMoves.text = move_count.ToString();
        inGameScore.text = score.ToString();
    }

    public void InitializeBoard()
    {
        board.width = grid_width;
        board.height = grid_height;
        board.grid = grid;
        board.allTiles = new BackgroundTile[board.width, board.height];
        board.allFruits = new GameObject[board.width, board.height];
        board.SetUp();
    }

    public void ReadFromFile()
    {
        StreamReader reader = new StreamReader(filePath);

        string line = reader.ReadLine();
        int pos = line.IndexOf(":");
        level_number = int.Parse(line.Substring(pos + 1));

        line = reader.ReadLine();
        pos = line.IndexOf(":");
        grid_width = int.Parse(line.Substring(pos + 1));

        line = reader.ReadLine();
        pos = line.IndexOf(":");
        grid_height = int.Parse(line.Substring(pos + 1));

        line = reader.ReadLine();
        pos = line.IndexOf(":");
        move_count = int.Parse(line.Substring(pos + 1));

        line = reader.ReadLine();
        pos = line.IndexOf(":");
        line = line.Substring(pos + 2);
        grid = line.Split(",");

        reader.Close();
    }

    private bool AreThereMovesLeft()
    {
        if (totMoves < move_count)
        {
            return true;
        }
        return false;
    }

    public void MakeMove()
    {
        totMoves += 1;
        inGameMoves.text = (int.Parse(inGameMoves.text) - 1).ToString();

        if (!AreThereMovesLeft())
        {
            if (!PlayerPrefs.HasKey("Highscore" + level_number))
            {
                PlayerPrefs.SetInt("Highscore" + level_number, score);
                PlayerPrefs.SetInt("NewHighScore", 1);
                PlayerPrefs.SetInt("LastHighScore", score);
            }
            else if (PlayerPrefs.GetInt("Highscore" + level_number) < score)
            {
                PlayerPrefs.SetInt("Highscore" + level_number, score);
                PlayerPrefs.SetInt("NewHighScore", 1);
                PlayerPrefs.SetInt("LastHighScore", score);
            }
            PlayerPrefs.SetInt("PreviousSceneLevel", 1);
            SceneManager.LoadScene("MainScene");
        }
    }

    public void ScoreChange()
    {
        inGameScore.text = score.ToString();
        if (score > highScore)
        {
            highScore = score;
            inGameHighScore.text = highScore.ToString();
        }
    }
}
