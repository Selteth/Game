﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    public float maxRadius = 3.0f;
    public float summTime = 1.0f;

    private float curTime = 0f;
    private float radIncPerSec;
    
    void Awake()
    {
        
        radIncPerSec = maxRadius / summTime;
    }
	
	// Update is called once per frame
	void Update () {
        if (curTime>summTime)
        {
            GameObject.Destroy(gameObject);
            return;
        }
        //Vector3 scale = gameObject.transform.localScale;
        //scale *= radIncPerSec*Time.deltaTime;
        gameObject.transform.localScale += new Vector3(1, 1, 0) * radIncPerSec * Time.deltaTime;
        curTime += Time.deltaTime;
        //gameObject.transform.lossyScale.Set(scale.x, scale.y, scale.z);
	}
}
