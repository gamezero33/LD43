using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour 
{

	#region -| Instance |-
	private static UIManager m_Instance;
	private static UIManager instance
	{
		get
		{
			if ( !m_Instance )
			{
				m_Instance = FindObjectOfType<UIManager>();
			}
			return m_Instance;
		}
	}
	#endregion

	[SerializeField]
	private Canvas m_Canvas;

	[SerializeField, Header( "Titles" )]
	private GameObject m_SplashTitle;

	[SerializeField]
	private GameObject m_GameScreen;

	[SerializeField, Header( "TOD Display" )]
	private Text m_DayCounterText;

	[SerializeField]
	private string m_DayCounterFormat = "DAY {0}";

	[SerializeField]
	private Text m_ClockHourText;

	[SerializeField]
	private Text m_ClockMinuteText;

	[SerializeField]
	private int m_ClockUpdateRate = 5;

	[SerializeField]
	private Text m_SpeedText;

	[SerializeField]
	private Button m_SlowButton;

	[SerializeField]
	private Button m_FastButton;


	private int m_Hour;
	private int m_Minute;
	private Image m_SlowBtnImg;
	private Image m_FastBtnImg;


	public void SlowButtonPressed ()
	{
		GameController.DecreaseSpeed();
		UpdateSpeedDisplay();
	}

	public void FastButtonPressed ()
	{
		GameController.IncreaseSpeed();
		UpdateSpeedDisplay();
	}


	private void Awake ()
	{
		if ( m_Canvas )
		{
			m_Canvas.gameObject.SetActive( true );
		}
		m_SplashTitle.SetActive( true );
		m_GameScreen.SetActive( false );
	}

	private void Start ()
	{
		UpdateSpeedDisplay();
	}

	private void Update ()
	{
		m_DayCounterText.text = string.Format( m_DayCounterFormat, GameController.Day );
		m_Hour = GameController.Hour;
		m_Minute = Mathf.FloorToInt( GameController.Minute / m_ClockUpdateRate ) * m_ClockUpdateRate;
		m_ClockHourText.text = m_Hour < 10 ? string.Format( "0{0}", m_Hour ) : m_Hour.ToString();
		m_ClockMinuteText.text = m_Minute < 10 ? string.Format( "0{0}", m_Minute ) : m_Minute.ToString();
	}

	public static void UpdateSpeedDisplay ()
	{
		instance.UpdateSlowButton();
		instance.UpdateFastButton();
		instance.m_SpeedText.text = GameController.CurrentSpeed.text;
		instance.m_SpeedText.color = GameController.CurrentSpeed.color;
	}

	private void UpdateSlowButton ()
	{
		if ( !m_SlowBtnImg ) m_SlowBtnImg = m_SlowButton.GetComponent<Image>();
		m_SlowBtnImg.color = GameController.PrevSpeed.color.Mix( Color.grey );
		m_SlowButton.enabled = !GameController.CurrentSpeed.Equals( GameController.PrevSpeed );
	}

	private void UpdateFastButton ()
	{
		if ( !m_FastBtnImg ) m_FastBtnImg = m_FastButton.GetComponent<Image>();
		m_FastBtnImg.color = GameController.NextSpeed.color.Mix( Color.grey );
		m_FastButton.enabled = !GameController.CurrentSpeed.Equals( GameController.NextSpeed );
	}

}
