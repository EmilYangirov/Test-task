using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenLine : MonoBehaviour
{
    private enum LineTypes
    {
        linear,
        loop
    }

    [SerializeField]
    private LineTypes lineType;

    public LineElement[] LineElements;

    public float lineLength;

    private void Awake()
    {
        GetLinesLengths();
    }

    //Draw line in inspector
    private void OnDrawGizmos()
    {
        if (LineElements == null || LineElements.Length < 2)
            return;

        for (int i = 1; i < LineElements.Length; i++)
        {
            Gizmos.DrawLine(LineElements[i - 1].elementTransform.position, LineElements[i].elementTransform.position);
        }

        if (lineType == LineTypes.loop)
            Gizmos.DrawLine(LineElements[0].elementTransform.position, LineElements[LineElements.Length - 1].elementTransform.position);
    }

    //Get length of all line path and length between start point and line parts
    private void GetLinesLengths()
    {
        for (int i = 1; i < LineElements.Length; i++)
        {           
            lineLength += Vector3.Distance(LineElements[i - 1].elementTransform.position, LineElements[i].elementTransform.position);
            LineElements[i].rangeToFirstPoint = lineLength;

            if (lineType == LineTypes.loop && i == LineElements.Length-1)
                lineLength += Vector3.Distance(LineElements[i].elementTransform.position, LineElements[0].elementTransform.position);
        }
    }    
}

//information about line point
[System.Serializable]
public class LineElement
{
    public Transform elementTransform;
    public float rangeToFirstPoint;

    public LineElement(Transform _elementTransform, float _rangeToFirstPoint)
    {
        elementTransform = _elementTransform;
        rangeToFirstPoint = _rangeToFirstPoint;
    }
}
