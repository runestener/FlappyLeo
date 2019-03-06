using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeObjectText : FadeObject {

	protected Text text;

	void Awake ()
	{
		text = GetComponent<Text>();
	}

	public override void ChangeAlpha(float alpha)
	{
		Color c = new Color(text.color.r, text.color.g, text.color.b, alpha);
			
		text.color = c;
		
	}
}
