using UnityEngine;
using System.Collections;
using System;

public class GroundPiece : MonoBehaviour {

	public Action<GroundPiece> leftScreen;

	protected SpriteRenderer mSpriteRenderer;
	public SpriteRenderer spriteRenderer 
	{
		get 
		{ 
			if (this.mSpriteRenderer == null) 
			{
				this.mSpriteRenderer = this.GetComponent<SpriteRenderer>();
			}
			return this.mSpriteRenderer;
		}
		set 
		{
			if (this.mSpriteRenderer == null) 
			{
				this.mSpriteRenderer = this.GetComponent<SpriteRenderer>();
			}
			this.mSpriteRenderer = value;
		}
	}
}
