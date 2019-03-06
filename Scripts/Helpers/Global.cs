using UnityEngine;
using System.Collections;
using System;
using GameAnalyticsSDK;


public enum LogCat { DEFAULT, GUI, NETWORK, GAME, CONTROLLER }

// Uniform input type to ensure same behaviour on different platforms
public enum InputType { DOWN = 0, HOLD = 1, MOVED = 2, RELEASE = 3 }

public enum GameState { STARTMENU = 0, GAMERUNNING = 1, GAMEOVER = 2, TUTORIAL = 3, NONE = 100 }

public enum Medal { NONE = 0, BRONZE = 1, SILVER = 2, GOLD = 3, PLATINUM = 4 }

public static class Global
{
	private const string leaderboardIdiOS = "temphstudio_flappyleo_leaderboard";
	private const string leaderboardIdAndroid = "CgkI8Ouwk6oVEAIQAA";
	public const string tag_LeoKiller = "LeoKiller";
	public const string tag_Prize = "Prize";

	public static string LeaderboardID 
	{
		get 
		{
			return (Application.platform == RuntimePlatform.IPhonePlayer ? leaderboardIdiOS : leaderboardIdAndroid);
		}
	}

	public static void Log(string message)
	{
		Log (LogCat.DEFAULT, message);
	}

	public static void Log(LogCat category, string message)
	{
		// we would also like to include a timestamp since startup at the beginning of a debug
		string s = category != LogCat.DEFAULT ? category.ToString() : "";
		Debug.Log(string.Format("{0} -- {1} ", s, message) );
	}



	public static void LogWarning(string message)
	{
		LogWarning (LogCat.DEFAULT, message);
	}

	public static void LogWarning(LogCat category, string message)
	{
		// we would also like to include a timestamp since startup at the beginning of a debug
		string s = category != LogCat.DEFAULT ? category.ToString() : "";
		Debug.LogWarning(string.Format("{0} -- {1} ", s, message) );
	}



	public static void LogError(string message)
	{
		LogError(LogCat.DEFAULT, message);
	}

	public static void LogError(LogCat category,string message)
	{
		// we would also like to include a timestamp since startup at the beginning of a debug
		string s = category != LogCat.DEFAULT ? category.ToString() : "";
		Debug.LogError(string.Format("{0} -- {1} ", s, message) );
	}



	public static void TrackingBusinessEvent()
	{

	}



	public static void TrackingProgressEventStart(string value)
	{
		GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, value);
	}

	public static void TrackingProgressEventFail(string value)
	{
		GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, value);
	}

	public static void TrackingProgressEvent(GAProgressionStatus status, string value)
	{
		GameAnalytics.NewProgressionEvent(status, value);
	}

	public static void TrackingDesignEvent(string value)
	{
		GameAnalytics.NewDesignEvent(value);
	}


}


[Serializable]
public class MedalTexture
{
	public Medal medal;
	public int value;
	public Sprite image;
	public GameObject particleSystem;
}