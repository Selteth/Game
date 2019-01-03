using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionInner : MonoBehaviour {

    public float ForcePower;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay2D(Collider2D other)
    {
        //push all from center

        Rigidbody2D ob = other.GetComponent<Rigidbody2D>();
        if (ob == null) return;
        Vector3 pushVec3 = (other.gameObject.transform.position - gameObject.transform.position);
        Vector2 pushVec = new Vector2(pushVec3.x, pushVec3.y);
        ob.AddForce(pushVec.normalized * ForcePower);
    }
    
}
