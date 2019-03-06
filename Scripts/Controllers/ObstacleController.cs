using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ObstacleController : Controller {

	protected const float cObstacle_distance = 3.2f;
	protected const float cObstacle_Max_Height = 3.65f;
	protected const float cObstacle_Min_Height = -2.8f;

	[SerializeField]
	protected List<Obstacle> _obstacles = new List<Obstacle>();

	public override void Initialize ()
	{
		base.Initialize ();

		foreach (Obstacle o in _obstacles)
		{
			o.GetComponent<ResetPosition>().resetEvent += ResetObstacle;
			o.Initialize();
		}
		FlappyManager.instance.gameStateChange_event += GameStateChanged;

		SetObstaclesInactive();
	}

	public override void Deactivate ()
	{
		base.Deactivate ();
	}

	public override void Reset()
	{
		foreach (Obstacle o in _obstacles)
		{
			o.Reset();
		}

		SetObstaclesInactive();
	}

	protected void GameStateChanged(GameState gs)
	{
		switch (gs)
		{
		case GameState.STARTMENU:
		case GameState.GAMEOVER:
			SetObstaclesInactive();
			break;
		case GameState.GAMERUNNING:
			SetObstaclesStart();
			break;
		case GameState.TUTORIAL:
		default:
			break;
		}
	}

	protected void SetObstaclesInactive()
	{
		foreach(Obstacle o in _obstacles)
		{
			o.SetObstacleState(false);
		}
	}

	protected void SetObstaclesStart()
	{
		foreach(Obstacle o in _obstacles)
		{
			o.SetObstacleState(true);
			o.transform.position += new Vector3(0, GetNewObstacleHeight(), 0);
		}
	}

	public void CollisionWithPrize(Component c)
	{
		if (c.GetComponent<Prize>() == null)
		{
			Global.Log("no prize component");

			return;
		}

		CollisionWithPrize(c.GetComponent<Prize>());
	}

	public void CollisionWithPrize(Prize prize)
	{
		prize.Deactivate();

		//Obstacle affectedObstacle = _obstacles.Find(o => o.prize == prize);

	}

	protected void ResetObstacle (ResetPosition obstacle)
	{
		Obstacle reset_Obstacle = obstacle.GetComponent<Obstacle>();

		Obstacle other_Obstacle = _obstacles.OrderByDescending(o => o.transform.position.x).FirstOrDefault();

		reset_Obstacle.transform.position =  new Vector3(other_Obstacle.transform.position.x + cObstacle_distance, GetNewObstacleHeight(), 0);

		reset_Obstacle.ResetObstacle();
	}

	protected float GetNewObstacleHeight()
	{
		return Random.Range(cObstacle_Min_Height, cObstacle_Max_Height);
	}
}
