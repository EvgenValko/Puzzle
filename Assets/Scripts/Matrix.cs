using UnityEngine;

public class Matrix : MonoBehaviour
{

    [SerializeField] private GameObject _matrixElement;
    [SerializeField] private Transform _backgroundMatrix;
    [SerializeField] private Transform _parent;
    [SerializeField] private int _gorizontalElement;
    private int _verticalElement;
    private float _scale;
    
    public int Gorizontal{ get => _gorizontalElement; }
    public int Vertical { get => _verticalElement; }
    public float Scale { get => _scale; }
    public Transform Parent { get => _parent; }
    
    
    private void Awake()

    {
        CreateMatrix();        
    }

    private void CreateMatrix()
    {
        _parent = Instantiate(_parent,Vector2.zero,Quaternion.identity).transform;        
        _parent.localScale = Vector2.one;

        _scale = _backgroundMatrix.localScale.x / _gorizontalElement;
        _verticalElement = Mathf.FloorToInt(_gorizontalElement / _backgroundMatrix.localScale.x * _backgroundMatrix.localScale.y);
        _backgroundMatrix.localScale = new Vector2(_backgroundMatrix.localScale.x, _verticalElement * _scale);

        for (int i = 0; i < _gorizontalElement; i++)
        {
            for (int j = 0; j < _verticalElement; j++)
            {
               Instantiate(_matrixElement, new Vector2(i, j), Quaternion.identity, _parent);
            }
        }

        _parent.position = (Vector2)(_backgroundMatrix.localScale / 2 - _backgroundMatrix.localScale + _backgroundMatrix.position) + new Vector2(_scale, _scale) / 2;
        _parent.localScale *= _scale;
    }    
}