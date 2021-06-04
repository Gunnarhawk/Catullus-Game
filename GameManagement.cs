using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{
    public GameObject player;
    public GameObject backgroundImage;
    public GameObject bossPrefab;

    private PlayerController playerController;
    private UserUI userUI;
    private EnemyAI enemyAI;

    public GameObject[] backGrounds;

    public int numBackgrounds;
    public int levelForBoss = 5;

    private int numberOfBosses = 0;

    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        userUI = player.GetComponent<UserUI>();
        enemyAI = transform.GetComponent<EnemyAI>();
        numBackgrounds = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && userUI.pauseMenuEnabled == false)
        {
            userUI.EnablePauseMenu(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && userUI.pauseMenuEnabled == true)
        {
            userUI.EnablePauseMenu(false);
        }

        if (userUI.currentLevel == levelForBoss && numberOfBosses == 0)
        { 
            Vector2 bossSpawnLocation = new Vector2(25f, 0f);

            Instantiate(bossPrefab, bossSpawnLocation, Quaternion.identity);

            numberOfBosses++;
        }

    }

    public void ChangeLevel()
    {
        if (numBackgrounds >= backGrounds.Length)
        {
            numBackgrounds = 0;
        }
        Destroy(GameObject.Find("Background-Image"));
        GameObject background = Instantiate(backGrounds[numBackgrounds], Vector3.zero, Quaternion.identity);
        background.transform.name = "Background-Image";
        numBackgrounds++;

        enemyAI.enemySettings.spawnTime -= .05f;
        enemyAI.enemySettings.moveSpeed += 10f;
    }
}
