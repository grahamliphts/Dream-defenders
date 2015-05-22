using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
public class LevelColliderScript : EditorWindow
{
	Object tpPrefab;
	Object sablierPrefab;
	string nameLevel;

    // Add menu item named "My Window" to the Window menu
	[MenuItem("Window/LevelCollider")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
		EditorWindow.GetWindow(typeof(LevelColliderScript));
    }
    
    void OnGUI()
    {
        GUILayout.Label ("Settings", EditorStyles.boldLabel);
		EditorGUILayout.BeginVertical();
		nameLevel = EditorGUILayout.TextField("Name of Level", nameLevel);
		tpPrefab = EditorGUILayout.ObjectField("Prefab Tp", tpPrefab, typeof(Object), true);
		sablierPrefab = EditorGUILayout.ObjectField("Prefab Nexus", sablierPrefab, typeof(Object), true);
		EditorGUILayout.EndVertical();

		if (GUILayout.Button("OK"))
			Init(sablierPrefab, tpPrefab, nameLevel);
		
    }

    static void Init(Object sablierPrefab, Object tpPrefab, string nameLevel)
    {
        GameObject level = GameObject.Find(nameLevel);
        if (level != null)
        {
			//Add the point Manager with script
            GameObject pointManager;
            if((pointManager = GameObject.Find("Level/ManagerPoint")) == null)
            {
                pointManager = new GameObject("ManagerPoint");
                pointManager.transform.parent = level.transform;
                pointManager.AddComponent<PointManager>();
            }

			//Get pointManager
            PointManager manager = pointManager.GetComponent<PointManager>();
            if(manager.point_list == null)
                manager.point_list = new List<GameObject>();
            manager.point_list.Clear();

			for (int i = 0; i < level.transform.childCount; i++)
			{
				Transform child = level.transform.GetChild(i);
				child.gameObject.AddComponent<BoxCollider>();
				if (child.gameObject.name.StartsWith("Point"))
				{
					manager.point_list.Add(child.gameObject);
					child.gameObject.tag = "tp";
				}
				if (child.gameObject.GetComponent<Renderer>() != null)
				{
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

			GameObject sablier = null;
			for (int i = 0; i < manager.point_list.Count; i++)
			{
				if (i == 0)
					manager.point_list[i].name = "spawnEnemy";
				else if (i == manager.point_list.Count - 1)
				{
					manager.point_list[i].name = "nexus";
					sablier = PrefabUtility.InstantiatePrefab(sablierPrefab) as GameObject;

					Vector3 sablierPosition = manager.point_list[i].transform.position;
					sablierPosition.y -= 1;
					sablier.transform.position = sablierPosition; 
				}
					
				else
				{
					manager.point_list[i].name = "tpPoint";
					GameObject tpPoint = PrefabUtility.InstantiatePrefab(tpPrefab) as GameObject;
					tpPoint.transform.position = manager.point_list[i].transform.position;
				}
			}

			GameObject gameManager = GameObject.Find("GameManager");

			Transform nexusPos = manager.point_list[manager.point_list.Count - 1].transform;
			gameManager.GetComponent<EnnemyManager>().SpawnEnemy = manager.point_list[0].transform;
			gameManager.GetComponent<EnnemyManager>().ArrivalP = nexusPos;
			EditorUtility.SetDirty(gameManager.GetComponent<EnnemyManager>());
			gameManager.GetComponent<LoopManager>().LifeNexus = sablier.GetComponent<NexusLife>();

			EditorUtility.SetDirty(gameManager.GetComponent<LoopManager>());

			GameObject guiManager = GameObject.Find("GuiInGameManager");

			guiManager.GetComponent<LifeBarManager>().LifeNexus = sablier.GetComponent<NexusLife>();
			guiManager.GetComponent<NbFloorScript>().SetPointManager(manager);

			EditorUtility.SetDirty(guiManager.GetComponent<LifeBarManager>());
			EditorUtility.SetDirty(guiManager.GetComponent<NbFloorScript>());

		}
	}
}

