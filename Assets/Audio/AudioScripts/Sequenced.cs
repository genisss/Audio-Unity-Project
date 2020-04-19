using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Sequenced : MonoBehaviour
{
    int _clip01Index;
    int _clip02Index;

    public void PlaySquencedSFX(AudioSource audioSource, GameObject gameobject, AudioMixer audiomixer, string audiomixergroup,
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

        if(_clip01Index < clip01.Length)
        {
            audioSource.PlayOneShot(clip01[_clip01Index], Random.Range(minVolumeClip01, maxVolumeClip01));
            _clip01Index++;
        }
        else
        {
            _clip01Index = 0;
            audioSource.PlayOneShot(clip01[_clip01Index], Random.Range(minVolumeClip01, maxVolumeClip01));
            _clip01Index++;
        }        

        if (clip02 != null)
        {
            if (_clip02Index < clip02.Length)
            {
                audioSource.PlayOneShot(clip02[_clip02Index], Random.Range(minVolumeClip02, maxVolumeClip02));
                _clip02Index++;
            }
            else
            {
                _clip02Index = 0;
                audioSource.PlayOneShot(clip02[_clip02Index], Random.Range(minVolumeClip02, maxVolumeClip02));
                _clip02Index++;
            }
        }
    }
}
