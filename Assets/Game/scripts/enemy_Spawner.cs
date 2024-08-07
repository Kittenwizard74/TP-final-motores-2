using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_Spawner : MonoBehaviour
{
    #region variables
    [Header("positions")]
    [SerializeField] float posx1;
    [SerializeField] float posx2;
    [SerializeField] float posz1;
    [SerializeField] float posz2;
    private float posX;
    private float posZ;

    [Header("enemy")]
    private int whatEnemy;
    [SerializeField] GameObject[] enemys;
    [SerializeField] float timer;

    [SerializeField] int loop;

    #endregion
    #region basic methods
    void Start()
    {
        StartCoroutine(spawner());
    }

    IEnumerator spawner()
    {
        while (true)
        {
            while (loop < 10)
            {
                //randomise enemy spawn pos
                posX = Random.Range(posx1, posx2);
                posZ = Random.Range(posz1, posz2);

                //randomise enemy spawned
                whatEnemy = Random.Range(0, enemys.Length);

                //spawn enemy, then wait timer to spawn again
                Instantiate(enemys[whatEnemy], new Vector3(posX, 1f, posZ), Quaternion.identity);
                //Debug.Log("enemy spawned");
                yield return new WaitForSeconds(timer);

                loop++;
            }

            yield return new WaitForSeconds(10f);

            loop = 0;
        }
    }
    #endregion
}
