using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private GameObject player;

    private PlayerController playerController;

    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player") == true)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerController = player.GetComponent<PlayerController>();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Bullet")
        {
            playerController.score += .04f;
            playerController.unChangingScore += .01f;
            Destroy(this.gameObject, 0.0f);
            Destroy(other.transform.gameObject, 0.0f);
        }
    }
}
