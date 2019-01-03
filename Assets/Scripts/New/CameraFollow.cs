using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followed;

    private void Update()
    {
        gameObject.transform.position = new Vector3(
            followed.position.x,
            followed.position.y,
            gameObject.transform.position.z);
    }
}
