using UnityEngine;

public class Control : MonoBehaviour
{
    private bool _isMouseDown = false;
    private Vector2 _startPozition;
    private Point _point;
    private bool _isJoin;

    public Point Point { get => _point; }
    public bool IsJoin { get => _isJoin; }
   
    void Update()
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

        foreach (var item in gameObject.transform.parent.GetComponentsInChildren<Control>())
        {          
            item._point = new Point();
            item._point.X = Mathf.RoundToInt(item.transform.localPosition.x + transform.parent.localPosition.x);
            item._point.Y = Mathf.RoundToInt(item.transform.localPosition.y + transform.parent.localPosition.y);
        }

        if (GameManager.Instance.IsFigureOnMatrix(gameObject.transform.parent))
        {
            transform.parent.localPosition = new Vector2(Mathf.Round(transform.parent.localPosition.x), Mathf.Round(transform.parent.localPosition.y));
            foreach (var item in gameObject.transform.parent.GetComponentsInChildren<Control>())
            {
                item._isJoin = true; 
            }
            GameEvent.Join();
        }

        else
        {
            transform.parent.position = _startPozition;
        }        
    }

    public void Delete()
    {
        Destroy(gameObject);
    }
}
