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
        PushInner();
	}
    
    private Dictionary<Rigidbody2D, Vector2> insiders = new Dictionary<Rigidbody2D, Vector2>();
    private void PushInner()
    {
        foreach(Rigidbody2D b in insiders.Keys)
        {
            b.AddRelativeForce(insiders[b]);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        //Debug.Log("TRIGGER ENTER");
        Rigidbody2D ob = collider.GetComponent<Rigidbody2D>();
        if (ob == null) return;
        Vector3 pushVec3 = (collider.gameObject.transform.position - gameObject.transform.position);
        Vector2 pushVec = new Vector2(pushVec3.x, pushVec3.y);
        insiders.Add(ob, (pushVec.normalized * ForcePower));
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        //Debug.Log("TRIGGER EXIT");
        Rigidbody2D ob = collider.GetComponent<Rigidbody2D>();

        insiders.Remove(ob);
    }
}
