using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{

    public float maxHp = 3.0f;
    public float damageFlashTime = 1f;
    public Color damageColor = Color.red;
    public GameObject boom;

    private float currentHealth;
    private Color originalColor;
    private Color originalEmissionColor;
    private float t;
    private bool dead = false;
    private AudioSource audioSource;

    private MeshRenderer[] meshRenderers;



    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHp;
        meshRenderers = GetComponentsInChildren<MeshRenderer>();
        originalColor = meshRenderers[0].material.color;
        originalEmissionColor = meshRenderers[0].material.GetColor("_EmissionColor");
        audioSource = GetComponent<AudioSource>();

        
        if (gameObject.CompareTag("Player"))
        {
            GameController.instance.SetHealth(currentHealth, maxHp);
        } 
        
    }

    public void ReduceHealth(float damage)
    {
        StartCoroutine(DamageFlash());
        currentHealth -= damage;
        audioSource.Play();
        

        
        if (gameObject.CompareTag("Player"))
        {
            GameController.instance.SetHealth(currentHealth, maxHp);
        }
        

        if (currentHealth <= 0 && !dead)
        {
            dead = true;
            if(gameObject.CompareTag("Enemy"))
            {
                GameController.instance.EnemyDestroyed();
            }

            Instantiate(boom, transform.position, new Quaternion());
            Destroy(gameObject);
        }
    }

    private IEnumerator DamageFlash()
    {
        t = damageFlashTime;
        while (t > 0)
        {
            t -= Time.deltaTime;

            Color newColor = Color.Lerp(originalColor, damageColor, t / damageFlashTime);
            Color newEmissionColor = Color.Lerp(originalEmissionColor, damageColor, t / damageFlashTime);

            foreach (MeshRenderer r in meshRenderers)
            {
                r.material.color = newColor;
                r.material.SetColor("_EmissionColor", newEmissionColor);
            }
            yield return null;
        }
        foreach (MeshRenderer r in meshRenderers)
        {
            r.material.color = originalColor;
            r.material.SetColor("_EmissionColor", originalEmissionColor);
        }
    }
}
