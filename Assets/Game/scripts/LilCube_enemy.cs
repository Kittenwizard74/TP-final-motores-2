using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class LilCube_enemy : MonoBehaviour
{
    public enemigos cubito;
    [SerializeField] private GameObject position;
    [SerializeField] private GameObject player;
    private Player playerscript;
    Experience_manager experience;

    private float currentHP;
    private bool enemyAlive;

    #region base methods
    void Start()
    {
        enemyAlive = true;
        currentHP = cubito.maxHP;
        player = GameObject.FindWithTag("Player");
        playerscript = GameObject.FindWithTag("Player").GetComponent<Player>();
        experience = GameObject.FindWithTag("XPManager").GetComponent<Experience_manager>();
    }
    private void Update()
    {
        HandleEnemyMovement();
        HandleEnemyHealth();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (enemyAlive && other.CompareTag("TPlayer"))
        {
            currentHP -= playerscript.Pdamage;
            //Debug.Log("damaged for: " + playerscript.Pdamage);
        }
    }
    #endregion

    #region custom methods
    protected virtual void HandleEnemyMovement()
    {
        //locate player
        Vector3 movement = player.transform.position - position.transform.position;
        //move to player
        transform.Translate(movement * cubito.speed * Time.deltaTime, Space.World);
    }

    protected virtual void HandleEnemyHealth()
    {
        if (currentHP <= 0)
        {
            enemyAlive = false;
        }

        if (enemyAlive == false)
        {
            experience.AddExperience(10);
            Destroy(gameObject);           
            //Debug.Log("enemy killed");
        }
    }
    #endregion
}
