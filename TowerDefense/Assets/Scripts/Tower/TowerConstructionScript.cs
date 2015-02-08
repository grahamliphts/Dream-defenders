using UnityEngine;
using System.Collections;

public class TowerConstructionScript : MonoBehaviour 
{
    public ConstructionController constructionController;
    public Transform transform;
    public ConstructionController ConstructionController
    {
        get
        {
            return constructionController;
        }
        set
        {
            constructionController = value;
        }
    }

    public Transform Transform
    {
        get
        {
            return transform;
        }
        set
        {
            transform = value;
        }
    }
}
