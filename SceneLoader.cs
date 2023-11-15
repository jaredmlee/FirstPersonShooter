using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Animator animator;
    public GameObject canvas;
    private GameObject mainCanvas;
    public GameObject image;
    // Start is called before the first frame update
    void Start()
    {
        mainCanvas = GameObject.FindGameObjectWithTag("Canvas");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void loadMetro()
    {
        StartCoroutine(loadLevel(3));
    }
    public void loadWarehouse()
    {
        StartCoroutine(loadLevel(1));
    }
    public void loadMountainLake()
    {
        StartCoroutine(loadLevel(2));
    }
    public void loadMenu()
    {
        StartCoroutine(loadLevel(0));
        Cursor.lockState = CursorLockMode.None;
    }

    IEnumerator loadLevel(int levelIndex)
    {
        image.SetActive(true);
        DontDestroyOnLoad(mainCanvas);
        animator.SetTrigger("Start");
        if (canvas != null)
        {
            canvas.SetActive(false);
        }
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(levelIndex);
    }
}
