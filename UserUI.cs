using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserUI : MonoBehaviour
{
    public Transform loadingBar;
    public Transform textPercentage;
    public Transform currentLevelText;

    public Transform pauseMenu;
    public Transform endMenu;

    [Range(0, 100)] public float currentPercentage;
    public float incrementSpeed;

    public bool pauseMenuEnabled = false;

    private PlayerController playerController;
    private GameManagement gameManager;

    [HideInInspector] public float totalScore;

    [HideInInspector] public int currentLevel = 1;

    public float finalScore = 0;

    private GameObject mainMenuEventSystem;
    private MenuSender menuSender;

    public AudioSource[] audioSources;
    private AudioSource mainAudioSource;

    public Transform toggle;
    public Transform slider;

    void Start()
    {
        playerController = this.transform.GetComponent<PlayerController>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManagement>();

        mainMenuEventSystem = GameObject.Find("Main Menu Event System");
        menuSender = mainMenuEventSystem.GetComponent<MenuSender>();

        slider.GetComponent<Slider>().value = menuSender.returnSliderValue();

        toggle.GetComponent<Toggle>().isOn = !menuSender.returnToggleValue();

        mainAudioSource = audioSources[menuSender.returnDropdownValue()];
    }

    void Update()
    {
        if (currentPercentage <= 100)
        {
            currentPercentage += incrementSpeed * Time.deltaTime;
        }

        totalScore = (currentPercentage / 100) + playerController.score;

        loadingBar.GetComponent<Image>().fillAmount = totalScore;
        textPercentage.GetComponent<TextMeshProUGUI>().text = Mathf.FloorToInt(totalScore * 100).ToString() + "%";

        currentLevelText.GetComponent<TextMeshProUGUI>().text = currentLevel.ToString();

        if (totalScore >= 1)
        {
            finalScore += totalScore;
            currentPercentage = 0;
            playerController.score = 0;

            gameManager.ChangeLevel();
            currentLevel++;
        }

        EnableEndMenu(playerController.isDead);

        ChangeAudioSettings();
    }

    public void EnablePauseMenu(bool enable)
    {
        pauseMenuEnabled = enable;
        pauseMenu.gameObject.SetActive(enable);
        if (enable == true)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    void ChangeAudioSettings()
    {
        mainAudioSource.volume = slider.GetComponent<Slider>().value;
        mainAudioSource.gameObject.SetActive(!toggle.GetComponent<Toggle>().isOn);
    }

    public void EnableEndMenu(bool enable)
    {
        if(enable == false)
        {
            if (pauseMenuEnabled == true)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
            return;
        }
        endMenu.gameObject.SetActive(true);

        endMenu.FindChild("Score").GetComponent<TextMeshProUGUI>().text = "Score: " + (Mathf.FloorToInt(finalScore + playerController.unChangingScore) * 100).ToString();
        endMenu.FindChild("Level").GetComponent<TextMeshProUGUI>().text = "Level: " + currentLevel.ToString();

        Time.timeScale = 0;
    }
}
