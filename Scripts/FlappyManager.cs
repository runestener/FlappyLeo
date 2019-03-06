using UnityEngine;
using System.Collections;
using System;

public class FlappyManager : GameManager {

	public static FlappyManager get
	{
		get { return instance as FlappyManager; }
	}

	protected GameState mGameState;
	public GameTracker gameTracker
	{
		get 
		{
			return GameTracker.get;
		}
	}


	public FlappyLeo Leo;
	public Leaderboard leaderboard;

	protected override bool Init ()
	{
		if (!base.Init () ) { return false; }

		AddController<GroundController>( GetComponent<GroundController>() );
		AddController<BackgroundController>( GetComponent<BackgroundController>() );
		AddController<ObstacleController>( GetComponent<ObstacleController>() );
		AddController<AudioController>( GetComponent<AudioController>() );

		Leo.Initialize();

		//PlayerPrefs.DeleteAll();

		GuiManager.instance.Init();

		//gameTracker.Save();


		//Leo.gameObject.SetActive(false);
		return true;
	}
	protected override void Update ()
	{
		base.Update ();

		if (mGameState == GameState.TUTORIAL)
		{
			if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
			{
				ChangeGameState(GameState.GAMERUNNING);
			}
		}
	}

	protected int mScore = 0;
	public int score 
	{
		get 
		{
			return mScore;
		}
		set 
		{
			mScore = value;
			if (scoreUpdated_event != null)
			{
				scoreUpdated_event(mScore);
			}
		}
	}

	public void AddPoint()
	{
		GetController<AudioController>().PlaySound("point");

		score++;
	}

	public bool ChangeGameState(GameState gs)
	{

		//Global.Log("changing gamestate to: " + gs);
		mGameState = gs;
		switch (gs)
		{
		case GameState.STARTMENU:
			GetController<AudioController>().PlaySound("swooshing");
			Reset ();
			Leo.gameObject.SetActive(false);
			break;
		case GameState.GAMERUNNING:
			GetController<AudioController>().PlaySound("oscar");
			Leo.bounce.enabled_bounce = false;
			StartGame();
			break;
		case GameState.GAMEOVER:
			Global.TrackingProgressEventFail(string.Format("Game over - Score is {0}", score));
			StopGame();
			break;
		case GameState.TUTORIAL:
			gameTracker.NewGame();
			Global.TrackingProgressEvent(GameAnalyticsSDK.GAProgressionStatus.Start, 
			                             string.Format("Start game event - game number {0}", gameTracker.gamesPlayed ));
			GetController<AudioController>().PlaySound("swooshing");
			score = 0;
			Leo.gameObject.SetActive(true);
			Leo.bounce.enabled_bounce = true;
			break;
		}

		if (gameStateChange_event != null)
		{
			gameStateChange_event(mGameState);
		}
		return true;
	}

	protected void Reset()
	{
		Leo.Reset();

		GetController<ObstacleController>().Reset();
		GetController<GroundController>().Reset();
		GetController<BackgroundController>().Reset();
	}
}
