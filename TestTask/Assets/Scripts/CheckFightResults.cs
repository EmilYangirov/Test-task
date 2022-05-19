using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckFightResults : MonoBehaviour
{
    private EnemyCounter enemyCounter;
    private MergeCharacters characters;

    [SerializeField]
    private TextMeshProUGUI resultText;

    private void Start()
    {
        GameObject charactersMainGo = GameObject.FindGameObjectWithTag("Characters");
        enemyCounter = charactersMainGo.GetComponent<EnemyCounter>();
        characters = charactersMainGo.GetComponent<MergeCharacters>();
    }

    public void CheckResults()
    {
        int theBestOne = characters.theBestMerge;
        bool defeat = false;

        foreach (EnemyCharacter enemy in enemyCounter.enemies)
        {
            if (theBestOne > enemy.strength)
                theBestOne -= enemy.attack;
            else
                defeat = true;
        }

        if (!defeat)
            resultText.text = "Congratulations! you are a winner =)";
        else
            resultText.text = "Unfortunately you lost =(";
    }
}
