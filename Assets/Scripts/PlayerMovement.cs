using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Camera cam;
    public Animator animator;
    public float runSpeed;
    public float turnSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");

        Vector3 moveDirection = new Vector3(horizontal, 0.0f, vertical);

        Debug.Log(moveDirection.magnitude + ": 1");
        moveDirection.Normalize();
        Debug.Log(moveDirection.magnitude + ": 3");

        if (moveDirection != Vector3.zero)
        {
            moveDirection = Quaternion.AngleAxis(cam.transform.rotation.eulerAngles.y, Vector3.up) * moveDirection;
            Quaternion toRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0.0f, moveDirection.z));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, turnSpeed * 100.0f * Time.deltaTime);
            animator.SetBool("run", true);
        }
        else
        {
            animator.SetBool("run", false);
        }

        moveDirection.y = -9.8f;

        moveDirection.Normalize();

        controller.Move(moveDirection * runSpeed * Time.deltaTime);
    }

    bool CanMove()
    {
        return controller.isGrounded;
    }
}
