using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;

    public Collider area;

    public float range;

    public GameObject spawnPlayer()
    {
        return Spawn(player);
    }

    public GameObject spawnEnemy()
    {
        return Spawn(enemy);
    }

    private GameObject Spawn(GameObject obj)
    {
        Vector3 minPoint = area.bounds.min;
        Vector3 maxPoint = area.bounds.max;

        float randomx = Random.Range(minPoint.x, maxPoint.x);
        float randomz = Random.Range(minPoint.z, maxPoint.z);

        Vector3 position = new Vector3(randomx, maxPoint.y, randomz);
        Collider[] colliders = Physics.OverlapSphere(position, range);
        
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Obstacle"))
            {
                Destroy(colliders[i].gameObject);
            }
        }


        GameObject newObj = Instantiate(obj, position, new Quaternion());
        return newObj;
    }


    
}
