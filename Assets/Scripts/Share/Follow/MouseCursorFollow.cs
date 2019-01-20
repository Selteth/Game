using UnityEngine;

public class MouseCursorFollow : MonoBehaviour {

    private new Transform transform;

    void Awake()
    {
        transform = GetComponent<Transform>();
    }

	void Update () {
        Vector2 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = mousePosition;
	}
}
