using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class TriggerOutsideMusic : MonoBehaviour
{
    bool toggle;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
                MusicManager mm = MusicManager.instance;
            if (!toggle)
            {

                mm.StartCoroutine(mm.FadeOut(mm.musicSource[0]));
                mm.StartCoroutine(mm.FadeIn(mm.musicSource[1], mm.outsideMusic, 0f));
            }
            else
            {
                mm.StartCoroutine(mm.FadeOut(mm.musicSource[1]));
                mm.StartCoroutine(mm.FadeIn(mm.musicSource[0], mm.insideMusic, 0f));
            }
            toggle = !toggle;
        }
    }
}
