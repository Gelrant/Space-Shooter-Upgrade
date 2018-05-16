using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour {

    public Slider mySlider;

    public void OnValueChanged()
    {
        AudioListener.volume = mySlider.value;
    }

    public void changeQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    public void setFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}
