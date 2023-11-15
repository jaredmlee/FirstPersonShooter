using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flameThrower : MonoBehaviour
{
    public int damage = 20;
    public float attackCoolDown = 1f;

    private bool dealDamageEnemy = false;
    private bool dealDamagePlayer = false;
    private bool dealDamageAnimal = false;
    private float nextAttackTime = 0f;
    private Enemy currEnemy;
    private Player currPlayer;
    private animal currAnimal;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(activateCollider());
        Destroy(gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        if (dealDamageEnemy)
        {
            if (Time.time >= nextAttackTime)
            {
                currEnemy.takeDamage(damage);
                nextAttackTime = Time.time + 1f / attackCoolDown;
            }
        }
        if (dealDamagePlayer)
        {
            if (Time.time >= nextAttackTime)
            {
                currPlayer.takeDamage(damage);
                nextAttackTime = Time.time + 1f / attackCoolDown;
            }
        }
        if (dealDamageAnimal)
        {
            if (Time.time >= nextAttackTime)
            {
                currAnimal.takeDamage(damage);
                nextAttackTime = Time.time + 1f / attackCoolDown;
            }
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.takeDamage(damage);
            currEnemy = enemy;
            dealDamageEnemy = true;
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.takeDamage(damage);
            currPlayer = player;
            dealDamagePlayer = true;
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Animal"))
        {
            animal animal = collision.gameObject.GetComponent<animal>();
            animal.takeDamage(damage);
            currAnimal = animal;
            dealDamageAnimal = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            dealDamageEnemy = false;
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            dealDamagePlayer = false;
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Animal"))
        {
            dealDamageAnimal = false;
        }
    }

    IEnumerator activateCollider()
    {
        yield return new WaitForSeconds(0.4f);
        GetComponent<Collider>().enabled = true;
    }
}
