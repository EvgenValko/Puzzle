using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDestroyer : MonoBehaviour
{
    private Matrix _matrix;
    public Matrix  Matrix { set => _matrix = value; }
      
    private void FindLine()
    {
        List<Control> elementsToDestroy = new List<Control>();

        for (int i = 0; i < _matrix.Gorizontal; i++)
        {
            List<Control> elementsVerticalLine = _matrix.controls
                   .Where(x => x.Point.x == i)
                   .OrderByDescending(x => x.Point.y)
                   .ToList();
            if (elementsVerticalLine.Count == _matrix.Vertical)
            {
                elementsToDestroy.AddRange(elementsVerticalLine);
            }
        }

        for (int i = 0; i < _matrix.Vertical; i++)
        {
            List<Control> elementsGorizontalLine = _matrix.controls
                    .Where(x => x.Point.y == i)
                    .OrderBy(x => x.Point.x)
                    .ToList();
            if (elementsGorizontalLine.Count == _matrix.Gorizontal)
            {
                elementsToDestroy.AddRange(elementsGorizontalLine);
            }
        }

        elementsToDestroy = elementsToDestroy.Distinct().ToList();
        StartCoroutine(DestroyLine(elementsToDestroy));
    }

    IEnumerator DestroyLine(List<Control> elementsToDestroy)
    {
        foreach (var item in elementsToDestroy)
        {
            _matrix.controls.Remove(item);
            Destroy(item.gameObject);
            GameEvent.Line();
            yield return new WaitForSeconds(0.02f);
        }
        elementsToDestroy.Clear();
    }

    private void OnEnable()
    {
        GameEvent.OnJoin += FindLine;
    }

    private void OnDisable()
    {
        GameEvent.OnJoin += FindLine;
    }
}
