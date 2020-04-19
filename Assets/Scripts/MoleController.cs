using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MoleController : MonoBehaviour {

	public float moveSpeed = 1000f;
	public float waitTime = 2.0f;
    public float bunnyPercentatge;
    public CanvasMoleController canvasMoleController;


	private const float TOP = 0.0f;
	private const float BOTTOM = -1.65f;
	private float tmpTime = 0;
    private Transform player;

    public GameObject particleCoins, particleCarrots;



    [SerializeField]
    private GameObject gopher, bunny;

	public enum State{
		UNDER_GROUND,
		UP,
		ON_GROUND,
		DOWN,
		HIT,
	}
	public State state;

    public enum HitResult
    {
        NULL,
        GOPHER,
        BUNNY
    }

    public SFXSettings hitSFXSettings = new SFXSettings();

    public AudioMixer mainMixer;
    public float reverbValue = 0;

    public void PlayHitSFX()
    {
      AudioSourcePool.instance.PooledRandomSFX(gameObject, mainMixer, "Enemies",
            hitSFXSettings.minPitch, hitSFXSettings.maxPitch, hitSFXSettings.clip01, hitSFXSettings.clip01MinVolume, hitSFXSettings.clip01MaxVolume,
            hitSFXSettings.spatialBlend, hitSFXSettings.spread, hitSFXSettings.dopplerLevel,
            hitSFXSettings.rollofMode, hitSFXSettings.minDistance, hitSFXSettings.maxDistance, reverbValue);

    }   
    public void PlayHitBunnySFX()
    {
        AudioSourcePool.instance.PooledRandomSFX(gameObject, mainMixer, "Enemies",
            hitSFXSettings.minPitch, hitSFXSettings.maxPitch, hitSFXSettings.clip02, hitSFXSettings.clip02MinVolume, hitSFXSettings.clip02MaxVolume,
            hitSFXSettings.spatialBlend, hitSFXSettings.spread, hitSFXSettings.dopplerLevel,
            hitSFXSettings.rollofMode, hitSFXSettings.minDistance, hitSFXSettings.maxDistance, reverbValue);

    }


    public void Up()
	{
		if (this.state == State.UNDER_GROUND) 
		{
			this.state = State.UP;
            if(Random.Range(0,100) < bunnyPercentatge)
            {
                bunny.SetActive(true);
                gopher.SetActive(false);
            }
            else
            {
                bunny.SetActive(false);
                gopher.SetActive(true);
            }
		}
	}

	public HitResult Hit(out int coins)
	{
        coins = 0;
		if (this.state == State.UNDER_GROUND) 
		{
			return HitResult.NULL;
		}
        Vector3 pos = transform.position;
		transform.localPosition = 
			new Vector3(transform.localPosition.x, BOTTOM, transform.localPosition.z);

		this.state = State.UNDER_GROUND;
        if (gopher.activeSelf)
        {

            Instantiate(this.particleCoins, pos, Quaternion.identity);
            PlayHitSFX();

            coins = (int)(((waitTime - tmpTime) / waitTime) * 50) + 50; // Retorna un nombre entre 50 - 100
            canvasMoleController.openPointsPanel(coins);
            return HitResult.GOPHER;
        }
        else
        {
            Instantiate(this.particleCarrots, pos, Quaternion.identity);
            PlayHitBunnySFX();
            canvasMoleController.openDecreaseTimePanel();
            return HitResult.BUNNY;
        }
	}
		
	void Start () 
	{
        transform.localPosition = new Vector3(transform.localPosition.x, BOTTOM, transform.localPosition.z);
        this.state = State.UNDER_GROUND;
        player = GameObject.FindGameObjectWithTag("Player").transform;
	}
		
	void Update () 
	{

		if (this.state == State.UP) 
		{
			transform.Translate (0, this.moveSpeed * Time.deltaTime, 0);

			if (transform.localPosition.y > TOP) 
			{
				transform.localPosition = 
					new Vector3 (transform.localPosition.x, TOP, transform.localPosition.z);

				this.state = State.ON_GROUND;

				this.tmpTime = 0;
			}
		} 
		else if (this.state == State.ON_GROUND)
		{
			this.tmpTime += Time.deltaTime;

			if (this.tmpTime > this.waitTime) 
			{
				this.state = State.DOWN;
			}
		}
		else if (this.state == State.DOWN) 
		{
			transform.Translate (0, -this.moveSpeed * Time.deltaTime, 0);

			if (transform.localPosition.y < BOTTOM) 
			{
				transform.localPosition = 
					new Vector3(transform.localPosition.x, BOTTOM, transform.localPosition.z);

				this.state = State.UNDER_GROUND;
			}
		}
        Vector3 relativePos = player.position - transform.position;
        Quaternion LookAtRotation = Quaternion.LookRotation(relativePos);

        Quaternion LookAtRotationOnly_Y = Quaternion.Euler(transform.rotation.eulerAngles.x, LookAtRotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        transform.rotation = LookAtRotationOnly_Y;
    }

    public void Explode()
    {
        if (this.state == State.UNDER_GROUND)
        {
            return;
        }
        transform.localPosition =
            new Vector3(transform.localPosition.x, BOTTOM, transform.localPosition.z);

        this.state = State.UNDER_GROUND;

        if (gopher.activeSelf)
        {
            int coins = (int)(((waitTime - tmpTime) / waitTime) * 50) + 50;
            canvasMoleController.openPointsPanel(coins);
            GameManager.instance.scorePoints(coins);
            GameManager.instance.missedHit();
        }

    }
}
