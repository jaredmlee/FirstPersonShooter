using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsAndArmsAnimations : MonoBehaviour
{
    public Rigidbody player;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.velocity.x != 0 || player.velocity.y != 0)
        {
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }
    }
}
