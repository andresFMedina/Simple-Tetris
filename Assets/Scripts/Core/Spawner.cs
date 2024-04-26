using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private Shape[] _shapes;
    [SerializeField]
    private Transform[] _shapesPosition;

    [SerializeField]
    private float queueShapeScale = 0.5f;

    [SerializeField]
    private Queue<Shape> _shapesQueue = new();
    // Start is called before the first frame update

    private void Start()
    {
        FillQueue();
    }

    Shape GetRandomShape()
    {
        var index = Random.Range(0, _shapes.Length);
        return _shapes[index];
    }

    public Shape SpawnShape()
    {
        Shape shape = GetQueuedShape();
        shape.transform.position = transform.position;
        shape.transform.localScale = Vector3.one;
        return shape;
    }

    void FillQueue()
    {
        for (int i = 0; i < _shapesPosition.Length; i++)
        {
            if (_shapesQueue.ElementAtOrDefault(i) == null)
            {
                var shapePosition = _shapesPosition[i].position;
                var shapeScale = new Vector3(queueShapeScale, queueShapeScale, queueShapeScale);
                Shape newShape = Instantiate(GetRandomShape(), transform.position, Quaternion.identity);
                newShape.transform.position = shapePosition + newShape.queueOffset;
                newShape.transform.localScale = shapeScale;
                _shapesQueue.Enqueue(newShape);
            }
        }
    }

    private Shape GetQueuedShape()
    {
        Shape nextShape = _shapesQueue.Dequeue();

        int i = 0;
        foreach (var shape in _shapesQueue)
        {
            shape.transform.position = _shapesPosition[i].position;
            i++;
        }

        FillQueue();

        return nextShape;
    }
}
