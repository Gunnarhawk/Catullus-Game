using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuAudio : MonoBehaviour
{
    public Transform slider;
    public Transform toggle;

    private Slider sliderScript;
    private Toggle toggleScript;

    void Start()
    {
        sliderScript = slider.GetComponent<Slider>();
        toggleScript = toggle.GetComponent<Toggle>();
    }

    void Update()
    {
        AudioSource audioSource = this.transform.gameObject.GetComponent<AudioSource>();

        audioSource.volume = sliderScript.value;
        audioSource.mute = !toggleScript.isOn;
    }
}
