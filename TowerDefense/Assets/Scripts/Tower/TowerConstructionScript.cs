using UnityEngine;
using System.Collections;

public class TowerConstructionScript : MonoBehaviour 
{
    public ConstructionController constructionController;
    public Transform newtransform;
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
            return newtransform;
        }
        set
        {
            newtransform = value;
        }
    }
}
