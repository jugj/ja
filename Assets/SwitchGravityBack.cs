using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchGravityBack : MonoBehaviour
{
    public Animator animator;
    private bool isCol1;
    private bool isCol2;

    private bool pressb = false;

    public AudioSource press;

    public Rigidbody2D Player1;

    public Rigidbody2D Player2;

    public PlayerMovement playermove1;

    public PlayerMovement2 playermove2;

    public Transform Player1position;
    public Transform Player2position;

    private bool top;
    private bool top2;


    void Start()
    {
        animator.SetBool("Press", false);
        pressb = false;
    }

    void Update()
    {
        if (isCol1 == true)
        {
            if (pressb == false)
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    Debug.Log("Press");
                    animator.SetBool("Press", true);
                    press.Play();
                    pressb = true;
                    Player2.gravityScale *= -1;
                    Rotation2();
                }


            }

        }

        if (isCol2 == true)
        {
            if (pressb == false)
            {
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    Debug.Log("Press");
                    animator.SetBool("Press", true);
                    press.Play();
                    pressb = true;

                    Player1.gravityScale *= -1;
                    Rotation1();
                }
            }
        }


    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Touch");
            isCol1 = true;

        }

        if (col.gameObject.CompareTag("Player2"))
        {
            Debug.Log("Touch");
            isCol2 = true;

        }



    }

    public void Rotation1()
    {
        if (top == false)
        {
            Player1position.transform.eulerAngles = new Vector3(0, 0, 0f);
        }
        else
        {
            Player1position.transform.eulerAngles = Vector3.zero;
        }
        playermove1.isFacingRight = !playermove1.isFacingRight;
        top = !top;
    }

    public void Rotation2()
    {
        if (top2 == false)
        {
            Player2position.transform.eulerAngles = new Vector3(0, 0, 0f);
        }
        else
        {
            Player2position.transform.eulerAngles = Vector3.zero;
        }
        playermove2.isFacingRight = !playermove1.isFacingRight;
        top2 = !top2;
    }
}
