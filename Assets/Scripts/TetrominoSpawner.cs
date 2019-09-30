using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrominoSpawner : MonoBehaviour
{
    public GameObject[] Tetrominos;

    void Start()
    {
        SpawnTetromino();
    }

    public void SpawnTetromino()
    {
        Instantiate(Tetrominos[Random.Range(0,Tetrominos.Length)], transform.position, Quaternion.identity);
    }
}
