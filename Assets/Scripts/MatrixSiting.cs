using UnityEngine;

[CreateAssetMenu]
public class MatrixSiting : ScriptableObject
{
    [SerializeField] private GameObject _matrixElement;
    [SerializeField] private Transform _backgroundMatrix;
    [SerializeField] private Transform _parent;
    [SerializeField] private int _gorizontalElement;
    [SerializeField] private GameObject[] _figures;
    [SerializeField] private Transform[] _spawnPozition;

    public GameObject MatrixElement { get => _matrixElement; }
    public Transform BackgroundMatrix { get => _backgroundMatrix; }
    public Transform Parent { get => _parent; }
    public int GorizontalElement { get => _gorizontalElement; }
    public GameObject[] Figures { get => _figures; }
    public Transform[] SpawnPozition { get => _spawnPozition; }
}
