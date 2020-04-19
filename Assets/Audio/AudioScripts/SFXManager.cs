using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SFXManager : MonoBehaviour
{    
    public static SFXManager instance = null;   

    public void Awake()
    {
        //Create Singleton
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }

    //Play clips in an array randomly (can play two clips at once if necessary)
    public void PlayRandomSFX(AudioSource audioSource, GameObject gameobject, AudioMixer audiomixer, string audiomixergroup, 
        float pitchMin, float pitchMax, AudioClip[] clip01, AudioClip[] clip02, 
        float minVolumeClip01, float maxVolumeClip01, float minVolumeClip02, float maxVolumeClip02, 
        float spatialblend, int spread, float dopplerLevel, AudioRolloffMode rolloffMode, float mindistance, float maxdistance)
    {
        audioSource = gameobject.GetComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = audiomixer.FindMatchingGroups(audiomixergroup)[0];

        audioSource.pitch = Random.Range(pitchMin, pitchMax);
        audioSource.spatialBlend = spatialblend;
        audioSource.spread = spread;
        audioSource.dopplerLevel = dopplerLevel;
        audioSource.rolloffMode = rolloffMode;
        audioSource.minDistance = mindistance;
        audioSource.maxDistance = maxdistance;

        audioSource.PlayOneShot(clip01[Random.Range(0, clip01.Length)], Random.Range(minVolumeClip01, maxVolumeClip01));

        if (clip02 != null)
        {
            audioSource.PlayOneShot(clip02[Random.Range(0, clip02.Length)], Random.Range(minVolumeClip02, maxVolumeClip02));
        }
    }   
}
