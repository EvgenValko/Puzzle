using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get { return _instance; } } 
    private static GameManager _instance = null;
   
    private Matrix _matrix;

    [SerializeField] private MatrixSiting _matrixSiting;


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
        GameObject backgroundMatrix = Instantiate(_matrixSiting.BackgroundMatrix.gameObject);
        _matrix = backgroundMatrix.gameObject.AddComponent<Matrix>();
        _matrix.Initialize(_matrixSiting);     
    }

    IEnumerator del(List<Control> controls)
    {
        foreach (var item in controls)
        {
            _matrix.controls.Remove(item);
            Destroy(item.gameObject);         
            GameEvent.Line();
            yield return new WaitForSeconds(0.02f);
        }
        controls.Clear();               
    }

    private void deletline()
    {
        List<Control> delet = new List<Control>();

        for (int i = 0; i < _matrix.Gorizontal; i++)
        {
            List<Control> deletVerticalLine = _matrix.controls
                   .Where(x => x.Point.X == i)
                   .OrderByDescending(x => x.Point.Y)
                   .ToList();
            if (deletVerticalLine.Count == _matrix.Vertical)
            {
                delet.AddRange(deletVerticalLine);
            }           
        }

        for (int i = 0; i < _matrix.Vertical; i++)
        {
            List<Control> deletGorizontalLine = _matrix.controls
                    .Where(x => x.Point.Y == i)
                    .OrderBy(x => x.Point.X)
                    .ToList();
            if (deletGorizontalLine.Count == _matrix.Gorizontal)
            {
                delet.AddRange(deletGorizontalLine);
            }
        }

        delet = delet.Distinct().ToList();
        StartCoroutine(del(delet));
    }

    private void OnEnable()
    {
        GameEvent.OnJoin += deletline;
    }

    private void OnDisable()
    {
        GameEvent.OnJoin += deletline;
    }
}
