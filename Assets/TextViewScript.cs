using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextViewScript : MonoBehaviour {

    private Text text;
	// Use this for initialization
	void Start () {
        text = gameObject.GetComponent<Text>();
        text.text = VariableContainer.LastScore.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
