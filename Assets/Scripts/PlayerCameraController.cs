using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour {

	public Camera cam;
	public Transform target;
	public float speedX = 360f;
	public float speedY = 240f;
	public float limitY = 40f;
	public float minDistance = 1.5f;
	private float _maxDistance;
	private Vector3 _LocalPosition;
	private float _currentYRotation;

	private Vector3 _position
	{
		get { return transform.position; }
		set { transform.position = value; } 
	}
	void Start () 
	{
		_LocalPosition = target.InverseTransformPoint (_position);	
	}
	

	void LateUpdate () 
	{
		_position = target.TransformPoint (_LocalPosition);
		CameraRotation ();
		_LocalPosition = target.InverseTransformPoint (_position);

	}

	void CameraRotation()
	{
		var mx = Input.GetAxis ("Mouse X");
		var my = Input.GetAxis ("Mouse Y");

		if (my != 0) {
			var tmp = Mathf.Clamp (_currentYRotation + my * speedY * Time.deltaTime, -limitY, limitY);
			if (tmp != _currentYRotation) {
				var rot = tmp - _currentYRotation;
				transform.RotateAround (target.position, transform.right, rot);
				_currentYRotation = tmp;
			}
		}
		if (mx != 0) {
			transform.RotateAround (target.position, Vector3.up, mx * speedX * Time.deltaTime);
		}
		transform.LookAt (target);
	}

}
