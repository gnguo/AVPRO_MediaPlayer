using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class MediaFunctionCtrl : MediaPlayerManager
{
    [Header("캔버스 그룹")]
    [SerializeField]
    private CanvasGroup functionGroup;
    [SerializeField]
    private CanvasGroup playBarGroup;
    [SerializeField]
    private CanvasGroup rateBarGroup;
    [SerializeField]
    private CanvasGroup rangeSliderGroup;

    [Header("GameObject")]
    [SerializeField]
    private GameObject functionGroupObject;
    [SerializeField]
    private GameObject playBarGroupObject;
    [SerializeField]
    private GameObject rateBarGroupObject;
    [SerializeField]
    private GameObject rangeSliderGroupObject;

    [Header("토글")]
    [SerializeField]
    private Toggle speedToggle;
    [SerializeField]
    private Toggle loopToggle;
    [SerializeField]
    private Toggle mirrorToggle;
    [SerializeField]
    private Toggle playPauseToggle;

    [Header("txt")]
    [SerializeField]
    private Text speedEngTxt;
    [SerializeField]
    private Text speedKrTxt;
    [SerializeField]
    private Text loopEngTxt;
    [SerializeField]
    private Text loopKrTxt;
    [SerializeField]
    private Text mirrorEngTxt;
    [SerializeField]
    private Text mirrorKrTxt;
    [SerializeField]
    private Text rangeCheckLowTxt;
    [SerializeField]
    private Text rangeCheckHIghTxt;
    [SerializeField]
    private Text rangeLowTxt;
    [SerializeField]
    private Text rangeHIghTxt;

    [Header("mediaplayerScreen")]
    [SerializeField]
    private RectTransform mediaRect;

    [Header("script")]
    [SerializeField]
    private PlayBarCtrl playBarCtrl;

    [Header("Slider")]
    [SerializeField]
    private RangeSlider rangeCheckSlider = null;
    [SerializeField]
    private RangeSlider rangeSlider = null;

    public bool isLooping =false;
    float duration = 0f;

    private void Start()
    {
        duration = (float)mediaPlayer.Control.GetSeekableTimes().Duration;

        rateBarGroup.alpha = 0;
        rateBarGroup.blocksRaycasts = false;

        rangeSliderGroup.alpha = 0;
        rangeSliderGroup.blocksRaycasts = false;
    }

    private void Update()
    {
        LoopRange();
    }

    public void ChangeToggleGroupColor(Toggle toggle, Text engT, Text krT)
    {
        if (toggle.isOn)
        {
            engT.color = new Color(255.255f, 213 / 255f, 0f);
            krT.color = new Color(255.255f, 213 / 255f, 0f);
        }
        else
        {
            engT.color = Color.white;
            krT.color = Color.white;
        }
    }

    /// <summary>
    /// speedToggle 이벤트함수
    /// </summary>
    public void OnRateGroup()
    {
        ChangeToggleGroupColor(speedToggle, speedEngTxt, speedKrTxt);

        if(speedToggle.isOn)
        {
            rateBarGroup.alpha = 1;
            playBarGroup.alpha = 0;

            rateBarGroup.blocksRaycasts = true;
            playBarGroup.blocksRaycasts = false;
        }

        else
        {
            rateBarGroup.alpha = 0;
            playBarGroup.alpha = 1;

            rateBarGroup.blocksRaycasts = false;
            playBarGroup.blocksRaycasts = true;
        }
    }

    /// <summary>
    /// loopingtoggle 이벤트함수
    /// </summary>
    public void OnLoopingGroup()
    {
        ChangeToggleGroupColor(loopToggle, loopEngTxt, loopKrTxt);
        
        if (loopToggle.isOn)
        {
            playBarCtrl.OnPause();

            rangeSliderGroup.alpha = 1;
            playBarGroup.alpha = 0;

            rangeSliderGroup.blocksRaycasts = true;
            playBarGroup.blocksRaycasts = false;
            functionGroup.blocksRaycasts = false;

            mediaPlayer.Loop = false;

            if (speedToggle.isOn)
            {
                rateBarGroup.alpha = 0;
                playBarGroup.alpha = 0;

                rateBarGroup.blocksRaycasts = false;
                playBarGroup.blocksRaycasts = false;

                speedToggle.isOn = false;
            }
        }
        //looptoggle끄기
        else
        {
            isLooping = false;

            playPauseToggle.isOn  = true;

            loopToggle.isOn = false;
            
            rangeSlider.LowValue = 0;
            rangeSlider.HighValue = 1;

            if(rangeSlider.gameObject.activeSelf)
            
                rangeSlider.gameObject.SetActive(false);

            rangeCheckSlider.LowValue = 0;
            rangeCheckSlider.HighValue = 1;
        }
    }

    public void LoopRange()
    {
        if (isLooping)
        {
            if (playBarCtrl.playbarSlider.value > rangeCheckSlider.HighValue)
            {
                mediaPlayer.Control.Seek(rangeCheckSlider.LowValue * duration);
            }
            if (playBarCtrl.playbarSlider.value < rangeCheckSlider.LowValue)
            {
                mediaPlayer.Control.Seek(rangeCheckSlider.LowValue * duration);
            }
        }
    }

    /// <summary>
    /// loopGroup내부cancel버튼 이벤트함수
    /// </summary>
    public void CancelLoopingBtn()
    {
            if (mediaPlayer == null)
            {
                return;
            }
            loopToggle.isOn = false;

            playPauseToggle.isOn = false;

            //rangeCheckSlider.LowValue = 0;
            //rangeCheckSlider.HighValue = 1;

            rangeSliderGroup.alpha = 0;
            playBarGroup.alpha = 1;

            rangeSliderGroup.blocksRaycasts = false;
            playBarGroup.blocksRaycasts = true;
            functionGroup.blocksRaycasts = true;

            loopToggle.isOn = false;

    }

    /// <summary>
    /// loopgroup 내부 start버튼
    /// </summary>
    public void StartLoopingBtn()
    {
        if (mediaPlayer == null)
        {
            return;
        }
       playBarGroup.alpha = 1;

       playBarGroup.blocksRaycasts = true;

        isLooping = true;

        playPauseToggle.isOn = false;
 
        playBarCtrl.OnPlay();
        
        rangeSlider.gameObject.SetActive(true);

        rangeSliderGroup.alpha = 0;

        rangeSliderGroup.blocksRaycasts = false;
        functionGroup.blocksRaycasts = true;
    }


    public void RangeSliderOnLowValueChange()
    {
        if (mediaPlayer == null)
        {
            return;
        }

        mediaPlayer.Control.SeekFast(rangeCheckSlider.LowValue * duration);

        rangeSlider.LowValue = rangeCheckSlider.LowValue;

        CurTimeText(rangeCheckLowTxt, rangeCheckSlider.LowValue * duration);
        CurTimeText(rangeLowTxt, rangeCheckSlider.LowValue * duration);
    }

    public void RangeSliderOnHighValueChange()
    {
        mediaPlayer.Control.SeekFast(rangeCheckSlider.HighValue * duration);

        rangeSlider.HighValue = rangeCheckSlider.HighValue;

        DurTimeText(rangeCheckHIghTxt, rangeCheckSlider.HighValue * duration);
        DurTimeText(rangeHIghTxt, rangeCheckSlider.HighValue * duration);
    }

    /// <summary>
    /// MirrorToggle 이벤트함수
    /// </summary>
    public void OnMirror()
    {
        ChangeToggleGroupColor(mirrorToggle, mirrorEngTxt, mirrorKrTxt);

        if(mirrorToggle.isOn)
        {
            mediaRect.transform.Rotate(0,180,0);
        }

        else
        {
            mediaRect.transform.Rotate(0, 180, 0);
        }
    }

}
