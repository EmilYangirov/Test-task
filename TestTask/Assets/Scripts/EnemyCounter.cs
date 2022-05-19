using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    [SerializeField]
    private Transform enemiesParent;

    [HideInInspector]
    public List<EnemyCharacter> enemies;

    private void Start()
    {
        GetEnemies();
    }

    private void GetEnemies()
    {
        foreach(Transform tr in enemiesParent)
        {
            enemies.Add(tr.GetComponent<EnemyCharacter>());
        }
    }
}
