using UnityEngine;

public class Ghost : MonoBehaviour
{
    Shape _ghostShape = null;
    bool _hitBottom;
    public Color _color = new Color(1f, 1f, 1f, 0.2f);


    public void DrawShape(Shape originalShape, Board gameBoard)
    {
        if (_ghostShape == null)
        {
            _ghostShape = Instantiate(originalShape, originalShape.transform.position, originalShape.transform.rotation);
            _ghostShape.name = "GhostShape";

            SpriteRenderer[] spriteRenderers = _ghostShape.GetComponentsInChildren<SpriteRenderer>();
            foreach (var spriteRenderer in spriteRenderers)
            {
                spriteRenderer.color = _color;
            }
        }
        else
        {
            _ghostShape.transform.position = originalShape.transform.position;
            _ghostShape.transform.rotation = originalShape.transform.rotation;
        }

        _hitBottom = false;

        while (!_hitBottom)
        {
            _ghostShape.MoveDown();
            if (!gameBoard.IsValidPosition(_ghostShape))
            {
                _ghostShape.MoveUp();
                _hitBottom = true;
            }
        }
    }

    public void ResetShape()
    {
        Destroy(_ghostShape.gameObject);
    }
}
