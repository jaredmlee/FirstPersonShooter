using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public bool isPaused;
    private SceneLoader sceneLoader;
    public bool gamePaused;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                resumeGame();
            }
            else
            {
                pauseGame();
            }
        }
    }

    public void pauseGame()
    {
        gamePaused = true;
        Cursor.lockState = CursorLockMode.None;
        isPaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void resumeGame()
    {
        gamePaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        Time.timeScale = 1f;

    }

    public void backToMenu()
    {
        resumeGame();
        sceneLoader = GameObject.FindGameObjectWithTag("Loader").GetComponent<SceneLoader>();
        sceneLoader.loadMenu();
    }
    
    public void openSettings()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }
}
