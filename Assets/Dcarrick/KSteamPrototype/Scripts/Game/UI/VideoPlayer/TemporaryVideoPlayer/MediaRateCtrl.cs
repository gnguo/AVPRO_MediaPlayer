using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MediaRateCtrl : MediaPlayerManager
{
    [SerializeField]
    Toggle toggle0, toggle1, toggle2, toggle3, toggle4, toggle5, toggle6;

    [SerializeField]
    Slider rateSlider;

    int index;

    float rate;



    private void Awake()
    {
         index = 3;
        rateSlider.value = (float)index;

    }


    public void ChangeSpeedSlider()
    {
        rateSlider.value = Mathf.Clamp(rateSlider.value, 1, 8);
        SettingSpeedrate((int)rateSlider.value);

        switch(index)
        {
            case 1:
                toggle0.isOn = true;
                break;
            case 2:
                toggle1.isOn = true;
                break;
            case 3:
                toggle2.isOn = true;
                break;
            case 4:
                toggle3.isOn = true;
                break;
            case 5:
                toggle4.isOn = true;
                break;
            case 6:
                toggle5.isOn = true;
                break;
            case 7:
                toggle6.isOn = true;
                break;
        }
    }

    public void RateToggle()
    {
        if (toggle0.isOn)
        {
            SettingSpeedrate(1);
        }
    }

    public void RateToggle1()
    {
        if (toggle1.isOn)
        {
            SettingSpeedrate( 2);
        }
    }

    public void RateToggle2()
    {
        if (toggle2.isOn)
        {
            SettingSpeedrate(3);
        }
    }

    public void RateToggle3()
    {
        if (toggle3.isOn)
        {
            SettingSpeedrate( 4);
        }
    }

    public void RateToggle4()
    {
        if (toggle4.isOn)
        {
            SettingSpeedrate( 5);
        }
    }

    public void RateToggle5()
    {
        if (toggle5.isOn)
        {
            SettingSpeedrate( 6);
        }
    }

    public void RateToggle6()
    {
        if (toggle6.isOn)
        {
            SettingSpeedrate(7);
        }
    }

    public void SettingSpeedrate( int _index)
    {
        //재생속도 조절하는곳 
        index = _index;

        switch (index)
        {
            case 1:
                rate = 0.25f;
                break;
            case 2:
                rate = 0.5f;
                break;
            case 3:
                rate = 1.0f;
                break;
            case 4:
                rate = 1.25f;
                break;
            case 5:
                rate = 1.5f;
                break;
            case 6:
                rate = 1.75f;
                break;
            case 7:
                rate = 2.0f;
                break;
        }

        mediaPlayer.PlaybackRate =1f*rate;

        rateSlider.value = (float) index;
    }

}
