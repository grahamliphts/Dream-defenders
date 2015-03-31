using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
public class LevelColliderScript : MonoBehaviour 
{
    [MenuItem("Window/LevelCollider")]
    static void Init()
    {
        GameObject level = GameObject.Find("Level");
        
        if (level != null)
        {
            GameObject pointManager;
            if((pointManager = GameObject.Find("Level/ManagerPoint")) == null)
            {
                pointManager = new GameObject("ManagerPoint");
                pointManager.transform.parent = level.transform;
                pointManager.AddComponent<PointManager>();
            }

            PointManager manager = pointManager.GetComponent<PointManager>();
            if(manager.point_list == null)
                manager.point_list = new List<GameObject>();
            manager.point_list.Clear();
			for (int i = 0; i < level.transform.childCount; i++)
			{
				Transform child = level.transform.GetChild(i);
				Debug.Log(child.gameObject.name);
				child.gameObject.AddComponent<BoxCollider>();
				if (child.gameObject.name.StartsWith("Point") || child.gameObject.name.StartsWith("spawnEnemy") || child.gameObject.name.StartsWith("nexus") || child.gameObject.name.StartsWith("tpPoint"))
					manager.point_list.Add(child.gameObject);

				if (child.gameObject.GetComponent<Renderer>() != null)
				{
					//Debug.Log(child.gameObject.name);
					//Debug.Log(child.gameObject.renderer.sharedMaterial.name);
					if (child.gameObject.GetComponent<Renderer>().sharedMaterial.name == "ground")
					{
						child.gameObject.tag = "ground";
						child.gameObject.name = "ground";
					}
					else if (child.gameObject.GetComponent<Renderer>().sharedMaterial.name == "wall")
					{
						child.gameObject.tag = "wall";
						child.gameObject.name = "wall";
					}

					else if (child.gameObject.GetComponent<Renderer>().sharedMaterial.name == "cap")
					{
						child.gameObject.tag = "cap";
						child.gameObject.name = "cap";
					}

					else if (child.gameObject.GetComponent<Renderer>().sharedMaterial.name == "wall_inside")
					{
						child.gameObject.tag = "wall_inside";
						child.gameObject.name = "wall_inside";
					}
				}
			}
			for (int i = 0; i < manager.point_list.Count; i++)
			{
				Debug.Log("managerPointList");
				if (i == 0)
					manager.point_list[i].name = "spawnEnemy";
				else if (i == manager.point_list.Count - 1)
					manager.point_list[i].name = "nexus";
				else
					manager.point_list[i].name = "tpPoint";
			}

			GameObject gameManager = GameObject.Find("GameManager");
			gameManager.GetComponent<EnnemyManager>().SpawnEnemy = manager.point_list[0].transform;
			gameManager.GetComponent<EnnemyManager>().ArrivalP = manager.point_list[manager.point_list.Count - 1].transform;
		}
	}
}

