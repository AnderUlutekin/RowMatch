using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{

    public int width;
    public int height;

    [SerializeField]
    private GameObject tilePrefab;

    [SerializeField]
    private GameObject[] fruits;

    public BackgroundTile[,] allTiles;

    public GameObject[,] allFruits;

    public string[] grid;

    private LevelController levelController;

    private void Start()
    {
        levelController = FindObjectOfType<LevelController>();
        //allTiles = new BackgroundTile[width, height];
        //allFruits = new GameObject[width, height];
    }

    private void Update()
    {
        HandleMatch();
    }

    public void SetUp()
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                Vector2 tempPos = new Vector2(j, i);
                GameObject backgroundTile = Instantiate(tilePrefab, tempPos, Quaternion.identity) as GameObject;
                backgroundTile.transform.parent = this.transform;
                backgroundTile.name = "( " + j + ", " + i + " )";

                int fruitToUse = 0;
                if (grid[(i*width)+j] == "r")
                {
                    fruitToUse = 0;
                }
                else if (grid[(i * width) + j] == "y")
                {
                    fruitToUse = 1;
                }
                else if (grid[(i * width) + j] == "b")
                {
                    fruitToUse = 2;
                }
                else if (grid[(i * width) + j] == "g")
                {
                    fruitToUse = 3;
                }
                GameObject fruit = Instantiate(fruits[fruitToUse], tempPos, Quaternion.identity);
                fruit.transform.parent = this.transform;
                fruit.name = "( " + j + ", " + i + " )";
                allFruits[j, i] = fruit;
            }
        }
    }

    public void HandleMatch()
    {
        int row = 0;
        if (IsThereMatch(ref row))
        {
            string fruitTag = allFruits[0, row].GetComponent<Fruit>().tag;
            if (fruitTag == "Apple")
            {
                Debug.Log("Apple match");
                levelController.score += 100 * width;
                levelController.ScoreChange();
            }
            else if (fruitTag == "Pear")
            {
                levelController.score += 150 * width;
                levelController.ScoreChange();
                Debug.Log("Pear match");
            }
            else if (fruitTag == "Blueberry")
            {
                levelController.score += 200 * width;
                levelController.ScoreChange();
                Debug.Log("Blueberry match");
            }
            else if (fruitTag == "Banana")
            {
                levelController.score += 250 * width;
                levelController.ScoreChange();
                Debug.Log("Banana match");
            }
        }
    }

    private bool IsThereMatch(ref int row)
    {
        bool match = true;
        GameObject checkFruit = allFruits[0, 0];
        for (int h = 0; h < height; h++)
        {
            for (int w = 0; w < width; w++)
            {
                if (w == 0)
                {
                    checkFruit = allFruits[w, h];
                }
                else
                {
                    if (allFruits[w, h].CompareTag(checkFruit.tag) == false)
                    {
                        match = false;
                        break;
                    }
                }
            }
            bool isMoveable = allFruits[0, h].GetComponent<Fruit>().moveable;
            if (match && isMoveable)
            {
                for (int i = 0; i < width; i++)
                {
                    allFruits[i, h].GetComponent<Fruit>().moveable = false;
                }
                row = h;
                return true;
            }
            else
            {
                match = true;
            }
        }
        return false;
    }

}
