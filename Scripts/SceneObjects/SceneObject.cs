using UnityEngine;
using System.Collections;

public class SceneObject : MonoBehaviour 
{

	protected Vector3 mInitialPosition = Vector3.zero;
	protected Quaternion mInitialRotation = Quaternion.identity;
	protected Vector3 mInitialScale;

	public virtual void Initialize()
	{
		mInitialPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		mInitialRotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
		mInitialScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
	}

	public virtual void Reset()
	{
		transform.position = mInitialPosition;
		transform.rotation = mInitialRotation;
		transform.localScale = mInitialScale;
	}
}
