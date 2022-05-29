using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : MonoBehaviour
{
    public Animator animator;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetBool("Shooting", true);
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            animator.SetBool("Shooting", false);
        }
    }
}
