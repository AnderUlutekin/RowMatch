using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{

    public int column;
    public int row;
    public int targetX, targetY;
    private GameObject otherFruit;
    private Board board;
    private LevelController levelController;

    private Vector2 firstTouchPos;
    private Vector2 finalTouchPos;
    private Vector2 tempPos;
    public float swipeAngle = 0;

    public bool moveable = true;
    
    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
        levelController = FindObjectOfType<LevelController>();
        targetX = (int)transform.position.x;
        targetY = (int)transform.position.y;
        row = targetY;
        column = targetX;
    }

    // Update is called once per frame
    void Update()
    {
        targetX = column;
        targetY = row;
        if (Mathf.Abs(targetX - transform.position.x) > .1)
        {
            tempPos = new Vector2(targetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPos, .3f);
        }
        else
        {
            tempPos = new Vector2(targetX, transform.position.y);
            transform.position = tempPos;
            board.allFruits[column, row] = this.gameObject;
        }
        if (Mathf.Abs(targetY - transform.position.y) > .1)
        {
            tempPos = new Vector2(transform.position.x, targetY);
            transform.position = Vector2.Lerp(transform.position, tempPos, .3f);
        }
        else
        {
            tempPos = new Vector2(transform.position.x, targetY);
            transform.position = tempPos;
            board.allFruits[column, row] = this.gameObject;
        }
    }

    private void OnMouseDown()
    {
        firstTouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseUp()
    {
        finalTouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (finalTouchPos.x <= board.width-1 && finalTouchPos.x >= 0 && finalTouchPos.y <= board.height-1 && finalTouchPos.y >= 0)
        {
            CalculateAngle();
        }
    }

    void CalculateAngle()
    {
        swipeAngle = Mathf.Atan2(finalTouchPos.y - firstTouchPos.y, finalTouchPos.x - firstTouchPos.x) * 180 / Mathf.PI;
        MoveFruits();
    }

    void MoveFruits()
    {
        if (swipeAngle > -35 && swipeAngle <= 35 && column < board.width - 1 && moveable)
        {
            // right swipe
            otherFruit = board.allFruits[column + 1, row];
            if (otherFruit.GetComponent<Fruit>().moveable)
            {
                otherFruit.GetComponent<Fruit>().column -= 1;
                column += 1;
                levelController.MakeMove();
            }
        }
        else if (swipeAngle > 55 && swipeAngle <= 125 && row < board.height - 1 && moveable)
        {
            // up swipe
            otherFruit = board.allFruits[column, row + 1];
            if (otherFruit.GetComponent<Fruit>().moveable)
            {
                otherFruit.GetComponent<Fruit>().row -= 1;
                row += 1;
                levelController.MakeMove();
            }
        }
        else if ((swipeAngle > 145 || swipeAngle <= -145) && column > 0 && moveable)
        {
            // left swipe
            otherFruit = board.allFruits[column - 1, row];
            if (otherFruit.GetComponent<Fruit>().moveable)
            {
                otherFruit.GetComponent<Fruit>().column += 1;
                column -= 1;
                levelController.MakeMove();
            }
        }
        else if (swipeAngle < -55 && swipeAngle >= -125 && row > 0 && moveable)
        {
            // down swipe
            otherFruit = board.allFruits[column, row - 1];
            if (otherFruit.GetComponent<Fruit>().moveable)
            {
                otherFruit.GetComponent<Fruit>().row += 1;
                row -= 1;
                levelController.MakeMove();
            }
        }
    }
}
