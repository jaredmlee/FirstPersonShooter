using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject player;
    public Transform spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spawnPoint = GameObject.FindGameObjectWithTag("Spawn").transform;
            player.GetComponent<FirstPersonController>().enabled = true;
            Player pScript = player.GetComponent<Player>();
            pScript.enabled = true;
            pScript.currentHealth = pScript.maxHealth;
            pScript.isDead = false;
            player.transform.position = spawnPoint.position;
            gameObject.SetActive(false);
        }
    }
}
