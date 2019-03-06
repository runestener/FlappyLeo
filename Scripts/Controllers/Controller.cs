using UnityEngine;
using System.Collections;
using System;

public abstract class Controller : MonoBehaviour
{
	public virtual void Initialize ()
	{
		//Global.Log( "Initializing " + this.GetType().ToString() );
	}

	public virtual void Deactivate ()
	{

	}

	public virtual void Reset()
	{

	}

}
