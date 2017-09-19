using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour 
{
	[SerializeField]private float xOffset, yOffset, zOffset;
	[SerializeField]private Transform objectToFollow;

	private void LateUpdate()
	{
		this.transform.position = new Vector3 (objectToFollow.position.x - xOffset, objectToFollow.position.y - yOffset, objectToFollow.position.z - zOffset);
		this.transform.LookAt (objectToFollow.position);
	}
}
