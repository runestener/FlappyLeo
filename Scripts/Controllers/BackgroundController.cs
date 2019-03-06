using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BackgroundController : Controller {

	[SerializeField]
	protected List<Background> mBackgrounds = new List<Background>();



	public override void Initialize ()
	{
		base.Initialize();

		foreach(Background b in mBackgrounds)
		{
			b.resetPosition.resetEvent += ResetBackground;
			b.Initialize();
		}

		FlappyManager.instance.gameStateChange_event += GameStateChanged;
	}

	public override void Reset ()
	{
		base.Reset ();

		foreach(Background b in mBackgrounds)
		{
			b.Reset();
		}
	}


	public override void Deactivate ()
	{
		throw new System.NotImplementedException ();
	}

	protected void ResetBackground(ResetPosition rp)
	{
		ResetPosition other = mBackgrounds.OrderByDescending(r => r.transform.position.x).First().resetPosition;

		Vector3 newPosition = other.transform.position;

		rp.transform.position = new Vector3(newPosition.x + rp.spriteRenderer.bounds.extents.x*2 - 0.05f, rp.transform.position.y, rp.transform.position.z);
	}

	protected void GameStateChanged(GameState gs)
	{
		switch (gs)
		{
		case GameState.GAMEOVER:
			StopBackgrounds();
			break;
		case GameState.GAMERUNNING:
		case GameState.STARTMENU:
		case GameState.TUTORIAL:
			break;
		}
	}
	
	protected void StopBackgrounds()
	{
		foreach (Background b in mBackgrounds)
		{
			b.GetComponent<Parallex>().isEnabled = false;
		}
	}
}
