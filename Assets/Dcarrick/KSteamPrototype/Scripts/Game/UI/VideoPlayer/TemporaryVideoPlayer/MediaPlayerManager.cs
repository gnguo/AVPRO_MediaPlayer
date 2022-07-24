using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RenderHeads.Media.AVProVideo;
using UnityEngine.UI;

public class MediaPlayerManager : MonoBehaviour
{
    [SerializeField]
    protected MediaPlayer mediaPlayer;

    [SerializeField]
    protected AudioOutput audioOutput;

    [SerializeField]
    protected AudioChannelMixer audioChannelMixer;

    public void CurTimeText(Text txt, float cur)
    {
        System.TimeSpan curT = System.TimeSpan.FromSeconds(cur);

        string curSting = string.Format("{0:D2}:{1:D2}", curT.Minutes, curT.Seconds);
        txt.text = curSting;
    }

    public void DurTimeText(Text txt, float dur)
    {
        System.TimeSpan durT = System.TimeSpan.FromSeconds(dur);

        string durSting = string.Format("{0:D2}:{1:D2}", durT.Minutes, durT.Seconds);
        txt.text = durSting;
    }

    public void FadeOutIcon(Image imgs)
    {
        for (float f = 1f; f >= 0; f -= 0.1f)
        {
            Color c = imgs.color;
            c.a = f;
            imgs.color = c;
        }
    }

    //https://www.renderheads.com/content/docs/AVProVideo/articles/usage-loading-media.html
}
