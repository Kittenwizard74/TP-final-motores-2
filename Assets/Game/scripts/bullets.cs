using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullets : MonoBehaviour
{
    private float lifetime;
    [SerializeField] float maxLifetime;


    void Update()
    {
        if(lifetime >= maxLifetime)
        {
            Destroy(gameObject);
        }

        lifetime += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other != null)
        {
            Destroy(gameObject);
        }
    }
}
