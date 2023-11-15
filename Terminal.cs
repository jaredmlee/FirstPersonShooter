using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Terminal : MonoBehaviour
{
    public GameObject MessageBox;
    public Text message;
    public GameObject player;
    public Transform spawnPoint;

    private bool triggered;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        MessageBox = GameObject.FindGameObjectWithTag("Message").transform.GetChild(0).gameObject;
        message = MessageBox.transform.GetChild(0).gameObject.GetComponent<Text>();
    }

    void Update()
    {
        if (triggered &&  Input.GetKeyDown(KeyCode.E))
        {
            spawnPoint.position = player.transform.position;
            message.text = "Respawn Point Set";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && !triggered)
        {
            triggered = true;
            MessageBox.SetActive(true);
            message.text = "Press E to activate terminal";
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
