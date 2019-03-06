using UnityEngine;
using System.Collections;

public class FlappyLeo : SceneObject {

	protected const float cRotation_Death_Time = 0.60f;
	protected const float cRotation_Max = 45f;
	protected const float cRotation_Min = -90f;

	private float mRotationRange;
	private float mVelocityRange;

	protected bool mGameRunning = false;

	public Bounce bounce;

	[SerializeField]
	protected Animator animator;

	protected float mVelocity;
	[SerializeField]
	protected float mVelocityMinimum;
	[SerializeField]
	protected float mVelocityMaximum;
	[SerializeField]
	protected float mVelocityIncrease;
	[SerializeField]
	protected float mVelocityDecrease;

	public override void Initialize ()
	{
		base.Initialize ();

		FlappyManager.instance.ChangedGameRunning += (bool b) => mGameRunning = b;
		
		mRotationRange = cRotation_Max - cRotation_Min;
		mVelocityRange = mVelocityMaximum - mVelocityMinimum;
	}
	

	void Update () 
	{

		if (mGameRunning) 
		{
			UserInput();

			DecreaseVelocity();

			transform.position += new Vector3(0, mVelocity * Time.deltaTime, 0); 

			SetLeoRotation();
		}
	}


	protected void AddUpVelocity()
	{
		if (mVelocity < 0)
		{
			mVelocity = 0;
		}

		//mVelocity = Mathf.Clamp(mVelocity + (mVelocityIncrease * Time.deltaTime) , mVelocityMinimum, mVelocityMaximum);
		mVelocity = mVelocityMaximum;
	}

	private void UserInput()
	{
		if ( Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) ) 
		{
			FlappyManager.get.GetController<AudioController>().PlaySound("wing");

			FlappyManager.get.gameTracker.totalScreenTap++;

			AddUpVelocity();
		}
	}

	private void DecreaseVelocity()
	{
		mVelocity = Mathf.Clamp(mVelocity - (mVelocityDecrease * Time.deltaTime), mVelocityMinimum, mVelocityMaximum);
	}

	protected void CollisionPrize(Transform prize)
	{
		//Global.Log("collision with prize");
		FlappyManager.instance.GetController<ObstacleController>().CollisionWithPrize(prize);

		FlappyManager.get.AddPoint();
	}

	protected void CollisionKiller(Transform killer)
	{
		//Global.Log("collision with killer");
		FlappyManager.get.ChangeGameState(GameState.GAMEOVER);

		FlappyManager.get.GetController<AudioController>().PlaySound("inception");

		animator.SetBool("leo_death", true);

		StartCoroutine( DeathSequenceFall() );

		StartCoroutine( "DeathSequenceRotate" );
	}

	protected IEnumerator DeathSequenceRotate()
	{
		Quaternion initialRotation = transform.rotation;

		Quaternion endRotation = Quaternion.Euler(new Vector3(0, 0, -90));

		float endTime = Time.time + cRotation_Death_Time;

		while (endTime > Time.time)
		{
			transform.rotation = Quaternion.Lerp(initialRotation, endRotation, (cRotation_Death_Time - (endTime - Time.time))/cRotation_Death_Time);
			yield return null;
		}

		transform.rotation = endRotation;
		yield return null;
	}

	protected IEnumerator DeathSequenceFall()
	{
		float endHeight = FlappyManager.instance.GetController<GroundController>().GetGroundSurfacePosition() + spriteRenderer.bounds.extents.x;//*1.10f;

		mVelocity = -1;

		while (transform.position.y > endHeight)
		{
			DecreaseVelocity();
			
			transform.position += new Vector3(0, mVelocity * Time.deltaTime, 0); 
			yield return null;
		}

		StopCoroutine("DeathSequenceRotate");

		yield return null;
	}

	protected void SetLeoRotation()
	{
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, mVelocity * (mRotationRange / mVelocityRange)));
	}


	void OnTriggerEnter2D(Collider2D other)
	{
		if (!mGameRunning)  { return; }

		if (other.tag == Global.tag_LeoKiller) 
		{
			CollisionKiller(other.transform);
		}
		else if (other.tag == Global.tag_Prize)
		{
			CollisionPrize(other.transform);
		}
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
}
