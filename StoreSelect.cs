using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreSelect : MonoBehaviour
{
    public Animator animator;
    public storeItem firstItem;
    public Text credits;
    public Text grenades;
    public Text healthPacks;
    public Text energyPacks;
    public Text playersCredits;
    public Text playersGrenades;
    public Text playersHealthPacks;
    public Text playersEnergyPacks;

    // Start is called before the first frame update
    private void OnEnable()
    {
        animator.SetTrigger("to0");
        if (firstItem.index != 0)
        {
            animator.SetTrigger("to" + firstItem.index);
            Debug.Log("to" + firstItem.index);
        }
    }
    void Start()
    {
        playersCredits = GameObject.FindGameObjectWithTag("CreditsNum").GetComponent<Text>();
        playersHealthPacks = GameObject.FindGameObjectWithTag("HealthPackNum").GetComponent<Text>();
        playersEnergyPacks = GameObject.FindGameObjectWithTag("EnergyPackNum").GetComponent<Text>();
        playersGrenades = GameObject.FindGameObjectWithTag("GrenadesNum").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        updateNums();
        if (firstItem.index == 0)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                animator.SetTrigger("to-1");
            }
        }
        else if (firstItem.index == -1)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                animator.SetTrigger("to-2");
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                animator.SetTrigger("to0");
            }
        }
        else if (firstItem.index == -2)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                animator.SetTrigger("to-3");
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                animator.SetTrigger("to-1");
            }
        }
        else if (firstItem.index == -3)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                animator.SetTrigger("to-4");
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                animator.SetTrigger("to-2");
            }
        }
        else if (firstItem.index == -4)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                animator.SetTrigger("to-5");
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                animator.SetTrigger("to-3");
            }
        }
        else if (firstItem.index == -5)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                animator.SetTrigger("to-6");
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                animator.SetTrigger("to-4");
            }
        }
        else if (firstItem.index == -6)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                animator.SetTrigger("to-7");
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                animator.SetTrigger("to-5");
            }
        }
        else if (firstItem.index == -7)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                animator.SetTrigger("to-8");
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                animator.SetTrigger("to-6");
            }
        }
        else if (firstItem.index == -8)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                animator.SetTrigger("to-9");
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                animator.SetTrigger("to-7");
            }
        }
        else if (firstItem.index == -9)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                animator.SetTrigger("to-10");
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                animator.SetTrigger("to-8");
            }
        }
        else if (firstItem.index == -10)
        {

            if (Input.GetKeyDown(KeyCode.A))
            {
                animator.SetTrigger("to-9");
            }
        }
    }
    public void updateNums()
    {
        credits.text = playersCredits.text;
        grenades.text = playersGrenades.text;
        healthPacks.text = playersHealthPacks.text;
        energyPacks.text = playersEnergyPacks.text;
    }
}
