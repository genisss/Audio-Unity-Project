using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SFXSettings
{
    [Range(0f, 2f)]
    public float minPitch = 1;
    [Range(0f, 2f)]
    public float maxPitch = 1;

    public AudioClip[] clip01;
    public AudioClip[] clip02;
    
    [Range(0f, 1f)]
    public float clip01MinVolume = 1;
    [Range(0f, 1f)]
    public float clip01MaxVolume = 1;
    [Range(0f, 1f)]
    public float clip02MinVolume = 1;
    [Range(0f, 1f)]
    public float clip02MaxVolume = 1;

    [Range(0f, 1f)]
    public float spatialBlend = 0;
    [Range(0f, 1f)]
    public float dopplerLevel = 0;
    [Range(0, 360)]
    public int spread = 0;

    public AudioRolloffMode rollofMode = AudioRolloffMode.Linear;

    public float minDistance = 1;
    public float maxDistance = 20;
}
