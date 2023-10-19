using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Assign your VideoPlayer components in the Inspector.

    private void Start()
    {
        videoPlayer.Play();
    }
}

