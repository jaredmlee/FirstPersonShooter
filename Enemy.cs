using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 40;
    public int currentHealth = 40;
    public float rotationSpeed = 2f;
    public float attackCoolDown = 0.3f;
    public float moveCoolDown = 0.1f;
    public Transform player;
    public Transform firePoint;
    public GameObject laser;
    public float rotationOffset = 1f;
    public float upDownRotationOffset = 5f;
    public bool allowedToMove = true;
    public bool moveRight = false;
    public bool moveLeft = false;
    public float speed = 2f;
    public float objectDetectionDistance = 2.5f;
    public Transform raycastPoint;
    public AudioSource blastSound;
    public float fieldOfViewAngle = 90f;
    public Transform sightRaycastPoint;
    public float initPosY;
    public int creditDrop = 5;
    public Rigidbody rb;
    public bool isFlamethrower;

    private Player playerScript;
    private float nextAttackTime = 0f;
    private float nextMoveTime = 0f;
    private int rand;
    private bool flamethowerActive = false;
    private GameObject flame;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerScript = player.GetComponent<Player>();
        currentHealth = maxHealth;
        initPosY = transform.position.y;
        int rand = Random.Range(0, 2);
        if (rand == 1)
        {
            attackCoolDown += 0.1f;
            moveCoolDown += 0.1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //look at player

        Vector3 direction = player.transform.position - transform.position;
            float distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance <= 2)
            {
                direction.y = 0f;
            }
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                Quaternion offsetRotation = Quaternion.Euler(upDownRotationOffset, rotationOffset, 0f); // Adjust the offset values as per your desired rotation
                targetRotation *= offsetRotation;
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

        if (isFlamethrower)
        {
            if (canSeePlayer() && distance <= 10 && !flamethowerActive)
            {
                animator.SetBool("FlameThrow", true);
                StartCoroutine(flameThrowerShoot());
            }
            if (flamethowerActive && distance > 18 || !canSeePlayer())
            {
                animator.SetBool("FlameThrow", false);
                Destroy(flame);
                flamethowerActive = false;
            }
            return;
        }

        //fire
        if (Time.time >= nextAttackTime)       {
            if (canSeePlayer())
            {
                attack();
            }
            nextAttackTime = Time.time + 1f / attackCoolDown;
        }
        if (!allowedToMove)
        {
            return;
        }
        if (!canSeePlayer())
        {
            return;
        }
        //move
        if (Time.time >= nextMoveTime)
        {
            rand = Random.Range(0, 2);
            if (rand == 0)
            {
                moveRight = true;
                animator.SetBool("WalkR", true);
            }
            else if ( rand == 1)
            {
                moveLeft = true;
                animator.SetBool("WalkL", true);
            }
            StartCoroutine(stopMove());
            nextMoveTime = Time.time + 1f / moveCoolDown;
        }
        if (moveRight)
        {
            if (!Physics.Raycast(raycastPoint.position+transform.right, transform.right, objectDetectionDistance))
            {
                transform.position += transform.right * speed * Time.deltaTime;
            }
            else
            {
                animator.SetBool("WalkR", false);
            }
        }
        else if (moveLeft)
        {
            if (!Physics.Raycast(raycastPoint.position - transform.right, -transform.right, objectDetectionDistance))
            {
                transform.position -= transform.right * speed * Time.deltaTime;
            }
            else
            {
                animator.SetBool("WalkL", false);
            }
        }
    }

    public void takeDamage(int damage)
    {
        if (isFlamethrower)
        {
            StartCoroutine(delayTurnOffFlame());
        }
        currentHealth -= damage;
        animator.SetTrigger("hit");
        if (currentHealth <= 0)
        {
            die();
        }
    }

    public void getHitWithGrenade(Vector3 explosionPosition)
    {
        rb.freezeRotation = false;
        rb.AddExplosionForce(5f, explosionPosition, 6f, 0f, ForceMode.Impulse);
        die();
    }
    public void die()
    {
        if (isFlamethrower)
        {
            animator.SetBool("FlameThrow", false);
            Destroy(flame);
            flamethowerActive = false;
        }
        player.GetComponent<Player>().credits += creditDrop;
        animator.SetBool("Dead", true);
        StartCoroutine(makeKinematic());
        this.enabled = false;
    }

    IEnumerator makeKinematic()
    {
        yield return new WaitForSeconds(1f);
        rb.useGravity = false;
        gameObject.GetComponent<Collider>().enabled = false;
        rb.isKinematic = true;
        yield return new WaitForSeconds(2f);
        Vector3 currentRotation = transform.localEulerAngles;
        currentRotation.x = 0f;
        currentRotation.z = 0f;
        transform.localEulerAngles = currentRotation;
    }

    public void attack()
    {
        if (playerScript.isDead)
        {
            return;
        }
        animator.SetTrigger("Shoot");
        StartCoroutine(delayAttack());
    }

    IEnumerator delayAttack()
    {
        yield return new WaitForSeconds(0.6f);
        blastSound.enabled = false;
        Instantiate(laser, firePoint.position, firePoint.rotation);
        blastSound.enabled = true;
    }

    IEnumerator stopMove()
    {
        yield return new WaitForSeconds(1.5f);
        moveRight = false;
        moveLeft = false;
        animator.SetBool("WalkR", false);
        animator.SetBool("WalkL", false);
    }

    private bool canSeePlayer()
    {
        Vector3 direction = player.position - sightRaycastPoint.transform.position;
        float angle = Vector3.Angle(direction, sightRaycastPoint.transform.forward);

        if (angle < fieldOfViewAngle * 0.5f)
        {
            RaycastHit hit;
            if (Physics.Raycast(sightRaycastPoint.transform.position, direction, out hit))
            {
                if (hit.collider.gameObject.CompareTag("Player") || hit.collider.gameObject.CompareTag("Turret"))
                {
                    return true;
                }
            }
        }
        return false;
    }

    IEnumerator flameThrowerShoot()
    {
        flamethowerActive = true;
        animator.SetTrigger("Shoot");
        yield return new WaitForSeconds(0.6f);
        flame = Instantiate(laser, firePoint.position, firePoint.rotation, firePoint.transform);
    }

    IEnumerator delayTurnOffFlame()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("FlameThrow", false);
        Destroy(flame);
        flamethowerActive = false;
    }
}
