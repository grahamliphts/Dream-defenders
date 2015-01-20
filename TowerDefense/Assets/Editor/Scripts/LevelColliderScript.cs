using UnityEngine;
using System.Collections;
using UnityEditor;
public class LevelColliderScript : MonoBehaviour 
{

    [MenuItem("Window/LevelCollider")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        //MyWindow window = (MyWindow)EditorWindow.GetWindow(typeof(MyWindow));
        GameObject level = GameObject.Find("level_generator");
        if (level != null)
        {
            for (int i = 0; i < level.transform.childCount; i++)
            {       
                Transform child = level.transform.GetChild(i);
                MeshCollider collider = child.gameObject.GetComponent<MeshCollider>();
                if (collider != null)
                {   
                    if(child.gameObject.GetComponent<BoxCollider>() == null)
                        child.gameObject.AddComponent("BoxCollider");
                    DestroyImmediate(collider);
                }

                if(child.gameObject.renderer != null)
                {
                    Debug.Log(child.gameObject.renderer.sharedMaterial.name);
                    if (child.gameObject.renderer.sharedMaterial.name == "ground")
                        child.gameObject.tag = "ground";
                    else if (child.gameObject.renderer.sharedMaterial.name == "wall")
                        child.gameObject.tag = "wall";
                    else if (child.gameObject.renderer.sharedMaterial.name == "cap")
                        child.gameObject.tag = "cap";
                    else if (child.gameObject.renderer.sharedMaterial.name == "wall_inside")
                        child.gameObject.tag = "wall_inside";
                }
            }
        }
    }
}
