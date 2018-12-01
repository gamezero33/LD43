using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour 
{

	#region -| Instance |-
	private static GameController m_Instance;
	private static GameController instance
	{
		get
		{
			if ( !m_Instance )
			{
				m_Instance = FindObjectOfType<GameController>();
			}
			return m_Instance;
		}
	}
	#endregion



}
