using UnityEngine;
using System.Collections;

public class EnemyPoolManager : MonoBehaviour {

    public EnemyScript[] enemies;
    private int _index = 0;
	public int index
	{
		get
		{
			return _index;
		}
		set
		{
			_index = value;
		}
	}

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

	public void IncreaseDamageOnEnemies(int value)
	{
		for (int i = 0; i < enemies.Length; i++)
		{
			var damages = enemies[i].monsterLifeManager.damages;
			var damagesBase = enemies[i].monsterLifeManager.damagesBase;
			for(int j = 0; j < enemies[i].monsterLifeManager.damagesBase.Length; j++)
			{
				damages[j].damage = damagesBase[j] + value;
				Debug.Log("newDamage" + damages[j].damage);
			}
		}
	}
}

