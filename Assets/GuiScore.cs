using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiScore : MonoBehaviour {

    private string text;
    private Text textComp;
    private ScoreCounter sc;
	void Start () {
        textComp = gameObject.GetComponent<Text>();
        text = textComp.text;

        foreach(GameObject go in gameObject.scene.GetRootGameObjects())
        {
            sc = go.GetComponent<ScoreCounter>();//)
            if (sc != null)
                break;
        }

	}
	
	// Update is called once per frame
	void Update () {
        textComp.text = text + ((sc!=null)?sc.score.ToString():"0");
	}
}
