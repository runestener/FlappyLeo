using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Obstacle : SceneObject {

	public Prize prize;

	[SerializeField]
	protected Parallex _parallex;

	public void ResetObstacle()
	{
		prize.Activate();
	}

	public void SetObstacleState(bool b)
	{
		_parallex.isEnabled = b;
	}

	public override void Reset ()
	{
		base.Reset();

		prize.Activate();
	}
}
