using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get { return _instance; } } 
    private static GameManager _instance = null;
    
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
        Matrix matrix = backgroundMatrix.gameObject.AddComponent<Matrix>();
        matrix.Initialize(_matrixSiting);
        LineDestroyer lineDestroyer = backgroundMatrix.AddComponent<LineDestroyer>();
        lineDestroyer.Matrix = matrix;
    }

}
