using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour 
{

	[SerializeField]
	private Vector3[] m_Doors;


	private void OnDrawGizmosSelected ()
	{
		if ( m_Doors == null || m_Doors.Length <= 0 ) return;

		Gizmos.color = Color.green;
		for ( int i = 0; i < m_Doors.Length; i++ )
		{
			Gizmos.DrawSphere( transform.TransformPoint( m_Doors[i] ), 0.15f );
		}
	}

}
