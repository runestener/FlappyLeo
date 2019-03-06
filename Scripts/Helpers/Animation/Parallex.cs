using UnityEngine;
using System.Collections;

// make a cool parallex effect on sprites
public class Parallex : MonoBehaviour {

	[SerializeField]
	private float parallexSpeed_X = 0;
	//private float parallexSpeed_Y = 0;

	public bool isEnabled = true;
	
	void Update () 
	{
		if (!isEnabled) { return; }

		if (parallexSpeed_X != 0) 
		{
			this.transform.position += new Vector3(1 * parallexSpeed_X * Time.deltaTime, 0, 0 );
		}
	}
}
