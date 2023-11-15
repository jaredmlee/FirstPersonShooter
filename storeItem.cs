using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class storeItem : MonoBehaviour
{
    public Animator animator;
    public int index = 0;
    public WeaponSlide weapon;
    public bool isBeginner;
    public bool isEnder;
    public stopMovementTracker stopMovement;
    public GameObject bought;
    public bool isWeapon;
    public bool purchased = false;
    public Text playerCredits;
    public Text healthPacks;
    public Text energyPacks;
    public Text grenades;
    public int cost;
    public string nameOfItem;
    public GameObject notEnoughCredits;


    void Start()
    {
        WeaponSlide[] weapons = FindObjectsOfType<WeaponSlide>(true);
        foreach (WeaponSlide w in weapons)
        {
            if (w.gameObject.name == nameOfItem)
            {
                weapon = w;
            }
        }
        playerCredits = GameObject.FindGameObjectWithTag("CreditsNum").GetComponent<Text>();
        healthPacks = GameObject.FindGameObjectWithTag("HealthPackNum").GetComponent<Text>();
        energyPacks = GameObject.FindGameObjectWithTag("EnergyPackNum").GetComponent<Text>();
        grenades = GameObject.FindGameObjectWithTag("GrenadesNum").GetComponent<Text>();
        if (weapon != null)
        {
            if (!weapon.isLocked)
            {
                purchased = true;
                bought.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (index == 0 )
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isWeapon && checkCredits() && !purchased)
                {

                    purchaseWeapon();
                }
                else if (!isWeapon && checkCredits())
                {
                    purchaseItem();
                }
                else if (!purchased)
                {
                    notEnoughCredits.SetActive(true);
                    StartCoroutine(setNotEnoughInactive());
                }
            }
        }

        if (isEnder && index == 0)
        {
            stopMovement.stopMovementD = true;
        }
        if (Input.GetKeyDown(KeyCode.D) && !stopMovement.stopMovementD)
        {
            stopMovement.stopMovement = false;
            if (index == 0)
            {
                animator.SetTrigger("reduceSize");
            }
            if (index == 1)
            {
                animator.SetTrigger("increaseSize");
            }
            StartCoroutine(decrement());
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
            if (index == -1)
            {
                animator.SetTrigger("increaseSize");
            }
            StartCoroutine(increment());
        }
    }
    IEnumerator increment()
    {
        yield return new WaitForEndOfFrame();
        index++;
    }
    IEnumerator decrement()
    {
        yield return new WaitForEndOfFrame();
        index--;
    }

    public bool checkCredits()
    {
        int number;
        bool success = int.TryParse(playerCredits.text, out number);
        if (success)
        {
            if (number >= cost)
            {
                return true;
            } 
        }
        return false;
    }

    public void purchaseWeapon()
    {
        int number;
        bool success = int.TryParse(playerCredits.text, out number);
        if (success)
        {
            number -= cost;
            playerCredits.text = number.ToString();
        }
        purchased = true;
        bought.SetActive(true);
        weapon.isLocked = false;
        weapon.locked.SetActive(false);
    }

    public void purchaseItem()
    {
        int number;
        bool success = int.TryParse(playerCredits.text, out number);
        if (success)
        {
            number -= cost;
            playerCredits.text = number.ToString();

        }
        int number1 = 0;
        bool success2;
        if (nameOfItem == "HealthPack")
        {
            success2 = int.TryParse(healthPacks.text, out number1);
        }
        else if (nameOfItem == "EnergyPack")
        {
            success2 = int.TryParse(energyPacks.text, out number1);
        }
        else if (nameOfItem == "Grenade")
        {
            success2 = int.TryParse(grenades.text, out number1);
        }
        else
        {
            success2 = false;
        }
        if (success2)
        {
            number1++;
            if (nameOfItem == "EnergyPack")
            {
                energyPacks.text = number1.ToString();
            }
            else if (nameOfItem == "HealthPack")
            {
                healthPacks.text = number1.ToString();
            }
            else if (nameOfItem == "Grenade")
            {
                grenades.text = number1.ToString();
            }
        }
        bought.SetActive(true);
        StartCoroutine(setBoughtInactive());
    }
    IEnumerator setBoughtInactive()
    {
        yield return new WaitForSeconds(0.5f);
        bought.SetActive(false);
    }
    IEnumerator setNotEnoughInactive()
    {
        yield return new WaitForSeconds(0.5f);
        notEnoughCredits.SetActive(false);
    }
}
