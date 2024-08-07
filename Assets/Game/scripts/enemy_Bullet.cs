using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_Bullet : MonoBehaviour
{
    private float lifetime;
    private float CurrentshotLife;
    [SerializeField] float shotLife;
    [SerializeField] float maxLifetime;

    public float EnemyBulletDamage;

    void Update()
    {
        if (lifetime >= maxLifetime || shotLife <= CurrentshotLife)
        {
            Destroy(gameObject);
        }

        lifetime += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.CompareTag("Player") || other.CompareTag("Geometry"))
        {
            Destroy(gameObject);
        }

        if (other!= null && other.CompareTag("TPlayer"))
        {
            CurrentshotLife++;
        }
    }
}
