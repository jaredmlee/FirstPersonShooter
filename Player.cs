using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int maxHealth = 200;
    public int currentHealth = 200;
    public int healthPacks = 0;
    public int energyPacks = 0;
    public int grenades = 0;
    public int credits = 0;
    public bool isDead = false;

    public Animator armsAnim;
    public Text respawnCreditNum;
    public GameObject respawnScreen;
    public CameraShake cameraShake;
    public HealthBar healthbar;
    public Text numHealthPacks;
    public Text numEnergyPacks;
    public Text numCredits;
    public Text numGrenades;
    public GameObject grenade;
    public Transform grenadeThrowPoint;
    // Start is called before the first frame update
    void Start()
    {
        armsAnim = GameObject.FindGameObjectWithTag("Arms").GetComponent<Animator>();
        healthbar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBar>();
        numCredits = GameObject.FindGameObjectWithTag("CreditsNum").GetComponent<Text>();
        numEnergyPacks = GameObject.FindGameObjectWithTag("EnergyPackNum").GetComponent<Text>();
        numHealthPacks = GameObject.FindGameObjectWithTag("HealthPackNum").GetComponent<Text>();
        numGrenades = GameObject.FindGameObjectWithTag("GrenadesNum").GetComponent<Text>();
        respawnScreen = GameObject.FindGameObjectWithTag("Respawn").transform.GetChild(0).gameObject;
        currentHealth = maxHealth;
        healthbar.setMaxHealth(maxHealth);
        setItemNums();
    }

    // Update is called once per frame
    void Update()
    {
        numHealthPacks.text = healthPacks.ToString();
        numEnergyPacks.text = energyPacks.ToString();
        numCredits.text = credits.ToString();
        numGrenades.text = grenades.ToString();
        healthbar.SetHealth(currentHealth);
        //use Health Pack
        if (Input.GetKeyDown(KeyCode.X) && healthPacks > 0)
        {
            FindObjectOfType<AudioManager>().play("Heal");
            healthPacks -= 1;
            currentHealth = maxHealth;
        }
        // throw grenade
        if (Input.GetKeyDown(KeyCode.Q) && grenades > 0)
        {
            grenades -= 1;
            Instantiate(grenade, grenadeThrowPoint.position, grenadeThrowPoint.rotation);
            armsAnim.SetTrigger("Throw");
        }
    }
    public void takeDamage(int damage)
    {
        if (isDead)
        {
            return;
        }
        //TODO - some sort of animation or camera shake that shows damage has been dealt
        cameraShake.shakeDuration = 0.3f;
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            die();
        }
    }

    public void die()
    {
        isDead = true;
        FindObjectOfType<AudioManager>().play("Death");
        respawnScreen.SetActive(true);
        if (credits >= 40)
        {
            respawnCreditNum = GameObject.FindGameObjectWithTag("RespawnNum").GetComponent<Text>();
            respawnCreditNum.text = "40";
            credits -= 40;
        }
        else
        {
            respawnCreditNum = GameObject.FindGameObjectWithTag("RespawnNum").GetComponent<Text>();
            respawnCreditNum.text = credits.ToString();
            credits = 0;
        }
        gameObject.GetComponent<FirstPersonController>().enabled = false;
        this.enabled = false;
    }
    public void setItemNums()
    {
        int number;
        bool success = int.TryParse(numHealthPacks.text, out number);
        if (success)
        {
            healthPacks = number;
        }
        bool success2 = int.TryParse(numEnergyPacks.text, out number);
        if (success2)
        {
            energyPacks = number;
        }
        bool success3 = int.TryParse(numCredits.text, out number);
        if (success3)
        {
            credits = number;
        }
        bool success4 = int.TryParse(numGrenades.text, out number);
        if (success4)
        {
            grenades = number;
        }
    }
}
