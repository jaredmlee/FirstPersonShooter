using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlide : MonoBehaviour
{
    public Animator arms;
    public GameObject weaponSlot;
    public Animator animator;
    public int index = 0;
    public GameObject weapon;
    public WeaponManager weaponManager;
    public int weaponEnergy;
    public Text currEnergynum;
    public bool isBeginner;
    public bool isEnder;
    public stopMovementTracker stopMovement;
    public bool isLocked = true;
    public GameObject locked;

    private bool done;
    // Start is called before the first frame update
    private void OnDisable()
    {
        done = false;
    }
    void Start()
    {
        arms = GameObject.FindGameObjectWithTag("Arms").GetComponent<Animator>();
        weaponManager = GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>();
        if (weapon!= null) // get rid of this condition later
        {
            weaponEnergy = weapon.GetComponent<Weapon>().currentEnergy;
        }
        //weaponSlot = GameObject.FindGameObjectWithTag("weaponSlot");
    }

    // Update is called once per frame
    void Update()
    {
        if (!done && !isLocked)
        {
            weaponSlot = GameObject.FindGameObjectWithTag("weaponSlot");
            weaponManager = GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>();
            Weapon currWeapon = weaponSlot.transform.GetChild(0).gameObject.GetComponent<Weapon>();
            if (currWeapon.gameObject.CompareTag(weapon.gameObject.tag))
            {
                weaponEnergy = currWeapon.currentEnergy;
            }
            done = true;
            currEnergynum.text = weaponEnergy.ToString();
        }
        if (index == 0 && !isLocked)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                weaponManager.equipWeapon(weapon, weaponEnergy) ;
                if (weapon.gameObject.CompareTag("Pistol"))
                {
                    arms.SetBool("pistol", true);
                }
                else
                {
                    arms.SetBool("pistol", false);
                }
                weaponManager.DeactivateMenu();
                done = false;
            }
        }
        if (isEnder && index == 0)
        {
            stopMovement.stopMovementD = true;
        }
        if (Input.GetKeyDown(KeyCode.D) && !stopMovement.stopMovementD)
        {
            stopMovement.stopMovement = false;
            if (index == 0 )
            {
                animator.SetTrigger("reduceSize");
            }
            if (index == 1)
            {
                animator.SetTrigger("increaseSize");
            }
            index--;
        }
        if (isBeginner && index == 0)
        {
            stopMovement.stopMovement = true;
        }
        if (Input.GetKeyDown(KeyCode.A) && !stopMovement.stopMovement)
        {
            stopMovement.stopMovementD = false;
            if (index == 0)
            {
                animator.SetTrigger("reduceSize");
            }
            if ( index == -1)
            {
                animator.SetTrigger("increaseSize");
            }
            index++;
        }

    }
}
