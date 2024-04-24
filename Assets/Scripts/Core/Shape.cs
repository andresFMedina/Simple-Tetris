using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    [SerializeField] private bool canRotate = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Move(Vector3 direction)
    {
        transform.position += direction;
    }

    public void MoveLeft()
    {
        Move(Vector3.left);
    }

    public void MoveRight()
    {
        Move(Vector3.right);
    }

    public void MoveUp()
    {
        Move(Vector3.up);
    }

    public void MoveDown()
    {
        Move(Vector3.down);
    }

    public void RotateRight()
    {
        transform.Rotate(0, 0, -90);
    }

    public void RotateLeft()
    {
        transform.Rotate(0, 0, 90);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
