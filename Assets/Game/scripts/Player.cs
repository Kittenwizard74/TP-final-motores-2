using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent (typeof(Collider))]
public class Player : MonoBehaviour
{
    #region variables
    Rigidbody rb;
    
    [Header("Movement")]
    public float Mvelocity;         //public bc UI && lvlup

    [Header("Health")]
    public float CurrentHP;         //public bc UI
    public float MaxHP;
    private float MinHP;

    private bool alive;

    [Header("Shooting")]
    [SerializeField] Transform muzzle;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float shotSpeed;

    public float fireRate;                  //public bc lvlup
    private float nextShot;

    public float Pdamage = 5;           //public bc UI && enemy DMG && lvlup

    Experience_manager experience;

    #endregion
    #region base methods
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        alive = true;
        experience = GameObject.FindWithTag("XPManager").GetComponent<Experience_manager>();
    }

    private void Update()
    {
        HandleHealth();
        nextShot += Time.deltaTime;

        if(alive == true)
        {
            HandleMovement();
            HandleShooting();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //damage by cube
        if(alive == true && other.gameObject.CompareTag("CubeEnemy"))
        {
            CurrentHP -= other.GetComponent<Cube_enemy>().cubo.damage;
            //Debug.Log("Player took damage");
        }

        //damage by piramid
        if (alive == true && other.gameObject.CompareTag("PiramidEnemy"))
        {
            CurrentHP -= other.GetComponent<Piramid_enemy>().Piramide.damage;
        }

        //damage by little cube
        if (alive == true && other.gameObject.CompareTag("LilCubeEnemy"))
        {
            CurrentHP -= other.GetComponent<LilCube_enemy>().cubito.damage;
            //Debug.Log("Player took damage");
        }

        //damage by shot
        if (alive == true && other.gameObject.CompareTag("EnemyBullet"))
        {
            CurrentHP -= other.GetComponent<enemy_Bullet>().EnemyBulletDamage;
        }
    }

    private void OnEnable()
    {
        //sub event
        //experience.OnExperienceChange += HandleExperienceChange;
    }

    private void OnDisable()
    {
        //unsub event
        //experience.OnExperienceChange -= HandleExperienceChange;
    }
    #endregion
    #region custom methods
    protected virtual void HandleMovement()
    {
        //get movement axis
        forwardinput = Input.GetAxis("Vertical");
        strafeinput = Input.GetAxis("Horizontal");

        //calculate wanted position
        Vector3 movement = new Vector3(strafeinput, 0f, forwardinput).normalized;

        //move to position
        transform.Translate(movement * Mvelocity * Time.deltaTime, Space.World);
    }

    protected virtual void HandleShooting()
    {
        if (fireRate < nextShot)
        {
            //shoot bullet
            var bullet = Instantiate(bulletPrefab, muzzle.position, bulletPrefab.transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(muzzle.transform.forward * shotSpeed, ForceMode.Impulse);

            //reset bullet coolddown
            nextShot = 0f;
        }
    }

    protected virtual void HandleHealth()
    {
        if(CurrentHP < MinHP)
        {
            alive = false;
            Destroy(gameObject);
            SceneManager.LoadScene(0);
        }
    }
    #endregion
    #region gets
    private float forwardinput;
    public float Forwardinput
    {
        get { return forwardinput; }
    }

    private float strafeinput;
    public float Strafeinput
    {
        get { return strafeinput; }
    }
    #endregion

}
