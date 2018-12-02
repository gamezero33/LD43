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


	[System.Serializable]
	public struct TimeSpeed
	{
		public string text;
		public float timeScale;
		public Color color;
	}


	[SerializeField]
	private float m_TimeScale = 20;
	public static float TimeScale
	{
		get
		{
			return instance.m_TimeScale;
		}
		set
		{
			instance.m_TimeScale = value;
			Time.timeScale = value / 5;
		}
	}

	[SerializeField]
	private int m_Day = 0;
	public static int Day
	{
		get
		{
			return instance.m_Day;
		}
		set
		{
			instance.m_Day = value;
		}
	}

	[SerializeField, Range( 0, 23 )]
	private int m_Hour = 6;
	public static int Hour
	{
		get
		{
			return instance.m_Hour;
		}
		set
		{
			instance.m_Hour = value;
		}
	}

	[SerializeField, Range( 0, 59 )]
	private float m_Minute = 0;
	public static float Minute
	{
		get
		{
			return instance.m_Minute;
		}
		set
		{
			instance.m_Minute = value;
		}
	}


	[SerializeField, Header( "Time Scale Speeds" )]
	private TimeSpeed[] m_Speeds;

	[SerializeField]
	private int m_CurrentSpeedIndex = 0;
	public static TimeSpeed CurrentSpeed
	{
		get
		{
			return instance.m_Speeds[instance.m_CurrentSpeedIndex];
		}
	}

	public static TimeSpeed PrevSpeed
	{
		get
		{
			return instance.m_Speeds[Mathf.Max( 0, instance.m_CurrentSpeedIndex - 1 )];
		}
	}

	public static TimeSpeed NextSpeed
	{
		get
		{
			return instance.m_Speeds[Mathf.Min( instance.m_Speeds.Length - 1, instance.m_CurrentSpeedIndex + 1 )];
		}
	}

	public static void IncreaseSpeed ()
	{
		if ( instance.m_CurrentSpeedIndex >= instance.m_Speeds.Length - 1 ) return;
		instance.m_CurrentSpeedIndex++;
		TimeScale = CurrentSpeed.timeScale;
	}

	public static void DecreaseSpeed ()
	{
		if ( instance.m_CurrentSpeedIndex == 0 ) return;
		instance.m_CurrentSpeedIndex--;
		TimeScale = CurrentSpeed.timeScale;
	}


	private int m_LastSpeedIndex;

	private void Start ()
	{
		TimeScale = CurrentSpeed.timeScale;
		UIManager.UpdateSpeedDisplay();
	}

	private void Update ()
	{
		if ( Input.GetKeyDown( KeyCode.P ) )
		{
			if ( m_CurrentSpeedIndex == 0 )
			{
				m_CurrentSpeedIndex = m_LastSpeedIndex;
			}
			else
			{
				m_LastSpeedIndex = m_CurrentSpeedIndex;
				m_CurrentSpeedIndex = 0;
			}
			TimeScale = CurrentSpeed.timeScale;
			UIManager.UpdateSpeedDisplay();
		}
		if ( Input.GetKeyDown( KeyCode.Alpha1 ) )
		{
			m_CurrentSpeedIndex = 0;
			TimeScale = CurrentSpeed.timeScale;
			UIManager.UpdateSpeedDisplay();
		}
		if ( Input.GetKeyDown( KeyCode.Alpha2 ) )
		{
			m_CurrentSpeedIndex = 1;
			TimeScale = CurrentSpeed.timeScale;
			UIManager.UpdateSpeedDisplay();
		}
		if ( Input.GetKeyDown( KeyCode.Alpha3 ) )
		{
			m_CurrentSpeedIndex = 2;
			TimeScale = CurrentSpeed.timeScale;
			UIManager.UpdateSpeedDisplay();
		}
		if ( Input.GetKeyDown( KeyCode.Alpha4 ) )
		{
			m_CurrentSpeedIndex = 3;
			TimeScale = CurrentSpeed.timeScale;
			UIManager.UpdateSpeedDisplay();
		}
		if ( Input.GetKeyDown( KeyCode.Alpha5 ) )
		{
			m_CurrentSpeedIndex = 4;
			TimeScale = CurrentSpeed.timeScale;
			UIManager.UpdateSpeedDisplay();
		}
		if ( Input.GetKeyDown( KeyCode.Alpha6 ) )
		{
			m_CurrentSpeedIndex = 5;
			TimeScale = CurrentSpeed.timeScale;
			UIManager.UpdateSpeedDisplay();
		}

		UpdateTime();
	}

	private void UpdateTime ()
	{
		m_Minute += Time.deltaTime * GameController.TimeScale;
		while ( m_Minute >= 60 )
		{
			m_Minute -= 60;
			m_Hour++;
			while ( m_Hour >= 24 )
			{
				m_Hour -= 24;
				m_Day++;
			}
		}
		TODSky.UpdateTweeners();
	}

	private void OnValidate ()
	{
		UpdateTime();
	}


}
