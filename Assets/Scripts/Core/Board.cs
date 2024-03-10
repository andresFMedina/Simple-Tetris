using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] Transform _emptySprite;
    [SerializeField] int _height = 30;
    [SerializeField] int _width = 10;
    [SerializeField] int _header = 8;

    Transform[,] _grid;

    private void Awake()
    {
        _grid = new Transform[_width, _height];
    }

    void Start()
    {
        DrawEmptyCells();
    }
    
    void Update()
    {
        
    }

    void DrawEmptyCells()
    {
        if (_emptySprite)
        {
            for (int y = 0; y < _height - _header; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    Transform clone;
                    clone = Instantiate(_emptySprite, new Vector3(x, y, 0), Quaternion.identity);
                    clone.name = $"Board Space ( {x}, {y} )";
                    clone.transform.parent = transform;
                }
            }
        }
        else
        {
            print("No empty square assigned");
        }
    }
}
