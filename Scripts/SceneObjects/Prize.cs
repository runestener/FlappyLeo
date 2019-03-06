using UnityEngine;
using System.Collections;

public class Prize : MonoBehaviour {

	/*[SerializeField]
	protected ParticleSystem particleSystem;
	[SerializeField]
	protected ParticleEmitter particleEmitter;*/
	[SerializeField]
	protected Collider2D collider2d;

	public void Deactivate()
	{
		//Global.Log("Deactivate");
		spriteRenderer.enabled = false;
		collider2d.enabled = false;
		//particleEmitter.emit = true;
		//particleSystem.Play();
	}


	public void Activate()
	{
		spriteRenderer.enabled = true;
		collider2d.enabled = true;

		//bounce.Reset();
	}


	protected IEnumerator ParticleCoRoutine()
	{
		yield return null;
	}

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

	protected Bounce mBounce;
	public Bounce bounce 
	{
		get 
		{
			if (mBounce == null)
			{
				mBounce = GetComponent<Bounce>();
			}
			return mBounce;
		}
		set 
		{
			if (mBounce == null)
			{
				mBounce = GetComponent<Bounce>();
			}
			mBounce = value;
		}
	}

}
