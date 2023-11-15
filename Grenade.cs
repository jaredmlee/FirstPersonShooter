using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float throwForce = 20f;  
    public float rotationForce = 5f;
    public float explodeTime = 4f;
    public float explosionRadius = 5f;
    public int damage = 60;
    public GameObject explosionFX;
    public LayerMask enemyLayer;
    public LayerMask playerLayer;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Apply the initial throw force.
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);

        // Apply a random rotation for added realism.
        rb.AddTorque(Random.insideUnitSphere * rotationForce, ForceMode.VelocityChange);

        StartCoroutine(explode());

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator explode()
    {
        yield return new WaitForSeconds(explodeTime);
        Instantiate(explosionFX, transform.position, transform.rotation);
        FindObjectOfType<AudioManager>().play("Grenade");
        dealDamage();
        Destroy(gameObject);
    }
    void dealDamage()
    {
        Collider[] enemyColliders = Physics.OverlapSphere(transform.position, explosionRadius, enemyLayer);
        foreach (Collider col in enemyColliders)
        {
            Enemy enemy = col.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.takeDamage(damage);
                //comment above and uncomment below to try having grenade effect.
                //enemy.getHitWithGrenade(gameObject.transform.position);

            }
        }
        Collider[] playerColliders = Physics.OverlapSphere(transform.position, explosionRadius, playerLayer);
        foreach (Collider col in playerColliders)
        {
            Player player = col.GetComponent<Player>();
            if (player != null)
            {
                player.takeDamage(damage);
            }
        }
    }
}
