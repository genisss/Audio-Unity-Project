using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Shuffle : MonoBehaviour
{
    public int _clip01Index;
    public int _clip02Index;

    public List<AudioClip> _clipShuffleBag01 = new List<AudioClip>();
    public List<AudioClip> _clipShuffleBag02 = new List<AudioClip>();

    public void PlayShuffledSFX(AudioSource audioSource, GameObject gameobject, AudioMixer audiomixer, string audiomixergroup,
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

        if(_clipShuffleBag01.Count < 1)
        {
            foreach(var clip in clip01)
            {
                _clipShuffleBag01.Add(clip);
            }
        }

        _clip01Index = RepeatCheck(_clip01Index, _clipShuffleBag01.Count);
        _clipShuffleBag01.RemoveAt(_clip01Index);
        audioSource.PlayOneShot(clip01[_clip01Index], Random.Range(minVolumeClip01, maxVolumeClip01));

        if(clip02 != null)
        {
            if (_clipShuffleBag02.Count < 1)
            {
                foreach (var clip in clip02)
                {
                    _clipShuffleBag02.Add(clip);
                }
            }

            _clip02Index = RepeatCheck(_clip02Index, _clipShuffleBag02.Count);
            _clipShuffleBag02.RemoveAt(_clip02Index);
            audioSource.PlayOneShot(clip02[_clip02Index], Random.Range(minVolumeClip02, maxVolumeClip02));
        }
    }

    int RepeatCheck(int previousIndex, int range)
    {
        int index = Random.Range(0, range);
        if (range != 1)
        {
            while (index == previousIndex)
            {
                index = Random.Range(0, range);
            }
        }
        return index;
    }
}
