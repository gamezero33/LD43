using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenSkyGrad : Tweener 
{

	[SerializeField]
	private float m_StartEnvIntensity = 1;

	[SerializeField]
	private float m_EndEnvIntensity = 1;

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
		RenderSettings.ambientIntensity = Mathf.Lerp( m_StartEnvIntensity, m_EndEnvIntensity, Factor );
		DynamicGI.UpdateEnvironment();

		Vector3 d1 = m_LerpingMaterial.GetVector( "_Direction1" );
		Vector3 d2 = m_LerpingMaterial.GetVector( "_Direction2" );
		Vector3 d3 = m_LerpingMaterial.GetVector( "_Direction3" );
		Vector3 d4 = m_LerpingMaterial.GetVector( "_Direction4" );
		m_LerpingMaterial.SetVector( "_NormalizedVector1", d1.normalized );
		m_LerpingMaterial.SetVector( "_NormalizedVector2", d2.normalized );
		m_LerpingMaterial.SetVector( "_NormalizedVector3", d3.normalized );
		m_LerpingMaterial.SetVector( "_NormalizedVector4", d4.normalized );

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
