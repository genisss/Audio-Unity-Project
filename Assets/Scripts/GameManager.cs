using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour {

	enum State{
		START,
		PLAY,
		GAMEOVER,
	}

	public static float time;
	public float timeLimit = 30;
	const float waitTime = 5;
    public static int score = 0;
   

	MoleManager moleManager;
	//AudioSource audio;

	State state;
	public float timer;
    Vector2 touchPos;

    public static GameManager instance = null;
    //public ScoreManager scoreManager;
    public TextMeshProUGUI remainingTIme;
    //public ComboManager comboManager;


    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

    }

    void Start () 
	{
		Application.targetFrameRate = 60;

		this.state = State.START;
		this.timer = 0;
        score = 0;
		this.moleManager = this.GetComponent<MoleManager> ();

	}




    void Update () 
	{
		if (this.state == State.START) 
		{


                Debug.Log("PLAY!");
                this.state = State.PLAY;

                moleManager.SpawnMoles(100);

                // hide start label

				// start to generate moles
				this.moleManager.StartGenerate ();

			
		}
		else if (this.state == State.PLAY) 
		{	
			this.timer += Time.deltaTime;
			time = this.timer / timeLimit;
				
			if (this.timer > timeLimit) 
			{				
				this.state = State.GAMEOVER;

				// show gameover label

				// stop to generate moles
				this.moleManager.StopGenerate ();

				this.timer = 0;

                MusicManager mm = MusicManager.instance;
                mm.StartCoroutine(mm.FadeOut(mm.musicSource[1]));
                mm.StartCoroutine(mm.FadeOut(mm.musicSource[2]));

                mm.StartCoroutine(mm.FadeIn(mm.musicSource[0], mm.winMusic, 0.7f, false));

                // stop audio
            }

			//this.remainingTIme.text = ((int)(timeLimit-timer)).ToString ("D2");
		}
		else if (this.state == State.GAMEOVER) 
		{

            GameSystem.Instance.StopTimer();
            GameSystemInfo.Instance.FinalUI.SetActive(true);
            GameSystemInfo.Instance.FinalScore.text = score.ToString(); ;

            Controller.Instance.DisplayCursor(true);
			


		}
	}

    public void scorePoints(int coins)
    {
        score += coins;
        GameSystemInfo.Instance.UpdateScore(score);
        //comboManager.hitCombo();
    }

    public void missedHit()
    {
       // comboManager.ResetCombo();
    }
    
    public void decreaseTime()
    {
        timeLimit -= 2;
       // comboManager.ResetCombo();
    }
}
