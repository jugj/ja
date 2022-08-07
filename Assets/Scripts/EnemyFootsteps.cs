using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFootsteps : MonoBehaviour
{

    public AudioClip[] footsounds;
    public AudioSource sound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void footsteps(int _num)
    {
        sound.clip = footsounds[_num];
        sound.Play();
    }
}
