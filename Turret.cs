using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Turret : MonoBehaviour
{
    public Animator animator;
    public GameObject MessageBox;
    public Text message;
    public bool triggered = false;
    public GameObject weaponSlot;
    public GameObject weapon;
    public Transform standPoint;
    public GameObject player;
    public GameObject turretGun;
    public bool equiped = false;
    public float fireSpeed = 10f;
    public GameObject laser;
    public Transform firePoint;
    public GameObject turretParent;
    public GameObject forceField;

    public Player playerScript;

    private float nextAttackTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        weaponSlot = GameObject.FindGameObjectWithTag("weaponSlot");
        weapon = weaponSlot.transform.GetChild(0).gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
        MessageBox = GameObject.FindGameObjectWithTag("Message").transform.GetChild(0).gameObject;
        message = MessageBox.transform.GetChild(0).gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && triggered && !equiped)
        {
            equip();

        }
        else if (equiped)
        {
            if (playerScript.isDead)
            {
                unequip();
            }
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
            {
                if (Time.time >= nextAttackTime)
                {
                    FindObjectOfType<AudioManager>().play("Blast");
                    Instantiate(laser, firePoint.position, firePoint.rotation);
                    animator.SetTrigger("Shoot");
                    nextAttackTime = Time.time + 1f / fireSpeed;
                }
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                unequip();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            MessageBox.SetActive(true);
            message.text = "Press E to use turret";
            triggered = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            MessageBox.SetActive(false);
            triggered = false;
        }
    }

    public void unequip()
    {
        turretGun.transform.SetParent(turretParent.transform);
        turretGun.transform.localPosition = new Vector3(0f, 0f, 0f);
        player.GetComponent<FirstPersonController>().playerCanMove = true;
        weapon.SetActive(true);
        forceField.SetActive(false);
        equiped = false;
    }

    public void equip()
    {
        weapon = weaponSlot.transform.GetChild(0).gameObject;
        animator.SetTrigger("Shoot");
        player.transform.position = standPoint.position;
        //MessageBox.SetActive(false);
        message.text = "Press E to detatch turret.";
        weapon.SetActive(false);
        player.GetComponent<FirstPersonController>().playerCanMove = false;
        player.GetComponent<FirstPersonController>().isWalking = false;
        turretGun.transform.SetParent(weaponSlot.transform);
        turretGun.transform.localPosition = new Vector3(0f, -0.7f, 0.21f);
        turretGun.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        forceField.SetActive(true);
        equiped = true;
    }
}