using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [System.Serializable]
    public class Settings
    {
        [Header("Movement Settings")]
        public float movementSpeed;

        [Header("Bullet Settings")]
        public float bulletSpeed;
        public GameObject LipsSpawn;
        public GameObject[] lipsPrefabs;
        public float bulletDieTime;
        public float fireRate;
    }
    public Settings settings;

    private float totalTime;

    public float score;
    public float unChangingScore;

    public bool isDead = false;

    void Update()
    {
        float xMove = Input.GetAxisRaw("Horizontal");
        float yMove = Input.GetAxisRaw("Vertical");

        Vector3 moveVector = new Vector3(xMove, yMove, 0.0f);

        if (Input.GetKeyDown(KeyCode.LeftArrow) && totalTime < Time.time)
        {
            Fire(-1);
            totalTime = Time.time + settings.fireRate;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && totalTime < Time.time)
        {
            Fire(1);
            totalTime = Time.time + settings.fireRate;
        }

        moveVector *= settings.movementSpeed;

        this.transform.position += moveVector;

        
    }

    public void Fire(int direction)
    {
        int randomInt = Random.Range(0, 2);

        GameObject bullet = settings.lipsPrefabs[randomInt];
        Vector2 bulletForce = new Vector2(settings.bulletSpeed, 0.0f);

        GameObject bulletInstance = Instantiate(bullet, settings.LipsSpawn.transform.position, Quaternion.identity);
        bulletInstance.GetComponent<Rigidbody2D>().AddForce(bulletForce * direction);

        Destroy(bulletInstance, settings.bulletDieTime);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.tag == "Enemy" || other.collider.tag == "Boss Bullet")
        {
            isDead = true;
            transform.FindChild("catullusFace").gameObject.SetActive(false);
        }
    }
}
