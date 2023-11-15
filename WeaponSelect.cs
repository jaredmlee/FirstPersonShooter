using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelect : MonoBehaviour
{
    public Animator animator;
    public WeaponSlide basicGun;
    // Start is called before the first frame update
    private void OnEnable()
    {
        if (basicGun.index != 0)
        {
            animator.SetTrigger("to" + basicGun.index);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (basicGun.index == 0)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                animator.SetTrigger("to-1");
            }
        }
        else if (basicGun.index == -1)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                animator.SetTrigger("to-2");
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                animator.SetTrigger("to0");
            }
        }
        else if (basicGun.index == -2)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                animator.SetTrigger("to-3");
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                animator.SetTrigger("to-1");
            }
        }
        else if (basicGun.index == -3)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                animator.SetTrigger("to-4");
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                animator.SetTrigger("to-2");
            }
        }
        else if (basicGun.index == -4)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                animator.SetTrigger("to-5");
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                animator.SetTrigger("to-3");
            }
        }
        else if (basicGun.index == -5)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                animator.SetTrigger("to-6");
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                animator.SetTrigger("to-4");
            }
        }
        else if (basicGun.index == -6)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                animator.SetTrigger("to-7");
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                animator.SetTrigger("to-5");
            }
        }
        else if (basicGun.index == -7)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                animator.SetTrigger("to-8");
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                animator.SetTrigger("to-6");
            }
        }
        else if (basicGun.index == -8)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                animator.SetTrigger("to-9");
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                animator.SetTrigger("to-7");
            }
        }
        else if (basicGun.index == -9)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                animator.SetTrigger("to-10");
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                animator.SetTrigger("to-8");
            }
        }
        else if (basicGun.index == -10)
        {
            
            if (Input.GetKeyDown(KeyCode.A))
            {
                animator.SetTrigger("to-9");
            }
        }
    }
}
