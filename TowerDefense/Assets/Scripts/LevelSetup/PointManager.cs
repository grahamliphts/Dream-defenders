using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PointManager : MonoBehaviour
{
    public List<GameObject> point_list;
    public GameObject TpParticle;
	private Teleport _teleport;
    void Start()
    {
        for(int i = 0; i < point_list.Count; i++)
        {
            if (i != 0) //on ignore le premier
            {
                point_list[i].AddComponent<SphereCollider>();
                SphereCollider collider = point_list[i].GetComponent<SphereCollider>();
                collider.isTrigger = true;
                collider.radius = 1;
                point_list[i].AddComponent<Teleport>();

                point_list[i].gameObject.tag = "tp";
				_teleport = point_list[i].GetComponent<Teleport>();

				
				if(TpParticle != null)
					GameObject.Instantiate(TpParticle, point_list[i].transform.position, Quaternion.identity);

                if(i%2 == 0)
					_teleport.GetGameObject().transform.Rotate(0, 180, 0, Space.World);

                if (i > 1 && (i % 2) == 0)
					_teleport.prevPoint = point_list[i - 1];

                if (i < point_list.Count - 1 && (i % 2) == 1)
                {
					_teleport.nextPoint = point_list[i + 1];
					_teleport.nextPointDest = point_list[i + 2]; //Destination ennemi
                }
            }
        }
    }

	public Teleport GetTeleport()
	{
		return _teleport;
	}
}
