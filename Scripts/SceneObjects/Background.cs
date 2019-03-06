using UnityEngine;
using System.Collections;

public class Background : SceneObject 
{

	public ResetPosition resetPosition;

	[SerializeField]
	protected Parallex mParallex;

	public override void Reset ()
	{
		base.Reset ();

		mParallex.isEnabled = true;
	}

}
