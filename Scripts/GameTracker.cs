using UnityEngine;
using System.Collections;
using System;
using System.Text;

[Serializable]
public class GameTracker 
{
	private const string cGame_Tracker_Id = "testelicious";

	private static GameTracker instance = null;
	public static GameTracker get
	{
		get 
		{
			if (instance == null)
			{
				instance = Load ();
			}
			return instance;
		}
	}

	public GameTracker()
	{

	}

	protected static GameTracker Load()
	{
		if (instance == null)
		{
			if (PlayerPrefs.HasKey(cGame_Tracker_Id))
			{
				byte[] decodedBytes = Convert.FromBase64String(PlayerPrefs.GetString(cGame_Tracker_Id) );
				string decodedText = Encoding.UTF8.GetString(decodedBytes);

				//Serializer.DeSerialize<GameTracker>(PlayerPrefs.GetString(cGame_Tracker_Id), ref instance);
				Serializer.DeSerialize<GameTracker>(decodedText, ref instance);
			}
			else 
			{
				instance = new GameTracker();
			}
			instance.Save ();
		}

		return instance;
	}

	public void Save()
	{
		string s = string.Empty;
		Serializer.Serialize<GameTracker>(instance, ref s);

		byte[] bytesToEncode = Encoding.UTF8.GetBytes(s);
		string encodedString = Convert.ToBase64String(bytesToEncode);

		//PlayerPrefs.SetString(cGame_Tracker_Id, s);

		PlayerPrefs.SetString(cGame_Tracker_Id, encodedString);
	}

	public int highScore = 0;
	
	public int totalScore = 0;
	
	public int totalScreenTap = 0;

	public int triesSinceLastHighscore = 1;

	public int gamesPlayed = 0;

	public int bronzeMedals = 0;

	public int silverMedals = 0;

	public int goldMedals = 0;

	public int platinumMedals = 0;

	public bool CheckEndGameScore(int score)
	{
		totalScore += score;
		//Debug.Log("total score: " + totalScore + " highscore: " + highScore + " this round: " + score);
		if (score > highScore)
		{
			triesSinceLastHighscore = 1;
			highScore = score;
			instance.Save ();
			return true;
		}

		triesSinceLastHighscore++;
		instance.Save();
		return false;
	}

	public void AddMedalToTracker(MedalTexture medalTexture)
	{
		switch (medalTexture.medal)
		{
		case Medal.BRONZE:
			bronzeMedals++;
			break;
		case Medal.SILVER:
			silverMedals++;
			break;
		case Medal.GOLD:
			goldMedals++;
			break;
		case Medal.PLATINUM:
			platinumMedals++;
			break;
		}
		instance.Save();
		
	}

	public void NewGame()
	{
		gamesPlayed++;
		instance.Save();
	}
}