using UnityEngine;
using System.Collections;

public class ParticleTester : MonoBehaviour {

	public GameObject particle1;
	public GameObject particle2;
	public GameObject particle3;
	public GameObject particle4;

	void Update () 
	{
	
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			particle1.SetActive( !particle1.activeInHierarchy);
		}

		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			particle2.SetActive( !particle2.activeInHierarchy);
		}

		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			particle3.SetActive( !particle3.activeInHierarchy);
		}

		if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			particle4.SetActive( !particle4.activeInHierarchy);
		}
	}
}
