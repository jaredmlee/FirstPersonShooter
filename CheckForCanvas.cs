using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForCanvas : MonoBehaviour
{
    public static CheckForCanvas instance;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
