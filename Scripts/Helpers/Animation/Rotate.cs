using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

	public float y_Rotation = 0;

	public bool enabled_Rotation = true;

	void Start () {
	
	}
	

	void Update () {
		if (enabled_Rotation) 
		{
			if (y_Rotation != 0)
			{
				transform.RotateAround(transform.position, Vector3.up, y_Rotation * Time.deltaTime);
			}
		}
	}
}
