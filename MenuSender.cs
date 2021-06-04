using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using Michsky.UI.ModernUIPack;
using UnityEngine.SceneManagement;

public class MenuSender : MonoBehaviour
{
    public Transform toggle;
    public Transform slider;
    public Transform dropdown;

    private Toggle toggleScript;
    private Slider sliderScript;
    private CustomDropdown customDropdown;
    

    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);

        toggleScript = toggle.GetComponent<Toggle>();
        sliderScript = slider.GetComponent<Slider>();
        customDropdown = dropdown.GetComponent<CustomDropdown>();
    }

    public bool returnToggleValue()
    {
        return toggleScript.isOn;
    }

    public float returnSliderValue()
    {
        return sliderScript.value;
    }

    public int returnDropdownValue()
    {
        return customDropdown.selectedItemIndex;
        // 0 is happy, 1 is sad
    }
}
