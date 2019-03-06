using UnityEngine;
using System.Collections;
using System;

public class Scale : MonoBehaviour 
{
	public float scalePercentage;
	public float timeToScale;

	public void ScaleDownAndUp()
	{
		Vector3 initialScale = new Vector3( transform.localScale.x, transform.localScale.y, transform.localScale.z);
		Vector3 downScale = new Vector3( transform.localScale.x * ( scalePercentage / 100), 
		                                transform.localScale.y * (scalePercentage / 100), 
		                                transform.localScale.z * (scalePercentage / 100));

		StartCoroutine( ScaleDownAndUpRoutine(initialScale, downScale) );

	}

	protected IEnumerator ScaleDownAndUpRoutine(Vector3 initialScale, Vector3 downScale)
	{
		while (true)
		{

			yield return StartCoroutine(ScaleRoutine(downScale));

			yield return StartCoroutine( ScaleRoutine(initialScale) );

			yield return new WaitForSeconds(2f);

		}

		yield break;
	}

	public void StartScaleDown(Action action = null)
	{
		StartScale(transform.localScale * (100 / scalePercentage), action);
	}

	public void StartScaleUp(Action action = null)
	{
		StartScale(transform.localScale * (1 + 100 / scalePercentage), action);
	}

	public void StartScale(Vector3 endScale, Action action = null )
	{
		StartCoroutine( ScaleRoutine(endScale, action) );
	}

	protected IEnumerator ScaleRoutine(Vector3 endScale, Action action = null)
	{
		Vector3 initialScale = transform.localScale;
		float endTime = Time.time + timeToScale;

		while (endTime > Time.time)
		{
			Vector3 lerpScale = Vector3.Lerp(initialScale, endScale, (timeToScale - (endTime- Time.time) / timeToScale));
			transform.localScale = lerpScale;
			yield return null;
		}

		transform.localScale = endScale;

		if (action != null)
		{
			action();
		}

		yield return null;
	}

}
