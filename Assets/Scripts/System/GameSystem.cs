using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif


public class GameSystem : MonoBehaviour
{
    public static GameSystem Instance { get; private set; }


    public GameObject[] StartPrefabs;
    public float TargetMissedPenalty = 1.0f;




    float m_Timer;
    bool m_TimerRunning = false;



    void Awake()
    {
        Instance = this;
        foreach (var prefab in StartPrefabs)
        {
            Instantiate(prefab);
        }

        PoolSystem.Create();
    }

    void Start()
    {
        WorldAudioPool.Init();
        GameSystemInfo.Instance.UpdateScore(0);
        StartTimer();
    }

    public void ResetTimer()
    {
        m_Timer = 0.0f;
    }

    public void StartTimer()
    {
        m_TimerRunning = true;
    }

    public void StopTimer()
    {
        m_TimerRunning = false;
    }


    void Update()
    {
        m_Timer = GameManager.instance.timer;

        GameSystemInfo.Instance.UpdateTimer(m_Timer);


        Transform playerTransform = Controller.Instance.transform;


     
    }




}
