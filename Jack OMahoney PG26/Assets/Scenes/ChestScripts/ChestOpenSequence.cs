using UnityEngine;
using UnityEngine.Video;

public class ChestOpenSequence : MonoBehaviour
{
    VideoPlayer video;
    BuffManager buffManager;

    void OnEnable()
    {
        video = GetComponentInChildren<VideoPlayer>();
        buffManager = FindFirstObjectByType<BuffManager>();


        video.Play();

        video.loopPointReached += VideoFinished;
    }

    void VideoFinished(VideoPlayer vp)
    {
        buffManager.ShowBuffChoices(this);
    }
}