using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour 
{

	private Camera m_Camera;
	private Camera Cam
	{
		get
		{
			if ( !m_Camera )
			{
				m_Camera = Camera.main;
			}
			return m_Camera;
		}
	}

	private void Update ()
	{
		transform.LookAt( Cam.transform );
	}

}
