using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private Shape[] _shapes;
    // Start is called before the first frame update

    Shape GetRandomShape()
    {
        var index = Random.Range(0, _shapes.Length);
        return _shapes[index];
    }

    public Shape SpawnShape() =>
        Instantiate(GetRandomShape(), transform.position, Quaternion.identity);


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
