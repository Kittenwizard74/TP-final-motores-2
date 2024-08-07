using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Piramid_enemy : MonoBehaviour
{
    public enemigos Piramide;
    [SerializeField] GameObject position;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bulletSpawn1;
    [SerializeField] GameObject bulletSpawn2;
    [SerializeField] GameObject bulletSpawn3;
    [SerializeField] GameObject bulletSpawn4;
    [SerializeField] GameObject player;
    [SerializeField] float rotationSpeed;
    [SerializeField] Vector3 rotationAxis = Vector3.up;

    private Player playerscript;
    private float currentHP;
    private float enemyNextShot;
    private bool enemyAlive;
    Experience_manager experience;


    #region base methods
    void Start()
    {
        enemyAlive = true;
        currentHP = Piramide.maxHP;
        player = GameObject.FindWithTag("Player");
        playerscript = GameObject.FindWithTag("Player").GetComponent<Player>();
        experience = GameObject.FindWithTag("XPManager").GetComponent<Experience_manager>();
    }
    private void Update()
    {
        enemyNextShot += Time.deltaTime;
        HandleEnemyShooting();
        HandleEnemyMovement();
        HandleEnemyHealth();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (enemyAlive && other.CompareTag("TPlayer"))
        {
            currentHP -= playerscript.Pdamage;
            //Debug.Log("damaged");
        }
    }
    #endregion

    #region custom methods
    protected virtual void HandleEnemyShooting()
    {
        if (Piramide.firerate < enemyNextShot)
        {
            //shoot bullet
            HandleBulletSpawner1();
            HandleBulletSpawner2();
            HandleBulletSpawner3();
            HandleBulletSpawner4();

            //reset bullet coolddown
            enemyNextShot = 0f;
        }
    }

    protected virtual void HandleEnemyMovement()
    {
        //locate player
        Vector3 movement = player.transform.position - position.transform.position;
        //move to player
        transform.Translate(movement * Piramide.speed * Time.deltaTime, Space.World);

        //calculates rotation based on time
        float rotationAmount = rotationSpeed * Time.deltaTime;
        //apply rotation
        transform.Rotate(rotationAxis, rotationAmount);
    }

    protected virtual void HandleEnemyHealth()
    {
        if (currentHP <= 0)
        {
            enemyAlive = false;
        }

        if (enemyAlive == false)
        {
            experience.AddExperience(100);
            Destroy(gameObject);
            //Debug.Log("enemy killed");
        }
    }

    protected virtual void HandleBulletSpawner1()
    {
        var enemybullet = Instantiate(bullet, bulletSpawn1.transform.position, bullet.transform.rotation);
        enemybullet.GetComponent<Rigidbody>().AddForce(bulletSpawn1.transform.forward * Piramide.bulletSpeed, ForceMode.Impulse);
    }
    protected virtual void HandleBulletSpawner2()
    {
        var enemybullet = Instantiate(bullet, bulletSpawn2.transform.position, bullet.transform.rotation);
        enemybullet.GetComponent<Rigidbody>().AddForce(bulletSpawn2.transform.right * Piramide.bulletSpeed * -1, ForceMode.Impulse);
    }
    protected virtual void HandleBulletSpawner3()
    {
        var enemybullet = Instantiate(bullet, bulletSpawn3.transform.position, bullet.transform.rotation);
        enemybullet.GetComponent<Rigidbody>().AddForce(bulletSpawn3.transform.forward * Piramide.bulletSpeed * -1, ForceMode.Impulse);
    }
    protected virtual void HandleBulletSpawner4()
    {
        var enemybullet = Instantiate(bullet, bulletSpawn4.transform.position, bullet.transform.rotation);
        enemybullet.GetComponent<Rigidbody>().AddForce(bulletSpawn4.transform.right * Piramide.bulletSpeed, ForceMode.Impulse);
    }
    #endregion
}
