using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class FadeEffect : MonoBehaviour 
{
	public Action finishedFadeIn;
	public Action finishedFadeOut;

	[SerializeField]
	protected float fadeInTime;
	[SerializeField]
	protected float fadeOutTime;

	[SerializeField]
	protected List<FadeObject> objectsToFade = new List<FadeObject>();

//	protected abstract void ChangeAlpha(float alpha);

	public void SetAlpha(float alpha)
	{
		foreach(FadeObject f in objectsToFade)
		{
			if (f == null)
			{
				continue;
			}
			
			f.ChangeAlpha(alpha);
		}
	}

	public void FadeIn(Action finishFadeAction = null)
	{
		StartCoroutine( FadeRoutine(fadeOutTime, 1, finishFadeAction) );
	}

	public void FadeOut(Action finishFadeAction = null)
	{
		StartCoroutine( FadeRoutine(fadeOutTime, 0, finishFadeAction) );
	}

	protected IEnumerator FadeRoutine(float fadeTime, float fadeTo, Action finishFade = null)
	{
		float initialFadeValue = fadeTo == 1 ? 0 : 1;
		float endTime = Time.time + fadeTime;


		while (endTime >= Time.time) 
		{
			foreach(FadeObject f in objectsToFade)
			{
				if (f == null)
				{
					continue;
				}


				float alpha = Mathf.Lerp(initialFadeValue, fadeTo, (fadeTime - (endTime - Time.time))/fadeTime);

				f.ChangeAlpha(alpha);

			}
			yield return null;
		}

		if (finishFade != null)
		{
			finishFade();
		}

		yield break;
	}


}
