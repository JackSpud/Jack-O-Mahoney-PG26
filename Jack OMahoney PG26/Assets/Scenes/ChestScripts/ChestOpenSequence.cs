using UnityEngine;
using UnityEngine.Video;

public class ChestOpenSequence : MonoBehaviour
{
    public VideoPlayer video;
    public BuffManager buffManager;

    void OnEnable()
    {
        video.Play();

        video.loopPointReached += VideoFinished;
    }

    void VideoFinished(VideoPlayer vp)
    {
        buffManager.ShowBuffChoices(this);
    }
}