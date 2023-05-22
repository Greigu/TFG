using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    private Slider sliderVolume;
    private new AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = GameObject.Find("BackgroundSound").GetComponent<AudioSource>();
        sliderVolume = GameObject.Find("SliderVolume").GetComponent<Slider>();
        sliderVolume.value = audio.volume;
        sliderVolume.onValueChanged.AddListener(delegate { UpdateVolume(); });
    }

    public void UpdateVolume()
    {
        audio.volume = sliderVolume.value;
        Debug.Log(sliderVolume.value);
        PlayerPrefs.SetFloat("Volume", audio.volume);
    }

}
