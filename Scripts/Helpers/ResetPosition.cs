using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;

[RequireComponent(typeof(Parallex))]
public class ResetPosition : MonoBehaviour {

	public Action<ResetPosition> resetEvent;

	protected void Update () 
	{
		if (Camera.main.WorldToScreenPoint(new Vector3(x_position, this.transform.position.y,0)).x < 0f) 
		{
			//Global.Log("reset " + this);
			if (resetEvent != null)
			{
				resetEvent(this);
			}
		}
	}

	protected float X_Bound(float direction) 
	{
		float val = 0;
		if (direction != 0) 
		{
			val = direction > 0 ? 0 : Screen.width;
		}
		float returnval = Camera.main.ScreenToWorldPoint(new Vector3(val, 0, 0)).x;
		return returnval;
	}

	protected float x_position 
	{
		get 
		{
			return this.transform.position.x + this.spriteRenderer.bounds.extents.x;
		}
	}



	protected SpriteRenderer _spriteRenderer;
	public SpriteRenderer spriteRenderer 
	{
		get 
		{ 
			if (this._spriteRenderer == null) 
			{
				this._spriteRenderer = this.GetComponent<SpriteRenderer>();
				
				if (this._spriteRenderer == null)
				{
					List<SpriteRenderer> sprites = this.GetComponentsInChildren<SpriteRenderer>().ToList();
					this._spriteRenderer = sprites.OrderByDescending(s => s.bounds.max.x).First();
				}
			}
			return this._spriteRenderer;
		}
		set 
		{
			if (this._spriteRenderer == null) 
			{
				this._spriteRenderer = this.GetComponent<SpriteRenderer>();
				if (this._spriteRenderer == null)
				{
					List<SpriteRenderer> sprites = this.GetComponentsInChildren<SpriteRenderer>().ToList();
					this._spriteRenderer = sprites.OrderByDescending(s => s.bounds.max.x).First();
				}
			}
			this._spriteRenderer = value;
		}
	}

	protected Parallex _parallex;
	public Parallex parallex 
	{
		get 
		{
			if (this._parallex == null) 
			{
				this._parallex = this.GetComponent<Parallex>();
			}
			return this._parallex;
		}
		set 
		{
			if (this._parallex == null) 
			{
				this._parallex = this.GetComponent<Parallex>();
			}
			this._parallex = value;
		}
	}

}
