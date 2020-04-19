using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour {

	public static MusicManager instance;

    public AudioSource[] musicSource;
    public AudioClip insideMusic, outsideMusic, winMusic;

    public AudioMixerGroup track01, track02_stem01;

    public float fadeInTime = 1.0f;
    public float fadeOutTime = 1.0f;

   

    bool enemiesAround;

	void Awake ()
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

    // Use this for initialization
	void Start ()
	{
        musicSource = new AudioSource[2];

        musicSource[0] = gameObject.AddComponent<AudioSource>();
        musicSource[0].outputAudioMixerGroup = track01;

        musicSource[1] = gameObject.AddComponent<AudioSource>();
        musicSource[1].outputAudioMixerGroup = track02_stem01;

        StartCoroutine(FadeIn(musicSource[0], insideMusic, 1f));

    }
	
	// Update is called once per frame
	void Update ()
	{
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

		if (enemies.Length > 0)
		{
            enemiesAround = true; ;
		}
		else
		{
            enemiesAround = false;
		}

        if(enemiesAround)
        {
            EnemyIn();
        }
        else
        {
            NoEnemies();
        }
	}
	public void EnemyIn ()
	{
		//print("There are enemies following you");
	}

	public void NoEnemies ()
	{
        enemiesAround = false;
		//print("No more enemies around");
	}

    /*public void PlayLoopedMusic(AudioSource audiosource, AudioClip clip, float volume)
    {
        audiosource.volume = volume;
        audiosource.clip = clip;
        audiosource.loop = true;
        audiosource.Play();
    }*/

    public void PlayStinger(AudioClip clip, float volume)
    {
        AudioSource audiosource = musicSource[3];
        audiosource.volume = volume;
        audiosource.loop = false;
        audiosource.PlayOneShot(clip);
    }

    public IEnumerator FadeIn(AudioSource audiosource, AudioClip clip, float timeToStart, bool loop = true)
    {
        yield return new WaitForSeconds(timeToStart);
        audiosource.volume = 0f;
        audiosource.clip = clip;
        audiosource.loop = loop;
        audiosource.Play();

        while(audiosource.volume < 1.0f)
        {
            audiosource.volume += 1 * Time.deltaTime / fadeInTime;
            yield return null;
        }
        audiosource.volume = 1.0f;
    }

    public IEnumerator FadeOut(AudioSource audiosource)
    {
        audiosource.volume = 1f;

        while(audiosource.volume > 0.001f)
        {
            audiosource.volume -= 1 * Time.deltaTime / fadeOutTime;
            yield return null;
        }
        audiosource.volume = 0;
        audiosource.Stop();
    }
}
