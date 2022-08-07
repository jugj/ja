using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StickyPlatform : MonoBehaviour 
{

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        Debug.Log("EnterBoxcollision");
        if (collision.gameObject.name == "Player1") 
        {

            collision.gameObject.transform.SetParent(transform);
        }

        if (collision.gameObject.name == "Player2")
        {
            collision.gameObject.transform.SetParent(transform);
        }
    } 
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("exitBoxcollision");

        if (collision.gameObject.name == "Player1")
        {
            collision.gameObject.transform.SetParent(null);
        }

        if (collision.gameObject.name == "Player2")
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}
