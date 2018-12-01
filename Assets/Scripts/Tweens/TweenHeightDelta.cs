using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenHeightDelta : Tweener
{
	
	[SerializeField]
	private float m_StartHeight = 0;

	[SerializeField]
	private float m_EndHeight = 100;


	protected override void UpdateTween ()
	{
		Vector2 sizeDelta = ( transform as RectTransform ).sizeDelta;
		sizeDelta.y = Mathf.LerpUnclamped( m_StartHeight, m_EndHeight, Factor );
		( transform as RectTransform ).sizeDelta = sizeDelta;
	}

}
