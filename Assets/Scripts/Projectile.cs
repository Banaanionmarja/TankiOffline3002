using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Apple;

public class Projectile : MonoBehaviour
{

    public float speed;
    public float lifeTime;
    public float radius;
    public float damage = 1f;

    public string shooterTag;
    public GameObject Boom;

    private Rigidbody rb;
    private float t;

    void Start()
    {
        t = lifeTime;
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;

    }

    void Update()
    {
        t -= Time.deltaTime;
        if ( t <= 0 )
        {
            Kaboom();
        }
    }

    private void Kaboom()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        for ( int i = 0; i < colliders.Length; i++)
        {
            Health health = colliders[i].GetComponent<Health>();
            
            if (health != null)
            {
                health.ReduceHealth(damage); 
            }

        }   
        Instantiate(Boom, transform.position, new Quaternion());

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Muzzle") && !other.CompareTag(shooterTag))
        {
            Kaboom();
        }
    }
}

