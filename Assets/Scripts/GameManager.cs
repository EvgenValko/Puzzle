using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private MatrixSiting _matrixSiting;
    private static GameManager _instance = null;
    public static GameManager Instance { get { return _instance; } } 
   
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
