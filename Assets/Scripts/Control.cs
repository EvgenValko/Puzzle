using UnityEngine;
using System.Linq;

public class Control : MonoBehaviour
{
    private bool _isMouseDown = false;
    private Vector2 _startPozition;
    private Coordinates _coordinates;
    private bool _isJoin;
    public Matrix _matrix;
    public Coordinates Point { get => _coordinates; }
   
    void Update()
    {
        MovingFigures();
    }

    private void MovingFigures()
    {
        Vector2 cursor = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (_isMouseDown && !_isJoin)
        {
            transform.parent.position = cursor - (Vector2)(transform.position - transform.parent.position);
        }
    }

    private void OnMouseDown()
    {
        _isMouseDown = true;
        _startPozition = transform.parent.position;
    }

    private void OnMouseUp()
    {
        _isMouseDown = false;
        CalculateCoordinatesFigureElements();       

        if (IsFigureOnMatrix(gameObject.transform.parent))
        {
            transform.parent.localPosition = new Vector2(Mathf.Round(transform.parent.localPosition.x), Mathf.Round(transform.parent.localPosition.y));
            foreach (var item in gameObject.transform.parent.GetComponentsInChildren<Control>())
            {
                item._isJoin = true; 
            }
            GameEvent.OnAttachToMatrix();
        }
        else
        {
            transform.parent.position = _startPozition;
        }        
    }

    private bool IsFigureOnMatrix(Transform parentPoz)
    {
        bool isJoin = true;

        foreach (var item in parentPoz.transform.GetComponentsInChildren<Control>())
        {
            bool isInside = (
                item.Point.x < _matrix.Gorizontal && 
                item.Point.x >= 0 &&
                item.Point.y < _matrix.Vertical &&
                item.Point.y >= 0);

            var elementWithSameCoordinates = _matrix.controls.Where(x => x.Point.x == item.Point.x && x.Point.y == item.Point.y).ToList();
            if (!(isInside && elementWithSameCoordinates.Count == 0))
            {
                isJoin = false;
            }
        }

        if (isJoin)
        {
            foreach (var item in parentPoz.transform.GetComponentsInChildren<Control>())
            {
                _matrix.controls.Add(item);
            }        
        }

        return isJoin;
    }

    private void CalculateCoordinatesFigureElements()
    {
        foreach (var item in gameObject.transform.parent.GetComponentsInChildren<Control>())
        {
            int x = Mathf.RoundToInt(item.transform.localPosition.x + transform.parent.localPosition.x);
            int y = Mathf.RoundToInt(item.transform.localPosition.y + transform.parent.localPosition.y);
            item._coordinates = new Coordinates(x, y);
        }
    }
}
