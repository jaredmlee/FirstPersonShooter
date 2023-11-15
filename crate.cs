using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class crate : MonoBehaviour
{
    public GameObject MessageBox;
    public Text message;
    public Animator animator;
    public bool opened;
    public GameObject player;
    public int numHealth = 1;
    public int numCredits = 1;
    public int numEnergy = 1;
    public int numGrenades = 1;
    public string mess;

    private bool triggered;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        MessageBox = GameObject.FindGameObjectWithTag("Message").transform.GetChild(0).gameObject;
        message = MessageBox.transform.GetChild(0).gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (triggered && !opened && Input.GetKeyDown(KeyCode.E))
        {
            animator.SetBool("opened", true);
            opened = true;
            Player script = player.GetComponent<Player>();
            script.healthPacks += numHealth;
            script.credits += numCredits;
            script.energyPacks += numEnergy;
            script.grenades += numGrenades;
            message.text = mess;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && !triggered)
        {
            triggered = true;
            if (!opened)
            {
                MessageBox.SetActive(true);
            }
            message.text = "Press E to open crate";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            triggered = false;
            MessageBox.SetActive(false);
        }
    }
}
