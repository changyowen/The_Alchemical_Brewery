using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LookToCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () 
	{
		float rotateX = Camera.main.transform.rotation.eulerAngles.x;
		Vector3 rotationVector = new Vector3(rotateX, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
		transform.rotation = Quaternion.Euler(rotationVector);
	}
}
