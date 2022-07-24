using RenderHeads.Media.AVProVideo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayBarCtrl : MediaPlayerManager
{
    [Header("슬라이더")]
    //public Slider playSlider = null;
    [SerializeField]
    private Slider volumeSlider = null;

    public UICustomSlider playbarSlider = null;


    [Header("버튼")]
    [SerializeField]
    private Button prewBtn;
    [SerializeField]
    private Button nextBtn;

    [Header("토글")]
    public Toggle onOff_Toggle;
    [SerializeField]
    private Toggle volumeToggle;

    [Header("gameobj")]
    [SerializeField]
    private GameObject[] onOffImg;
    [SerializeField]
    private GameObject[] volumeImgs;

    [Header("txt")]
    [SerializeField]
    private Text curPlayTxt;
    [SerializeField]
    private Text entirePlayTxt;

    private float duration;
    private float current;

    [Header("아이콘")]
    public Image[] iconImgs;

    private void Start()
    {
        volumeSlider.value = mediaPlayer.AudioVolume;

        if (onOff_Toggle.isOn)
        {
            mediaPlayer.AutoStart = false;
        }

        else
        {
            mediaPlayer.AutoStart = true;
        }
    }

    private void OnEnable()
    {
        duration = (float)mediaPlayer.Control.GetSeekableTimes().Duration;
    }

    private void Update()
    {
        volumeSlider.onValueChanged.AddListener((value) =>
        {
            mediaPlayer.AudioVolume = value;
        });

        //재생중인시간
        current = (float)mediaPlayer.Control.GetCurrentTime();

        playbarSlider.SetValueWithoutNotify (current / duration);
        
        CurTimeText(curPlayTxt,current);

        DurTimeText(entirePlayTxt, duration);
    }


    public void PlaybarValueChange()
    {
        mediaPlayer.Control.SeekFast(playbarSlider.value * duration);
    }

    /// <summary>
    /// 일시정지, 재생 토글이벤트함수
    /// </summary>
    public void PausePlayMediaPlay()
    {
        if (onOff_Toggle.isOn)
        {
            OnPause();        
        }

        else
        {
            OnPlay();        
        }
    }

    public void OnPause()
    {
        mediaPlayer.Pause();

        onOffImg[0].SetActive(true);
        onOffImg[1].SetActive(false);
    }

    public void OnPlay()
    {
        mediaPlayer.Control.Play();

        onOffImg[1].SetActive(true);
        onOffImg[0].SetActive(false);

    }
    /// <summary>
    /// 15초 앞으로 넘기기
    /// </summary>
    public void NextView()
    {
        float nextT = current+ 15f;


        if (nextT >= duration)
        {
            nextT = duration;
        }
        mediaPlayer.Control.Seek(nextT);
    }

    /// <summary>
    /// 15초 전으로 이동
    /// </summary>
    public void PrewVIew()
    {
        float prewT = current - 15f;

        if(prewT <= 0)
        {
            prewT = 0;
        }

        mediaPlayer.Control.Seek(prewT);
    }

    /// <summary>
    /// 볼륨뮤트onoff
    /// </summary>
    public void OnOffMuteToggle()
    {
        if (volumeToggle.isOn)
        {
            OnMute();
        }

        else if (!volumeToggle.isOn)
        {
            OffMute();
        }
    }

    void OnMute()
    {
        mediaPlayer.AudioMuted = true;

        volumeSlider.gameObject.SetActive(false);

        volumeImgs[0].SetActive(true);
        volumeImgs[1].SetActive(false);
    }

    void OffMute()
    {
        mediaPlayer.AudioMuted = false;

        volumeSlider.gameObject.SetActive(true);

        volumeImgs[1].SetActive(true);
        volumeImgs[0].SetActive(false);
    }
}
