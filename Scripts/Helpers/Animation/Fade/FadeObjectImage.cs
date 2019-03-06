using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeObjectImage : FadeObject {

	protected Image image;
	
	void Awake ()
	{
		image = GetComponent<Image>();
	}
	
	public override void ChangeAlpha(float alpha)
	{
		if (image == null) { return; }

		Color c = new Color(image.color.r, image.color.g, image.color.b, alpha);
		
		image.color = c;
		
	}
}
