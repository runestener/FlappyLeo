using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioController : Controller {

	[SerializeField]
	protected AudioSource audioSource;

	[SerializeField]
	protected List<AudioClip> audioClips = new List<AudioClip>();

	public override void Initialize ()
	{
		base.Initialize ();

		/*audioSource.volume = 0;

		foreach (AudioClip ac in audioClips)
		{
			PlaySound(ac);
		}

		audioSource.volume = 1;*/
	}

	public override void Deactivate ()
	{
		base.Deactivate ();
	}

	public bool PlaySound(string clipId)
	{
		AudioClip ac = audioClips.Find(a => a.ToString().Contains(clipId));

		if (ac != null)
		{
			return PlaySound(ac);
		}

		return false;
	}

	public bool PlaySound(AudioClip clip)
	{
		if (clip == null)
		{
			return false;
		}

		audioSource.PlayOneShot(clip);

		return true;
	}

}
