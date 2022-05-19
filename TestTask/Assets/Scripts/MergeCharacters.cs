using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeCharacters : MonoBehaviour
{
    private LineRenderer line;
    private Camera mainCamera;
    private bool mouseDrag;
    private int lastCharacter = -1;
    private CharactersPosition characterPosition;

    public List<AliedCharacter> mergedCharacters;

    [HideInInspector]
    public int theBestMerge = 0;

    private void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();
        characterPosition = GetComponent<CharactersPosition>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        MouseController();

        if (mouseDrag)
        {
            GetElementWithMouse();
        }
    }

    private void MouseController()
    {
        if (Input.GetMouseButtonDown(0))
            mouseDrag = true;

        if (Input.GetMouseButtonUp(0))
        {
            mouseDrag = false;

            if(mergedCharacters.Count!=0)
                Merge();

            lastCharacter = -1;
            mergedCharacters.Clear();
            line.positionCount = 0;
        }
    }

    private void GetElementWithMouse()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform.tag == "AlliedCharacter")
        {
            int index = characterPosition.characters.IndexOf(hit.transform);
            AliedCharacter thisCharacter = hit.transform.GetComponent<AliedCharacter>();

            if (!mergedCharacters.Contains(thisCharacter) && CheckCharacter(index,thisCharacter.strength))
            {
                mergedCharacters.Add(thisCharacter);                
                lastCharacter = index;
                UpdateLine();                
            }                
        }
    }

    private bool CheckCharacter(int _index, int strength)
    {
        //if we don't have characters in list
        if (lastCharacter == -1)
            return true;

        //check neighbors
        bool isNeighbor = false;
        
        if (_index == lastCharacter + 1 || _index == lastCharacter - 1)
            isNeighbor = true;

        //check edge points
        if (lastCharacter == 0 && (_index == 1 || _index == characterPosition.characters.Count - 1))
            isNeighbor = true;

        if (lastCharacter == characterPosition.characters.Count - 1 && (_index == 0 || _index == lastCharacter - 1))
            isNeighbor = true;

        //check character strength
        if (strength != mergedCharacters[mergedCharacters.Count - 1].strength || !isNeighbor)
            return false;
        else
            return true;
    }

    private void UpdateLine()
    {
        line.positionCount = mergedCharacters.Count;

        float x = mergedCharacters[mergedCharacters.Count - 1].transform.position.x;
        float y = mergedCharacters[mergedCharacters.Count - 1].transform.position.y + 2;
        float z = mergedCharacters[mergedCharacters.Count - 1].transform.position.z;

        line.SetPosition(mergedCharacters.Count - 1, new Vector3(x,y,z));
    }

    private void Merge()
    {
        int newStrength = mergedCharacters[0].strength * mergedCharacters.Count;
        mergedCharacters[0].ChangeStats(newStrength);

        if (theBestMerge < newStrength)
            theBestMerge = newStrength;

        for (int i = 1; i < mergedCharacters.Count; i++)
        {
            characterPosition.characters.Remove(mergedCharacters[i].transform);
            Destroy(mergedCharacters[i].gameObject);
        }

        characterPosition.SetCharactersPositions();
    }
}
