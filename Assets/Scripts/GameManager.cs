using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get { return _instance; } }
 
    private static GameManager _instance = null;
    private bool[,] _isElementsOnMatrix;
    private List<Control> _element = new List<Control>();

    [SerializeField] private MatrixSiting matrixSiting;
    [SerializeField] private GameObject[] _figures;
    private Matrix _matrix;
    [SerializeField] private Transform _spawn;

    private void Awake()
    {
        if (_instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    
    void Start()
    {        
        GameObject backgroundMatrix = Instantiate(matrixSiting.BackgroundMatrix.gameObject);
        _matrix = backgroundMatrix.gameObject.AddComponent<Matrix>();
        _matrix.Initialize(matrixSiting);

        _isElementsOnMatrix = new bool[_matrix.Gorizontal, _matrix.Vertical];
        AddFigure();
    }

    public bool IsFigureOnMatrix(Transform parentPoz)
    {
        bool isJoin = true;

        foreach (var item in parentPoz.transform.GetComponentsInChildren<Control>())
        {
            int x = item.Point.X;
            int y = item.Point.Y;
            bool isInside = (x < _matrix.Gorizontal && x >= 0 && y < _matrix.Vertical && y >= 0);

            if (!(isInside && !_isElementsOnMatrix[x, y]))
            {
                isJoin = false;
            }
        }

        if (isJoin)
        {
            foreach (var item in parentPoz.transform.GetComponentsInChildren<Control>())
            {
                _isElementsOnMatrix[item.Point.X, item.Point.Y] = true;
                _element.Add(item);
            }

            AddFigure();
        }

        return isJoin;
    }

    private void AddFigure()
    {
        GameObject go = Instantiate(_figures[Random.Range(0, _figures.Length)], _spawn.position, Quaternion.identity);
        go.transform.localScale *= _matrix.Scale;
        go.transform.parent = _matrix.Parent;
    }

    IEnumerator del(List<Control> controls)
    {
        foreach (var item in controls)
        {
            _isElementsOnMatrix[item.Point.X,item.Point.Y]= false;
        }

        foreach (var item in controls)
        {
            _element.Remove(item);
            item.Delete();
            GameEvent.Line();
            yield return new WaitForSeconds(0.05f);
        }
        controls.Clear();       
        
    }

    private void deletline()
    {
        for (int i = 0; i < _isElementsOnMatrix.GetLength(0); i++)
        {
            List<Control> deletVerticalLine = _element
                   .Where(x => x.Point.X == i)
                   .OrderByDescending(x => x.Point.Y)
                   .ToList();
            if (deletVerticalLine.Count == _isElementsOnMatrix.GetLength(1))
            {
                StartCoroutine(del(deletVerticalLine));
            }           
        }

        for (int i = 0; i < _isElementsOnMatrix.GetLength(1); i++)
        {
            List<Control> deletGorizontalLine = _element
                    .Where(x => x.Point.Y == i)
                    .OrderBy(x => x.Point.X)
                    .ToList();
            if (deletGorizontalLine.Count == _isElementsOnMatrix.GetLength(0))
            {
                StartCoroutine(del(deletGorizontalLine)); 
            }
        }
    }
        private void OnEnable()
    {
        GameEvent.OnJoin += IsLine;
    }

    private void OnDisable()
    {
        GameEvent.OnJoin += IsLine;

    }

    private void IsLine()
    {
        deletline();
    }

}
