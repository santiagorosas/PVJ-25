using UnityEngine;
using UnityEngine.InputSystem;

public class Penguin : MonoBehaviour
{
    private Animator _animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            _animator.SetTrigger("Attack");
        }

        bool isMoving = Keyboard.current.rightArrowKey.isPressed;
        Debug.Log("is moving? " + isMoving);
        _animator.SetBool("IsMoving", isMoving);
    }
}
