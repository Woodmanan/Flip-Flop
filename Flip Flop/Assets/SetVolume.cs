using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{

    public AudioMixer mixer;
    public string volName;
    public Slider slider;

    private void Start()
    {
        slider.value = PlayerPrefs.GetFloat(volName, 0.75f);
        mixer.SetFloat(volName, Mathf.Log10(slider.value = PlayerPrefs.GetFloat(volName, 0.75f)) * 20);
    }

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat(volName, Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat(volName, sliderValue);
    }
}
