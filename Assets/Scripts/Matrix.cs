using UnityEngine;
using System.Collections.Generic;

public class Matrix : MonoBehaviour
{

    private GameObject _matrixElement;
    private Transform _backgroundMatrix;
    private Transform _parent;
    private int _gorizontalElement;
    private int _verticalElement;
    private float _scale;
    private GameObject[] _figuresPrefab;
    private Transform[] _spawnPozition;
    private List<GameObject> _figures = new List<GameObject>(); 

    public List<Control> controls = new List<Control>();       
    public int Gorizontal{ get => _gorizontalElement; }
    public int Vertical { get => _verticalElement; }
    
    public void Initialize(MatrixSiting matrixSiting)
    {
        _matrixElement = matrixSiting.MatrixElement;     
        _parent = matrixSiting.Parent;
        _gorizontalElement = matrixSiting.GorizontalElement;
        _figuresPrefab = matrixSiting.Figures;
        _spawnPozition = matrixSiting.SpawnPozition;
        CreateMatrix();
    }

    private void CreateMatrix()
    {
        _backgroundMatrix = transform;

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

        AddFigure();
    }

    private void AddFigure()
    {
        foreach (var spawnPoz in _spawnPozition)
        {
            GameObject go = Instantiate(_figuresPrefab[Random.Range(0, _figuresPrefab.Length)], spawnPoz.position, Quaternion.identity);
            go.transform.localScale *= _scale;
            go.transform.parent = _parent;

            foreach (var item in go.transform.GetComponentsInChildren<Control>())
            {
                item._matrix = this;
            }
            _figures.Add(go);
        }
    }

    private void RemuveFigure()
    {
        if (_figures.Count > 0)
        {
            _figures.RemoveAt(_figures.Count - 1);          
        }
        if(_figures.Count == 0)
        {
            AddFigure();
        }  
    }

    private void OnEnable()
    {
        GameEvent.AttachToMatrix += RemuveFigure;
    }

    private void OnDisable()
    {
        GameEvent.AttachToMatrix -= RemuveFigure;
    }
}