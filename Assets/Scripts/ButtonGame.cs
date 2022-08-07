using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonGame : MonoBehaviour
{
    public Animator animator;
    private bool isCol1;
    private bool isCol2;

    public Transform SpawnPoint;
    public GameObject JumpPrefab;

    private bool pressb = false;

    public AudioSource press;

    void Start()
    {
        animator.SetBool("Press", false);
        pressb = false;
    }

    void Update()
    {
        if(isCol1 == true)
        {
            if (pressb == false)
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    Debug.Log("Press");
                    animator.SetBool("Press", true);
                    press.Play();
                    pressb = true;
                    Instantiate(JumpPrefab, SpawnPoint.position, SpawnPoint.rotation);
                }

                    
            }

        }

        if(isCol2 == true)
        {
            if (pressb == false)
            {
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    Debug.Log("Press");
                    animator.SetBool("Press", true);
                    press.Play();
                    pressb = true;

                    Instantiate(JumpPrefab, SpawnPoint.position, SpawnPoint.rotation);
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
}
