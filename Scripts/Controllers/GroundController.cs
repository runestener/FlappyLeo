using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class GroundController : Controller {

	[SerializeField]
	protected List<GroundPiece> _groundPieces = new List<GroundPiece>();
	[SerializeField]
	protected List<GroundGroup> _groundGroup = new List<GroundGroup>();


	public override void Initialize()
	{
		base.Initialize();

		foreach (GroundGroup gg in _groundGroup)
		{
			gg.Initialize();
		}

		FlappyManager.instance.gameStateChange_event += GameStateChanged;
	}

	public override void Reset ()
	{
		base.Reset ();

		foreach (GroundGroup gg in _groundGroup)
		{
			gg.Reset();
		}
	}

	public override void Deactivate ()
	{
		base.Deactivate();

		FlappyManager.instance.gameStateChange_event -= GameStateChanged;
	}

	public GroundGroup ResetGroundGroup(GroundGroup gg)
	{
		// get the group that is NOT the one inputted
		return _groundGroup.FirstOrDefault(g => g != gg);
	}

	public float GetGroundSurfacePosition()
	{
		GroundPiece gp = _groundPieces.FirstOrDefault();
		return gp.transform.position.y + gp.spriteRenderer.bounds.extents.y;
	}

	protected void GameStateChanged(GameState gs)
	{
		switch (gs)
		{
		case GameState.GAMEOVER:
			StopGrounds();
			break;
		case GameState.GAMERUNNING:
		case GameState.STARTMENU:
		case GameState.TUTORIAL:
			break;
		}
	}

	protected void StopGrounds()
	{
		foreach (GroundGroup gg in _groundGroup)
		{
			gg.StopGround();
		}
	}
}
