using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Board _gameBoard;
    Spawner _spawner;

    // Start is called before the first frame update
    void Start()
    {
        _gameBoard = FindObjectOfType<Board>();
        _spawner = FindObjectOfType<Spawner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
