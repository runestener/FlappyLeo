using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;

public class GuiManager : MonoBehaviour {

	protected bool mInstantiated = false;

	protected static GuiManager _instance = null;
	public static GuiManager instance 
	{
		get 
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(GuiManager)) as GuiManager;
				if (_instance == null)
				{
					GameObject go = Instantiate(Resources.Load("Controllers/GuiManager", typeof(GameObject))) as GameObject;
					_instance = go.GetComponent<GuiManager>();
					_instance.Init();
				}
			}
			return _instance;
		}
	}



	[SerializeField]
	protected List<MedalTexture> medals;

	[SerializeField]
	protected GameObject mInGamePanel;
	[SerializeField]
	protected GameObject mGameOverPanel;
	[SerializeField]
	protected GameObject mStartMenuPanel;
	[SerializeField]
	protected GameObject mTutorialPanel;
	[SerializeField]
	protected FadeEffect mTransitionPanel;
	[SerializeField]
	protected Text mInGameScore;
	[SerializeField]
	protected Text mGameOverScore;
	[SerializeField]
	protected Text mGameOverHighScore;
	[SerializeField]
	protected GameObject mGameOverNewScore;
	[SerializeField]
	protected Image mGameOverMedal;
	[SerializeField]
	protected FadeEffect mGameOverFade;
	[SerializeField]
	protected GameObject mGameOverButton;
	[SerializeField]
	protected GameObject mGameOverSummary;


	private int mGamesStarted;

	public void Init()
	{
		if (mInstantiated) 
		{
			return;
		}

		mInstantiated = true;

		FlappyManager.instance.gameStateChange_event += GameStateChanged;
		FlappyManager.instance.scoreUpdated_event += SetScore;

		GameStateChanged(GameState.STARTMENU);

		mGamesStarted = 0;

	}

	void Start () 
	{
		Init();
	
	}

	void Update() 
	{
		/*if (Input.GetKeyDown(KeyCode.S))
		{
			ButtonPlayNewGame();
		}

		if (Input.GetKeyDown(KeyCode.R))
		{
			ButtonPlayAgain();
		}*/
	}

	public void ButtonPlayNewGame()
	{
		ChangeScreenEffect(GameState.TUTORIAL);
		mGamesStarted++;

		Global.TrackingDesignEvent(string.Format("started game number {0} this session", mGamesStarted));
	}

	public void ButtonPlayAgain()
	{
		//FlappyManager.get.GetController<AdsController>().CloseAdd();

		foreach (MedalTexture mt in medals)
		{
			if (mt.particleSystem != null)
			{
				mt.particleSystem.SetActive(false);
			}
		}
		FlappyManager.get.ChangeGameState(GameState.STARTMENU);
		ChangeScreenEffect();
	}

	public void ButtonHighscore()
	{
		//Global.Log("highscore");
		if (!Social.localUser.authenticated)
		{
			FlappyManager.get.leaderboard.CheckLogin( () => Social.ShowLeaderboardUI());
		}
		else 
		{

			Social.ShowLeaderboardUI();
		}


	}

	public void ButtonSubmitScore()
	{
		if (!Social.localUser.authenticated)
		{
			FlappyManager.get.leaderboard.CheckLogin( () => {
				Social.Active.CreateLeaderboard();
				long score = (long) FlappyManager.get.score;
				Social.ReportScore(score, Global.LeaderboardID, HighScoreCheck);
				Social.Active.ShowLeaderboardUI();
			});
		}
		else 
		{
			long score = (long) FlappyManager.get.score;
			Social.ReportScore(score, Global.LeaderboardID, HighScoreCheck);
		}

	}

	protected void HighScoreCheck(bool result) 
	{
		if(result)
		{
			Social.ShowLeaderboardUI();
		}
		else
		{
			Debug.Log("score submission failed");
		}
	}

	public void ButtonRate()
	{
		//Global.Log("rate");
		#if UNITY_ANDROID
		Application.OpenURL("market://details?id=com.temphstudios.flappyleonardo");
		#elif UNITY_IPHONE
		Application.OpenURL("itms-apps://itunes.apple.com/app/flappy-leonardo");
		#endif
	}

	protected void GameStateChanged(GameState gs)
	{
		switch(gs)
		{
		case GameState.STARTMENU:
			mStartMenuPanel.SetActive(true);
			mInGamePanel.SetActive(false);
			mGameOverPanel.SetActive(false);
			mTutorialPanel.SetActive(false);
			break;
		case GameState.GAMERUNNING:
			mStartMenuPanel.SetActive(false);
			mGameOverPanel.SetActive(false);
			mInGamePanel.SetActive(true);
			//mTutorialPanel.SetActive(false);
			mTutorialPanel.GetComponent<FadeEffect>().FadeOut( () => mTutorialPanel.SetActive(false));
			break;
		case GameState.GAMEOVER:
			mStartMenuPanel.SetActive(false);
			mGameOverPanel.SetActive(true);
			mInGamePanel.SetActive(false);
			mTutorialPanel.SetActive(false);
			SetGameOverScreen();
			break;
		case GameState.TUTORIAL:
			mStartMenuPanel.SetActive(false);
			mGameOverPanel.SetActive(false);
			mInGamePanel.SetActive(true);
			mTutorialPanel.SetActive(true);
			mTutorialPanel.GetComponent<FadeEffect>().SetAlpha( 1f );
			//ChangeScreenEffect();
			mTutorialPanel.GetComponentInChildren<Scale>().ScaleDownAndUp();
			break;
		}
	}

	protected void SetGameOverScreen()
	{

		mGameOverFade.gameObject.SetActive(false);
		mGameOverButton.SetActive(false);
		mGameOverSummary.SetActive(false);

		ChangeScreenEffect( GameState.NONE, () => 
		                   	{
								mGameOverFade.gameObject.SetActive(true);
								mGameOverFade.FadeIn( StartGameOverScreen ) ;
							}
		);
	}

	protected void StartGameOverScreen()
	{
		StartCoroutine( GameOverScreenRoutine() );
	}

	protected IEnumerator GameOverScreenRoutine()
	{

		mGameOverSummary.SetActive(true);



		int score = FlappyManager.get.score;

		//score = 50;

		MedalTexture medalTexture = medals.FindAll(m => m.value <= score).Aggregate((n,o) => n.value > o.value ? n : o);
		
		if (medalTexture.medal != Medal.NONE) 
		{  
			FlappyManager.get.gameTracker.AddMedalToTracker(medalTexture);
			medalTexture.particleSystem.SetActive(true);
		} 
		
		mGameOverMedal.sprite = medalTexture.image;
		
		bool newHighScore = FlappyManager.get.gameTracker.CheckEndGameScore(score);

		mGameOverNewScore.SetActive( newHighScore );
		
		if(newHighScore)
		{
			string msg = string.Format("New highscore. Score {0} after {1} tries and time {2}",
			                           score, FlappyManager.get.gameTracker.triesSinceLastHighscore, FlappyManager.get.timeSinceStartup);
			Global.TrackingDesignEvent(msg);
		}
		
		mGameOverHighScore.text = FlappyManager.get.gameTracker.highScore.ToString();

		/*if (!newHighScore && FlappyManager.get.gameTracker.gamesPlayed % 3 == 0)
		{
			FlappyManager.get.GetController<AdsController>().RequestBannerGameOver();
		}*/

		yield return StartCoroutine( ScoreCountUp(score) );

		mGameOverButton.SetActive(true);

		yield break;
	}


	protected IEnumerator ScoreCountUp(int score)
	{

		for (int i = 0; i <= score; i++)
		{
			mGameOverScore.text = score.ToString();
			yield return new WaitForEndOfFrame();
			yield return new WaitForEndOfFrame();
			yield return new WaitForEndOfFrame();
		}
		yield return null;
	}

	protected void SetScore(int score)
	{
		mInGameScore.text = score.ToString();
	}

	public void ChangeScreenEffect(GameState newGS = GameState.NONE, Action action = null)
	{
		mTransitionPanel.gameObject.SetActive(true);
		mTransitionPanel.FadeIn( 
		                        () => 
		                        {
									if (newGS != GameState.NONE) { FlappyManager.get.ChangeGameState(newGS); }
									mTransitionPanel.FadeOut( 
			                         						() => 
										                        {
																	mTransitionPanel.gameObject.SetActive(false); 
																	if (action != null)
																	{
																		action();
																	}
																}
															);
								}
				); 
	}
}

