using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Collider))]
public class Cube_enemy : MonoBehaviour
{
    public enemigos cubo;
    [SerializeField] private GameObject position;
    [SerializeField] private GameObject player;
    private Player playerscript;

    private float currentHP;
    private bool enemyAlive;
    Experience_manager experience;

    #region base methods
    void Start()
    {
        enemyAlive = true;
        currentHP = cubo.maxHP;
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
            //Debug.Log("damaged");
        }
    }
    #endregion

    #region custom methods
    protected virtual void HandleEnemyMovement()
    {
        //locate player
        Vector3 movement = player.transform.position - position.transform.position;
        //move to player
        transform.Translate(movement * cubo.speed * Time.deltaTime, Space.World);
    }

    protected virtual void HandleEnemyHealth()
    {
        if (currentHP <= 0)
        {
            enemyAlive = false;
        }

        if (enemyAlive == false)
        {
            experience.AddExperience(50);
            Destroy(gameObject);
            //Debug.Log("enemy killed");
        }
    }
    #endregion
}
