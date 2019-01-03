using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombMechanism : MonoBehaviour, MechanismInterface {
    private GameObject explObj = null;

    public void Awake()
    {
        
    }

    public void Activate()
    {
        if (explObj == null)
            explObj = Resources.Load<GameObject>("Prefabs/Explosion");

        //Debug.Log(explObj);

        //GameObject expl = 
            GameObject.Instantiate(explObj, gameObject.transform.position, Quaternion.identity);
        
        GameObject.Destroy(gameObject);
    }

}
