using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private Text _text;
    private int _score;

    private void Start()
    {
        _text.text = _score.ToString();
    }
    private void OnEnable()
    {
        GameEvent.OnLine += WriteScore; 
    }
    private void OnDisable()
    {
        GameEvent.OnLine -= WriteScore;
    }
    private void WriteScore()
    {
        _score += 10;
        _text.text = _score.ToString();
    }
}
