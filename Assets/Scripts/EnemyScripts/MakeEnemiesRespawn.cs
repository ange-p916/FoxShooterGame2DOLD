using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class MakeEnemiesRespawn : MonoBehaviour
{

    public List<EnemyBehaviourTemplate> enemies = new List<EnemyBehaviourTemplate>();

    void Start()
    {
        enemies = FindObjectsOfType<EnemyBehaviourTemplate>().OrderBy(e => e.gameObject.name).ToList();
        //SpawnEnemies();
    }

    public void SpawnEnemies()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (!enemies[i].gameObject.activeInHierarchy)
            {
                enemies[i].gameObject.transform.position = enemies[i].startPosition;
                enemies[i].gameObject.SetActive(true);
                enemies[i].SetEnemyHealth();
                //Debug.Log("respawning when dead");
            }
            if (enemies[i].gameObject.activeInHierarchy)
            {
                //Debug.Log("respawning when alive");
                enemies[i].gameObject.transform.position = enemies[i].startPosition;
                enemies[i].SetEnemyHealth();
            }
        }
    }
}