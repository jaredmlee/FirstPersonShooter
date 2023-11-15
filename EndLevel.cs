using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EndLevel : MonoBehaviour
{
    public GameObject MessageBox;
    public Text message;
    public SceneLoader sceneLoader;

    public bool triggered = false;
    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = GameObject.FindGameObjectWithTag("Loader").GetComponent<SceneLoader>();
        MessageBox = GameObject.FindGameObjectWithTag("Message").transform.GetChild(0).gameObject;
        message = MessageBox.transform.GetChild(0).gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && triggered)
        {
            MessageBox.SetActive(false);
            sceneLoader.loadMenu();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            MessageBox.SetActive(true);
            message.text = "Press E to grab crystal";
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
}
