using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
	public Transform raposa;
	private Vector3 offset;

	void Start()
	{
		offset = transform.position - raposa.position;
	}

    void LateUpdate()
	{
		Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, raposa.position.z + offset.z);
		transform.position = newPosition;
	}
}
