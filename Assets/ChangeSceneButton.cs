using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeSceneButton : MonoBehaviour {

    public Button SomeButton;
    public string SceneName;

	void Start () {
        SomeButton.onClick.AddListener(GoTo);
	}
	
    void GoTo()
    {
        SceneManager.LoadScene(SceneName);
    }
}
