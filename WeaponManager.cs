using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WeaponManager : MonoBehaviour
{
    public GameObject weaponSlot;
    public GameObject weaponMenu;
    public GameObject MessageBox;
    public Text message;


    public bool menuActivated = false;


    // Start is called before the first frame update
    void Start()
    {
        weaponMenu = GameObject.FindGameObjectWithTag("WeaponSelect").transform.GetChild(0).gameObject;
        MessageBox = GameObject.FindGameObjectWithTag("Message").transform.GetChild(0).gameObject;
        message = MessageBox.transform.GetChild(0).gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!menuActivated)
            {
                activateMenu();
            }
            else
            {
                DeactivateMenu();
            }
        }
    }

    public void equipWeapon(GameObject weapon, int weaponEnergy)
    {
        Destroy(weaponSlot.transform.GetChild(0).gameObject);
        GameObject newChild = Instantiate(weapon, weaponSlot.transform);
        newChild.GetComponent<Weapon>().currentEnergy = weaponEnergy;
        newChild.transform.localPosition = Vector3.zero;
        newChild.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        newChild.transform.localScale = Vector3.one;
    }
    
    public void activateMenu()
    {
        weaponMenu.SetActive(true);
        menuActivated = true;
        Time.timeScale = 0f;
        MessageBox.SetActive(false);
    }

    public void DeactivateMenu()
    {
        weaponMenu.SetActive(false);
        menuActivated = false;
        Time.timeScale = 1f;
    }
}
