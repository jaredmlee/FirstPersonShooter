using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenStore : MonoBehaviour
{
    public GameObject store;
    public GameObject settings;
    public GameObject start;
    public GameObject menu;

    public bool storeOpen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void openStore()
    {
        store.SetActive(true);
        storeOpen = true;
    }
    public void closeStore()
    {
        store.SetActive(false);
        storeOpen = false;
    }
    public void openSettings()
    {
        menu.SetActive(false);
        settings.SetActive(true);
    }
    public void backToStart()
    {
        start.SetActive(true);
    }
    public void playGame()
    {
        start.SetActive(false);
    }
    public void quitGame()
    {
        Application.Quit();
        Debug.Log("Quit game");
    }
}
