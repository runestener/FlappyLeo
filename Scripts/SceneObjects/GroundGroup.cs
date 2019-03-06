using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class GroundGroup : SceneObject {

	public List<ResetPosition> resets = new List<ResetPosition>();

	public List<GroundPiece> pieces = new List<GroundPiece>();

	public override void Initialize ()
	{
		base.Initialize ();

		foreach (ResetPosition rp in resets)
		{
			rp.resetEvent += RequestedReset;
		}
	}

	public override void Reset ()
	{
		base.Reset ();

		parallex.isEnabled = true;
	}

	/*public void Setup()
	{
		foreach (ResetPosition rp in resets)
		{
			rp.resetEvent += RequestedReset;
		}
	}*/

	protected void RequestedReset(ResetPosition reset)
	{

		GroundGroup gg = GameManager.instance.GetController<GroundController>().ResetGroundGroup(this);

		// get the position of the resetposition with the highest x value (right most) 
		Vector2 position = gg.resets.Aggregate((g1, g2) => g1.transform.position.x > g2.transform.position.x ? g1 : g2).transform.position;

		GroundPiece rp = pieces.Aggregate((g1, g2) => g1.transform.position.x < g2.transform.position.x ? g1 : g2);

		float distance = Vector2.Distance(rp.transform.position, transform.position);

		transform.position = new Vector3(position.x + distance, transform.position.y, transform.position.z);

	}

	public void StopGround()
	{
		parallex.isEnabled = false;
	}

	protected Parallex mParallex;
	public Parallex parallex
	{
		get 
		{
			if (mParallex == null)
			{
				mParallex = GetComponent<Parallex>();
			}
			return mParallex;
		}
		set 
		{
			if (mParallex == null)
			{
				mParallex = GetComponent<Parallex>();
			}
			mParallex = value;
		}
	}
}
