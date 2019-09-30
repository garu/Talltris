using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetromino : MonoBehaviour
{
    public Vector3 rotationPoint; // rotation point in relation to tetrimino position
//    private float previousTime;
//    public float fallTime = 0.8f;
//    public static int height = 20;
//    public static int width = 10;
//    private static Transform[,] grid = new Transform[width, height];

    enum Movement { None, Left, Right, Down, BounceLeft, BounceRight }
    Movement movement;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        rb.velocity = new Vector3(0,-2,0);
    }

    void FixedUpdate()
    {
        if (movement == Movement.Right)
        {
            rb.AddForce(new Vector3(100,0,0));
            movement = Movement.None;
        }
        else if (movement == Movement.Left)
        {
            rb.AddForce(new Vector3(-100,0,0));
            movement = Movement.None;
        }
        else if (movement == Movement.Down)
        {
            rb.AddForce(new Vector3(0,-100,0));
            movement = Movement.None;
        }
        else if (movement == Movement.BounceRight)
        {
            //rigidbody.AddForce(new Vector3(-200,0,0));
            rb.velocity = new Vector3(-2,-5,0);
            movement = Movement.None;
        }
        else if (movement == Movement.BounceLeft)
        {
            //rigidbody.AddForce(new Vector3(200,0,0));
            rb.velocity = new Vector3(2,-5,0);
            movement = Movement.None;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "endgame")
        {
                Debug.Log("GAME OVER!");
                //Destroy(gameObject);
                Time.timeScale = 0;
                FindObjectOfType<ScoreUpdater>().GameOver();
        }
        else if (col.gameObject.name == "sky")
        {
            Debug.Log("Heaven is reached");
            DestroyAllTetrominos();
            FindObjectOfType<ScoreUpdater>().BonusScore();
            FindObjectOfType<TetrominoSpawner>().SpawnTetromino();
        }
        if (this.enabled)
        {
            Debug.Log("collision with " + col.gameObject.name);
            if (col.gameObject.name == "wall-left")
            {
                movement = Movement.BounceLeft;
            }
            else if (col.gameObject.name == "wall-right")
            {
                movement = Movement.BounceRight;
            }
            else {
                rb.gravityScale = 1.0f;
                this.enabled = false;
                FindObjectOfType<ScoreUpdater>().UpdateScore();
                FindObjectOfType<TetrominoSpawner>().SpawnTetromino();
            }
        }
    }

    void Update()
    {
        if (Input.GetKey("escape") || Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }
        else if (Time.timeScale == 0 && Input.anyKeyDown)
        {
            DestroyAllTetrominos();
            FindObjectOfType<ScoreUpdater>().NewGame();
            FindObjectOfType<TetrominoSpawner>().SpawnTetromino();
            Time.timeScale = 1;
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            movement = Movement.Left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            movement = Movement.Right;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // RotateAround only works in "world" coordinate, but we set
            // rotationPoint to a local coordinate, so we use TransformPoint
            // to switch from local to world.
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0,0,1), 90);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            movement = Movement.Down;
        }
    }

    void DestroyAllTetrominos()
    {
        GameObject[] tetrominos = GameObject.FindGameObjectsWithTag("tetromino");

        foreach (GameObject tetromino in tetrominos)
        {
            Destroy(tetromino);
        }
    }


/*
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!ValidMove())
            {
                transform.position -= new Vector3(-1, 0, 0);
            }
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!ValidMove())
            {
                transform.position -= new Vector3(1, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // RotateAround only works in "world" coordinate, but we set
            // rotationPoint to a local coordinate, so we use TransformPoint
            // to switch from local to world.
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0,0,1), 90);
            if (!ValidMove())
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0,0,1), -90);
            }
        }

        if (Time.time - previousTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime / 10 : fallTime))
        {
            transform.position += new Vector3(0, -1, 0);
            if (!ValidMove()) {
                transform.position -= new Vector3(0, -1, 0);
                AddToGrid();
                this.enabled = false;
                FindObjectOfType<TetrominoSpawner>().SpawnTetromino();
            }
            previousTime = Time.time;
        }
    }

    void AddToGrid()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            grid[roundedX,roundedY] = children;
        }
    }

    bool ValidMove()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= height)
            {
                return false;
            }

            if (grid[roundedX, roundedY] != null) {
                return false;
            }
        }
        return true;
    }
*/

}
