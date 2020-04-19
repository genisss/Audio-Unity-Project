using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        GetComponent<Animator>().SetBool("character_nearby", true);
        GetComponent<AudioSource>().Play();
    } 
    private void OnTriggerExit(Collider other)
    {
        GetComponent<Animator>().SetBool("character_nearby", false);
        GetComponent<AudioSource>().Play();
    }
}
