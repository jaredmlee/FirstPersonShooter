using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public HealthBar energyBar;
    public GameObject laser;
    public Animator animator;
    public Animator armsAnim;
    public float fireSpeed = 1.5f;
    public int maxEnergy = 100;
    public int currentEnergy = 100;
    public int energyCost = 1;
    public GameObject message;
    public Text messageText;
    public Player player;
    public bool isEnergyGun;
    public bool isPistol;

    private float nextAttackTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        //currentEnergy = maxEnergy;
        energyBar = GameObject.FindGameObjectWithTag("EnergyBar").GetComponent<HealthBar>();
        energyBar.setMaxHealth(maxEnergy);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        message = GameObject.FindGameObjectWithTag("Message").transform.GetChild(0).gameObject;
        messageText = message.transform.GetChild(0).gameObject.GetComponent<Text>();
        armsAnim = GameObject.FindGameObjectWithTag("Arms").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        energyBar.SetHealth(currentEnergy);
        if (Input.GetKeyDown(KeyCode.C) && player.energyPacks > 0 &&!isPistol)
        {
            animator.SetTrigger("reload");
            armsAnim.SetTrigger("Reload");
            FindObjectOfType<AudioManager>().play("Reload");
            player.energyPacks -= 1;
            currentEnergy += 50;
            if (currentEnergy > maxEnergy)
            {
                currentEnergy = maxEnergy;
            }
        }
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            if (currentEnergy >= energyCost)
            {
                if (Time.time >= nextAttackTime)
                {
                    currentEnergy -= energyCost;
                    energyBar.SetHealth(currentEnergy);
                    if (isEnergyGun)
                    {
                        FindObjectOfType<AudioManager>().play("EnergyBlast");
                    }
                    else
                    {
                        FindObjectOfType<AudioManager>().play("Blast");
                    }
                    Instantiate(laser, firePoint.position, firePoint.rotation);
                    animator.SetTrigger("shoot");
                    armsAnim.SetTrigger("Shoot");
                    nextAttackTime = Time.time + 1f / fireSpeed;
                }
            }
            else
            {
                message.SetActive(true);
                messageText.text = "WEAPON OUT OF ENERGY!";
                StartCoroutine(setInactive());
            }
        }
        if (Input.GetKeyDown(KeyCode.Q) && player.grenades > 0)
        {
            animator.SetTrigger("throw");
        }
    }

    IEnumerator setInactive()
    {
        yield return new WaitForSeconds(2f);
        message.SetActive(false);
    }
}
