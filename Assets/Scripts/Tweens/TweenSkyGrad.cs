using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenSkyGrad : Tweener 
{

	[SerializeField]
	private Material m_StartSkyGrad;

	[SerializeField]
	private Material m_EndSkyGrad;

[SerializeField]
	private Material m_LerpingMaterial;


	protected override void UpdateTween()
	{
		if ( !m_StartSkyGrad || !m_EndSkyGrad ) return;

		if ( !m_LerpingMaterial )
		{
			m_LerpingMaterial = new Material( m_StartSkyGrad.shader );
		}

		LerpColor( "_BaseColor" );
		LerpFloat( "_Exposure" );

		LerpVector( "_Direction1" );
		LerpColor( "_Color1" );
		LerpFloat( "_Exponent1" );

		if ( m_LerpingMaterial.GetFloat( "_Switch2" ) == 1 )
		{
			LerpVector( "_Direction2" );
			LerpColor( "_Color2" );
			LerpFloat( "_Exponent2" );
		}

		if ( m_LerpingMaterial.GetFloat( "_Switch3" ) == 1 )
		{
			LerpVector( "_Direction3" );
			LerpColor( "_Color3" );
			LerpFloat( "_Exponent3" );
		}

		if ( m_LerpingMaterial.GetFloat( "_Switch4" ) == 1 )
		{
			LerpVector( "_Direction4" );
			LerpColor( "_Color4" );
			LerpFloat( "_Exponent4" );
		}

		RenderSettings.skybox = m_LerpingMaterial;
	}

	private void LerpColor ( string propertyLabel )
	{
		m_LerpingMaterial.SetColor( propertyLabel,
			Color.Lerp( m_StartSkyGrad.GetColor( propertyLabel ),
				m_EndSkyGrad.GetColor( propertyLabel ), Factor ) );
	}

	private void LerpFloat ( string propertyLabel )
	{
		m_LerpingMaterial.SetFloat( propertyLabel,
			Mathf.Lerp( m_StartSkyGrad.GetFloat( propertyLabel ),
				m_EndSkyGrad.GetFloat( propertyLabel ), Factor ) );
	}

	private void LerpVector ( string propertyLabel )
	{
		m_LerpingMaterial.SetVector( propertyLabel,
			Vector3.Lerp( m_StartSkyGrad.GetVector( propertyLabel ),
				m_EndSkyGrad.GetVector( propertyLabel ), Factor ) );
	}


}
