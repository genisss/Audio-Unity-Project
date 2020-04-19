using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class RandomNonRepeat : MonoBehaviour
{
    int _clip01Index;
    int _clip02Index;

    public void PlayRandomNonRepeatSFX(AudioSource audioSource, GameObject gameobject, AudioMixer audiomixer, string audiomixergroup,
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

        _clip01Index = RepeatCheck(_clip01Index, clip01.Length);
        audioSource.PlayOneShot(clip01[_clip01Index], Random.Range(minVolumeClip01, maxVolumeClip01));




        if (clip02 != null)
        {
            _clip02Index = RepeatCheck(_clip02Index, clip02.Length);
            audioSource.PlayOneShot(clip02[_clip02Index], Random.Range(minVolumeClip02, maxVolumeClip02));
        }
    }
    int RepeatCheck(int previousIndex, int range)
    {
        int index = Random.Range(0, range);

        while(index == previousIndex)
        {
            index = Random.Range(0, range);
        }
        return index;
    }


}
