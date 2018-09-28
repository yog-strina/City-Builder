using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class CameraController : MonoBehaviour
{

	public float rotationSpeed = 3f;
	private Vector3 mouseOriginPoint;
	private Vector3 cameraOriginPoint = new Vector3(-33f, 57f, -33f);
	private Vector3 cameraOriginRotation = new Vector3(45, 45, 0);
	private Vector3 offset;
	private bool isDragging = false;
	private float yaw = 45.0f;
	// Use this for initialization
	void Start ()
	{
		Camera.main.orthographicSize = 10;
		cameraOriginRotation = Camera.main.transform.eulerAngles;
	}
	
	// Update is called once per frame
	void Update ()
	{
		Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * Camera.main.orthographicSize * .5f, 3f, 20f);
		if (Input.GetMouseButton(2) || (Input.GetMouseButton(1) && Input.GetKey(KeyCode.LeftAlt)))
		{
			if (!isDragging)
			{
				mouseOriginPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				isDragging = true;
			}

			if (isDragging)
			{
				yaw += rotationSpeed * Input.GetAxis("Mouse X");
				Camera.main.transform.eulerAngles = new Vector3(cameraOriginRotation.x, yaw, cameraOriginRotation.z);
			}
		}
		else if (Input.GetMouseButton(1))
		{
			offset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
			if (!isDragging)
			{
				isDragging = true;
				mouseOriginPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			}

			if (isDragging)
			{
				transform.position = mouseOriginPoint - offset;
			}
		}
		else
		{
			isDragging = false;
		}
	}
}
