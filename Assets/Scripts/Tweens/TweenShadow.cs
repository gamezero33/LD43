using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TweenShadow : Tweener 
{

	[SerializeField]
	private Vector2 m_StartDistance;

	[SerializeField]
	private Vector2 m_EndDistance;


	private Shadow m_Shadow;


	protected override void UpdateTween ()
	{
		if ( !m_Shadow )
			m_Shadow = GetComponent<Shadow>();

		Vector2 distance = Vector2.LerpUnclamped( m_StartDistance, m_EndDistance, Factor );
		if ( m_Shadow )
			m_Shadow.effectDistance = distance;
	}

}
