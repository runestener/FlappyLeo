using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameManager : MonoBehaviour {

	public Action<GameState> gameStateChange_event;
	public Action<int> scoreUpdated_event;

	protected static GameManager _instance = null;
	public static GameManager instance 
	{
		get 
		{
			if (_instance == null) 
			{
				_instance = FindObjectOfType(typeof(GameManager)) as GameManager;
				if (_instance == null) 
				{
					_instance = Instantiate(Resources.Load("Controllers/GameManager", typeof(GameManager))) as GameManager;
					_instance.Init();
				}
			}
			return _instance;
		}
	}

	void Awake ()
	{
		Init();
	}

	protected virtual bool Init() 
	{
		//Global.Log("Init game manager");
		if (wasInitialized)
		{
			return false;
		}

		wasInitialized = true;

		return true;
	}

	public float timeSinceStartup
	{
		get { return Time.realtimeSinceStartup; }
	}

	protected virtual void Update()
	{


	}

	protected bool wasInitialized = false;

	[HideInInspector]
	protected bool mGameRunning = false;
	public Action<bool> ChangedGameRunning;

	protected List<Controller> _controllers = new List<Controller>();


	public CType GetController<CType>() where CType : Controller
	{
		if (_controllers == null || _controllers.Count < 1)
		{
			Global.Log(LogCat.CONTROLLER, "Controller " + typeof(CType).ToString() + " has not been added");
			return null;
		}

		if (_controllers.FindAll(c => c.GetType() is CType).Count >= 2)
		{
			Global.LogWarning(LogCat.CONTROLLER, "Several controllers of type: " + typeof(CType).ToString() );
		}

		return (CType) _controllers.FindAll(c => c.GetType() == typeof(CType))[0];
	}

	public CType AddController<CType>(CType controller) where CType : Controller
	{
		if (_controllers != null && _controllers.Find(c => c.GetType() is CType) != null)
		{
			Global.Log(LogCat.CONTROLLER, "_controller list al ready has object of type: " + typeof(CType).ToString() );
			return GetController<CType>();
		}

		if (_controllers == null) 
		{
			Global.Log(LogCat.CONTROLLER, "Instantiating: _controller list");
			_controllers = new List<Controller>();
		}


		controller.Initialize();
		_controllers.Add( controller as CType);
		return controller;
	}


	public void StartGame()
	{
		mGameRunning = true;
		if (ChangedGameRunning != null)
		{
			ChangedGameRunning(mGameRunning);
		}
	}

	public void StopGame()
	{
		mGameRunning = false;
		if (ChangedGameRunning != null)
		{
			ChangedGameRunning(mGameRunning);
		}
	}
}