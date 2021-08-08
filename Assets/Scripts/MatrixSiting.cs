using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MatrixSiting : ScriptableObject
{
    [SerializeField] private GameObject _matrixElement;
    [SerializeField] private Transform _backgroundMatrix;
    [SerializeField] private Transform _parent;
    [SerializeField] private int _gorizontalElement;

    public GameObject MatrixElement { get => _matrixElement; }
    public Transform BackgroundMatrix { get => _backgroundMatrix; }
    public Transform Parent { get => _parent; }
    public int GorizontalElement { get => _gorizontalElement; }
}
