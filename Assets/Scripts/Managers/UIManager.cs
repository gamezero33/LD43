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

	[SerializeField]
	private GameObject m_SplashTitle;

	[SerializeField]
	private GameObject m_GameScreen;


	private void Awake ()
	{
		if ( m_Canvas )
		{
			m_Canvas.gameObject.SetActive( true );
		}
		m_SplashTitle.SetActive( true );
		m_GameScreen.SetActive( false );
	}


}
