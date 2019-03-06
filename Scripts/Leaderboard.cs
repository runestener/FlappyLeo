using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;
using System;

public class Leaderboard : MonoBehaviour 
{
	protected Action callback = null;


	public void CheckLogin(Action callback = null)
	{
		if (!Social.localUser.authenticated)
		{
			this.callback = callback;
			Social.localUser.Authenticate (ProcessAuthentication);
		}
	}

	protected void ProcessAuthentication (bool success) {
		if (success) 
		{
			Global.Log ("Authenticated, checking score");
			
			// Request loaded achievements, and register a callback for processing them
			if (callback != null) 
			{
				Global.Log("Callback not null");
				callback();
				callback = null;
			}
			else 
			{
				Social.LoadScores(Global.LeaderboardID, ProcessLoadedScores);
			}
		}
		else
		{
			Global.Log ("Failed to authenticate");
		}
	}

	protected void ProcessLoadedScores(IScore[] scores)
	{
		if (scores.Length == 0)
		{
			Global.Log ("Error: no scores found");
		}
		else
		{
			Global.Log ("Got " + scores.Length + " scores");
		}
	}

	protected void ProcessLoadedAchievements (IAchievement[] achievements) 
	{

		if (achievements.Length == 0)
		{
			Global.Log ("Error: no achievements found");
		}
		else
		{
			Global.Log ("Got " + achievements.Length + " achievements");
		}
		
		// You can also call into the functions like this
		Social.ReportProgress ("Achievement01", 100.0, (bool result) => {
			if (result)
			{
				Debug.Log ("Successfully reported achievement progress");
			}
			else
			{
				Debug.Log ("Failed to report achievement");
			}
		});
	}
}
