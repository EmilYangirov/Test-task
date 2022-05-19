using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersPosition : MonoBehaviour
{
    [SerializeField]
    private BrokenLine characterLine; //character placement line

    [SerializeField]
    private Transform chararacterGroup; //parent transform of characters transform

    [HideInInspector]
    public List<Transform> characters; //list of characters transforms


    private void Start()
    {
        GetAllCharacters();
        SetCharactersPositions();
    }

    //find all characters in parent transform
    public void GetAllCharacters()
    {      
        foreach (Transform tr in chararacterGroup)
        {
            characters.Add(tr);
        }
    }

    //give equidistant position to characters
    public void SetCharactersPositions()
    {
        float step = characterLine.lineLength / (characters.Count);
        Debug.Log(step);

        for(int i = 0; i < characters.Count; i++)
        {
            float characterLinePosition = i * step;
            characters[i].transform.position = GetCharacterPosition(characterLinePosition);
        }
    }
    
    private Vector3 GetCharacterPosition(float _characterLinePosition)
    {
        //find extreme position of the desired line
        int partIndex = 0;

        for(int i = 1; i < characterLine.LineElements.Length; i++)
        {
            if(characterLine.LineElements[i].rangeToFirstPoint >= _characterLinePosition)
            {
                partIndex = i;
                break;
            }
        }

        //find segment i-1 -> i
        LineElement firstPointPos;
        LineElement secondPointPos;
        

        if(partIndex != 0)
        {
            firstPointPos = characterLine.LineElements[partIndex - 1];
            secondPointPos = characterLine.LineElements[partIndex];            
        } else
        {
            firstPointPos = characterLine.LineElements[characterLine.LineElements.Length - 1];
            secondPointPos = characterLine.LineElements[partIndex];
        }        


        //find character position in segment   
        float segmentLength = Vector3.Distance(firstPointPos.elementTransform.position, secondPointPos.elementTransform.position);
        float diff = _characterLinePosition - firstPointPos.rangeToFirstPoint;        
        float factor = diff / segmentLength;    
        
        return Vector3.Lerp(firstPointPos.elementTransform.position, secondPointPos.elementTransform.position, factor);
    }
   
}
