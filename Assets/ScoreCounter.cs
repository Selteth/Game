using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ScoreCounter : MonoBehaviour {

    private ScoreTableLogic scl;
    //private VariableContainer vc;

    public int score=0;
    public float LengthToScorePoint = 0.1f;
    private float lengthDone=0f;
    private float prevPosX = 0;
	// Use this for initialization

	void Start () {
        prevPosX = gameObject.transform.position.x;

        foreach(GameObject go in gameObject.scene.GetRootGameObjects())
        {
            //if (scl==null)
                scl = go.GetComponent<ScoreTableLogic>();
            //if (vc == null)
           //     vc = go.GetComponent<VariableContainer>();

            if (scl != null )//&& vc != null)
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {
        lengthDone += gameObject.transform.position.x - prevPosX;
        prevPosX = gameObject.transform.position.x;

        if (lengthDone>LengthToScorePoint)
        {
            int delta = Mathf.FloorToInt(lengthDone / LengthToScorePoint);
            score += delta;
            lengthDone -= LengthToScorePoint * delta;
        }

    }
    void OnDestroy()
    {
        scl.Add(score);
        //Debug.Log("SCORE("+score+") added");
        VariableContainer.LastScore = score;
    }
}
