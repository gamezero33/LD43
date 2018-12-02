using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TODSky : MonoBehaviour 
{

	#region -| Instance |-
	private static TODSky m_Instance;
	private static TODSky instance
	{
		get
		{
			if ( !m_Instance )
			{
				m_Instance = FindObjectOfType<TODSky>();
			}
			return m_Instance;
		}
	}
	#endregion


	[SerializeField, Range( 0, 23 )]
	private int m_HoursOffset = 6;

	private int Hour
	{
		get
		{
			int hour = GameController.Hour + m_HoursOffset;
			while ( hour > 23 ) hour -= 24;
			while ( hour < 0 ) hour += 24;
			return hour;
		}
	}

	[SerializeField]
	private TweenSkyGrad[] m_SkyGrads;

	[SerializeField]
	private TweenSkyGrad[] m_SkyGradTweeners;

	[SerializeField]
	private Tweener[] m_SkyTweeners;


	private int m_SkyGradIndex = 0;

	[SerializeField, ReadOnly]
	private float m_DebugValue;


	public static void UpdateTweeners ()
	{
		instance.updateTweeners();
	}

	private void updateTweeners ()
	{
		if ( m_SkyGradTweeners != null && m_SkyGradTweeners.Length > 0 )
		{
			float duration = 24f / Mathf.Max( 1, m_SkyGradTweeners.Length );
			int index = Mathf.FloorToInt( Hour / duration );
			float factor = ( Hour + ( GameController.Minute / 60f ) ) % duration / duration;

			for ( int i = 0; i < m_SkyGradTweeners.Length; i++ )
			{
				if ( m_SkyGradTweeners[i] && m_SkyGradTweeners[i].gameObject )
				{
					m_SkyGradTweeners[i].gameObject.SetActive( index == i );
					if ( index == i )
					{
						m_SkyGradTweeners[i].Factor = factor;
					}
				}
			}
		}

		if ( m_SkyTweeners != null && m_SkyTweeners.Length > 0 )
		{
			float factor = ( Hour + ( GameController.Minute / 60f ) ) / 24f;
			m_DebugValue = factor;
			for ( int i = 0; i < m_SkyTweeners.Length; i++ )
			{
				if ( m_SkyTweeners[i] )
				{
					m_SkyTweeners[i].Factor = factor;
				}
			}
		}
	}


}
