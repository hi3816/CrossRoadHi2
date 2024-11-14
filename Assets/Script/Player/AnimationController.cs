using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class AnimationController : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector3 input = context.ReadValue<Vector3>();
        if (input.magnitude > 1f) return;
        if (context.performed)  //수행됐는지 체크
        {
            if (input.magnitude == 0f)
            {
                animator.SetBool("KeyDown", false);
            }
            else
            {
                animator.SetBool("KeyDown", true);
            }
        }
    }
}
