using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSourcePool : MonoBehaviour
{
    public static AudioSourcePool instance;

    //Variables for sfx Pooling

    public GameObject pooledAudioSource;
    public int pooledAmount = 10;

    AudioSource _audioSource;

    List<GameObject> pooledAudioSourceList;

    //Create Singleton
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        pooledAudioSourceList = new List<GameObject>();

        for (int i = 0; i < pooledAmount; i++)
        {
            var obj = (GameObject)Instantiate(pooledAudioSource, this.transform);
            obj.SetActive(false);
            pooledAudioSourceList.Add(obj);
        }
    }

    public GameObject GetAudioSourceFromPool()
    {
        foreach(var pooledObject in pooledAudioSourceList)
        {
            if (!pooledObject.activeInHierarchy)
            {
                return pooledObject;
            }
        }
        return null;
    }
    
    //Play Pooled SFX Randomly 
    public void PooledRandomSFX(GameObject gameobject, AudioMixer audiomixer, string audiomixergroup, float pitchMin, float pitchMax, 
        AudioClip[] clip, float minVolumeClip, float maxVolumeClip, float spatialblend, int spread, float dopplerLevel, 
        AudioRolloffMode rolloffMode, float mindistance, float maxdistance, float reverbTime)
    {
        GameObject objFromPool = GetAudioSourceFromPool();

        if (objFromPool == null)
        {
            return;
        }

        objFromPool.transform.position = gameobject.transform.position;
        objFromPool.transform.rotation = gameobject.transform.rotation;
        objFromPool.SetActive(true);

        _audioSource = objFromPool.GetComponent<AudioSource>();

        SFXManager.instance.PlayRandomSFX(_audioSource, objFromPool, audiomixer, audiomixergroup, pitchMin, pitchMax, 
            clip, null, minVolumeClip, maxVolumeClip, 0, 0, spatialblend, spread, dopplerLevel, 
            rolloffMode, mindistance, maxdistance);

        StartCoroutine(StopPooledSFX(objFromPool, clip[Random.Range(0, clip.Length)], reverbTime));
    }

    IEnumerator StopPooledSFX(GameObject gameObject, AudioClip sfxClip, float reverbTime)
    {
        yield return new WaitForSeconds(sfxClip.length + reverbTime);
        _audioSource.Stop();

        gameObject.SetActive(false);
    }
}
