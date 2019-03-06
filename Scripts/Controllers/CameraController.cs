using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	protected static CameraController _instance = null;
	public static CameraController instance 
	{
		get 
		{
			if (_instance == null) 
			{
				_instance = FindObjectOfType(typeof(CameraController)) as CameraController;
				if (_instance == null) 
				{
					_instance = Instantiate(Resources.Load("Controllers/CameraController", typeof(CameraController))) as CameraController;
					_instance.Init();
				}
			}
			return _instance;
		}
	}


	protected void Init() 
	{

	}
}
