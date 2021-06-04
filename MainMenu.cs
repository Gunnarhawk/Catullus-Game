using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject player;
    private UserUI userUI;
    private PlayerController playerController;


    void Start()
    {
        if(player != null)
        {
            userUI = player.GetComponent<UserUI>();
            playerController = player.GetComponent<PlayerController>();
        }
        else
        {
            player = null;
            userUI = null;
            playerController = null;
        }
        
    }

    public void Play()
    {
        SceneManager.LoadSceneAsync("Main Scene", LoadSceneMode.Single);
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        playerController.isDead = false;
        SceneManager.LoadSceneAsync("Main Scene", LoadSceneMode.Single);
    }

    public void Resume()
    {
        userUI.EnablePauseMenu(false);
    }

    public void ReturnToMenu()
    {
        Destroy(GameObject.Find("Main Menu Event System"));

        SceneManager.LoadSceneAsync("Main Menu", LoadSceneMode.Single);
    }
}
