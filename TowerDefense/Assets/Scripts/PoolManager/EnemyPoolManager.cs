using UnityEngine;
using System.Collections;

public class EnemyPoolManager : MonoBehaviour {

    public EnemyScript[] enemies;
    int _index = 0;

    public EnemyScript GetEnemy()
    {
        var enemy = enemies[_index];
        _index++;

        if (_index >= enemies.Length)
            Debug.Log("No more enemies");
        return enemy;
    }

    public int NumberEnemiesActive()
    {
        int found = 0;
        for(int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].gameObject.activeSelf == true)
                found++;
        }
        return found;
    }

    public void ResetIndex()
    {
        _index = 0;
    }
}

