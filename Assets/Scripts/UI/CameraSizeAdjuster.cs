using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraSizeAdjuster : MonoBehaviour
{
    public float defaultWidth = 1920f;  // The width of your reference resolution
    public float defaultHeight = 1080f; // The height of your reference resolution
    public float defaultOrthographicSize = 5f; // Default camera orthographic size

    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void Start()
    {
        // Adjust the camera's orthographic size based on current screen ratio
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = defaultWidth / defaultHeight;

        if (screenRatio >= targetRatio)
        {
            // If screen is wider than the target ratio
            _camera.orthographicSize = defaultOrthographicSize;
        }
        else
        {
            // If screen is narrower than the target ratio
            float differenceInSize = targetRatio / screenRatio;
            _camera.orthographicSize = defaultOrthographicSize * differenceInSize;
        }
    }
}
