using UnityEngine;

public class CameraInfo : MonoBehaviour
{
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
    }

    public Vector2 GetSize()
    {
        float height = 2f * mainCamera.orthographicSize;
        float width = height * mainCamera.aspect;
        return new Vector2(width, height);
    }
}
