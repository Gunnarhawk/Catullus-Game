using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    private GameObject player;
    private GameObject gameManagementGO;
    public GameObject bossMouth;
    public GameObject badVibesBullet;

    private PlayerController charController;
    private UserUI userUI;
    private GameManagement gameManager;
    private EnemyAI enemyAI;

    private bool isDead = false;

    public float health = 15f;

    public float damp = 6.0f;

    private float totalTime;

    public float fireRate;

    void Start()
    {
        player = GameObject.Find("Player");
        gameManagementGO = GameObject.Find("Game Manager");

        charController = player.GetComponent<PlayerController>();
        userUI = player.GetComponent<UserUI>();
        gameManager = gameManagementGO.GetComponent<GameManagement>();
        enemyAI = gameManagementGO.GetComponent<EnemyAI>();
    }

    void Update()
    {
        if(!charController.isDead || !isDead)
        {
            Rigidbody2D rb2D = transform.gameObject.GetComponent<Rigidbody2D>();
            Vector2 bossMovePosition = new Vector2(8f, 0f);
            rb2D.MovePosition(bossMovePosition);

            Quaternion rotate = Quaternion.LookRotation(player.transform.position - bossMouth.transform.position);
            Vector2 rotation =  player.transform.position - bossMouth.transform.position;
            bossMouth.transform.rotation = Quaternion.Slerp(bossMouth.transform.rotation, rotate, Time.deltaTime * damp);

            if (totalTime < Time.time && transform.position.x == 8)
            {
                GameObject bullet = Instantiate(badVibesBullet, bossMouth.transform.position, Quaternion.identity);
                bullet.AddComponent<Rigidbody2D>();
                bullet.GetComponent<Rigidbody2D>().gravityScale = 0;
                bullet.GetComponent<Rigidbody2D>().AddForce(rotation.normalized * 150f);
                totalTime = Time.time + fireRate;

                Destroy(bullet, 15.0f);
            }
        }

        if (isDead)
        {
            enemyAI.bossOut = false;

            this.transform.gameObject.GetComponent<SpriteRenderer>().enabled = false;

            Destroy(this.transform.gameObject);
        }
        else
        {
            enemyAI.bossOut = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            health -= 1;

            if (health <= 0)
            {
                isDead = true;
            }

            Destroy(other.transform.gameObject);
        }
    }
}
