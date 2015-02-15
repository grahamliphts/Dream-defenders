using UnityEngine;
using System.Collections;

public class EnemyPoolManager : MonoBehaviour {

    public EnemyScript[] enemies;
    int _index = 0;

    public EnemyScript GetEnemy()
    {
        var enemy = enemies[_index];
        //Debug.Log(_spells[_index]);
        _index++;
        if (_index >= enemies.Length)
            _index = 0;
        return enemy;
    }
}
