using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Camera cam;
    public Animator animator;
    public GameOver gameOver;
    public int runSpeed;
    public float turnSpeed;
    bool damaged = false;
    float inflictDamageTimer = 9.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");

        Vector3 moveDirection = new Vector3(horizontal, 0.0f, vertical);
        moveDirection.Normalize();
        
        if (moveDirection != Vector3.zero && CanMove())
        {
            moveDirection = Quaternion.AngleAxis(cam.transform.rotation.eulerAngles.y, Vector3.up) * moveDirection;
            Quaternion toRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0.0f, moveDirection.z));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, turnSpeed * 100.0f * Time.deltaTime);

            switch(runSpeed)
            {
                case 50:
                    animator.SetBool("run", true);
                    break;
                case 15:
                    animator.SetBool("walk", true);
                    break;
                case 10:
                    animator.SetBool("hurtWalk", true);
                    break;
            }
        }
        else
        {
            animator.SetBool("run", false);
            animator.SetBool("walk", false);
            animator.SetBool("hurtWalk", false);
        }

        moveDirection.y = -9.8f;

        moveDirection.Normalize();

        if(CanMove())
            controller.Move(moveDirection * runSpeed * Time.deltaTime);
    }

    public void TakeDamage()
    {
        if(!damaged && inflictDamageTimer == 9.0f)
        {
            damaged = true;
            animator.SetBool("damaged", true);
            switch(runSpeed)
            {
                case 50:
                    runSpeed = 15;
                    break;
                case 15:
                    runSpeed = 10;
                    break;
                case 10:
                    animator.SetBool("dead", true);
                    damaged = true;
                    break;
            }
                
            StartCoroutine(InvisibiltyPeriod());
        }
    }

    IEnumerator InvisibiltyPeriod()
    {
        while (inflictDamageTimer > 0)
        {
            inflictDamageTimer -= Time.deltaTime;
            yield return null;
        }

        inflictDamageTimer = 9.0f;
    }

    public void Heal()
    {
        damaged = false;
        animator.SetBool("damaged", false);
    }

    public void SetPlayerSpeed(int speed)
    {
        runSpeed = speed;
    }

    public int GetPlayerSpeed()
    {
        return runSpeed;
    }

    bool CanMove()
    {
        return !damaged;
    }

    public void StartGameOver()
    {
        gameOver.FadeToBlack();
    }
}
