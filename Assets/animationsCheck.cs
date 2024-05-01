using UnityEngine;
using UnityEngine.InputSystem;

public class animationsCheck : MonoBehaviour
{
    private Keyboard keyboard;
    private Animator animator;

    void Start()
    {
        keyboard = Keyboard.current;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (keyboard.vKey.wasPressedThisFrame)
        {
            animator.SetTrigger("Attack1");
        }
    }
}