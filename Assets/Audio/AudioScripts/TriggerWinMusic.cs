using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWinMusic : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            MusicManager mm = MusicManager.instance;
            mm.StartCoroutine(mm.FadeOut(mm.musicSource[1]));
            mm.StartCoroutine(mm.FadeOut(mm.musicSource[2]));

            mm.StartCoroutine(mm.FadeIn(mm.musicSource[0], mm.winMusic, 0f));
        }
    }
}
