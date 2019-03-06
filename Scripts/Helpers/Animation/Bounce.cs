using UnityEngine;
using System.Collections;

public class Bounce : MonoBehaviour {

	public float y_Bounce = 0;
	public float y_Speed = 0;

	public float x_Bounce = 0;
	public float x_Speed = 0;

	public bool enabled_bounce = false;



	protected int y_direction;

	protected Vector2 initialPosition;

	void Start()
	{
		//enabled = true;

		Reset();

		y_direction = 1;

	}

	public void Reset()
	{
		
		initialPosition = transform.position;
	}

	void Update () 
	{
		if (enabled_bounce)
		{
			if (y_Bounce != 0)
			{
				if (transform.position.y >= initialPosition.y + y_Bounce) { y_direction = -1; } 
				else if (transform.position.y <= initialPosition.y - y_Bounce) { y_direction = 1; }


				transform.position = Vector3.MoveTowards(transform.position, 
				                                         new Vector3(transform.position.x, transform.position.y + y_Bounce*y_direction, transform.position.z), 
				                                         y_Speed*Time.deltaTime);
			}
		}
	}
}
