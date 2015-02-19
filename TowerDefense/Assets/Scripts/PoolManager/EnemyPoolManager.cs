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
}
