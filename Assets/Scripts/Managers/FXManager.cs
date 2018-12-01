using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXManager : MonoBehaviour 
{

    // TODO: Encapsulate PFX Prefabs and static method calls into a serializable class

	#region -| Instance |-
    private static FXManager m_Instance;
    private static FXManager instance
    {
        get
        {
            if ( !m_Instance )
            {
                m_Instance = FindObjectOfType<FXManager>();
            }
            return m_Instance;
        }
    }
    #endregion


    // Published Fields

    [SerializeField, Header( "Camera Shake Parameters" )]
    private float m_ShakeDuration = 0.2f;

    [SerializeField]
    private float m_ShakeAngleMod = 10;

    [SerializeField]
    private float m_ShakePosMod = 2;


    [SerializeField, Header( "PFX Prefabs" )]
    private GameObject m_BlastPFXPrefab;

    [SerializeField]
    private GameObject m_SmashPFXPrefab;



    // Private Members
    
	private Vector3 m_CameraPos;
	private Vector3 m_CameraEulers;


    // Mono Callback Methods

    private void Start ()
    {
        m_CameraPos = Camera.main.transform.position;
        m_CameraEulers = Camera.main.transform.eulerAngles;
    }


    // Static Methods

    public static void CameraShake ( float intensity = 1 )
    {
        instance.DoCameraShake( intensity );
    }

    public static void CreateBlastFX ( Vector3 position, Transform parent = null )
    {
        instance.CreateFX( instance.m_BlastPFXPrefab, position, parent, Color.clear );
    }

    public static void CreateSmashFX ( Color color, Vector3 position, Transform parent = null )
    {
        instance.CreateFX( instance.m_SmashPFXPrefab, position, parent, color );
    }


	// Private Methods

	private void CreateFX ( GameObject fxPrefab, Vector3 position, Transform parent, Color color )
	{
		GameObject go = Instantiate( fxPrefab, parent );
		go.transform.position = position;
		ParticleSystem ps = go.GetComponentInChildren<ParticleSystem>();
		if ( !ps )
		{
        	Debug.LogError( "Instantiated PFX entity has no Particle System attached!" );
			return;
		}
        else
        {
            if ( color != Color.clear )
            {
                ParticleSystem.MainModule main = ps.main;
                main.startColor = color;
            }
        }
		instance.StartCoroutine( instance.DestroyAfterDuration( go, ps.main.duration ) );
	}


	private IEnumerator DestroyAfterDuration ( GameObject go, float duration )
	{
		yield return new WaitForSeconds( duration );

		if ( go )
		{
			Destroy( go );
		}
	}


    private void SlingSprite ( Sprite sprite, Vector3 startPos, Vector3 endPos )
    {
        StartCoroutine( slingSprite( sprite, startPos, endPos ) );
    }

    private IEnumerator slingSprite ( Sprite sprite, Vector3 startPos, Vector3 endPos, System.Action callback = null )
    {
        GameObject sgo = new GameObject( "Star Sprite" );
        SpriteRenderer sr = sgo.AddComponent<SpriteRenderer>();
        sr.sprite = sprite;
        AnimationCurve curve = AnimationCurve.EaseInOut( 0, 0, 1, 1 );

        float t = 0;
        while ( t < 1 )
        {
            t += Time.deltaTime;

            sgo.transform.position = Vector3.Lerp( startPos, endPos, curve.Evaluate( t ) );
            sgo.transform.eulerAngles = Vector3.forward * Mathf.Lerp( 0, -560, t );

            yield return null;
        }

        Destroy( sgo );

        if ( callback != null )
        {
            callback.Invoke();
        }
    }


    private void DoCameraShake ( float intensity )
    {
        StartCoroutine( shakeCamera( intensity ) );
    }

    private IEnumerator shakeCamera ( float intensity )
    {
        float t = 0;
        while ( t < m_ShakeDuration )
        {
            t += Time.deltaTime;

            NeonPlay.HapticFeedback.ImpactVibrate( NeonPlay.HapticFeedback.ImpactStyle.Light );
            Camera.main.transform.position = Camera.main.transform.position.Random( intensity * m_ShakePosMod );
            Camera.main.transform.eulerAngles = Camera.main.transform.eulerAngles.Random( intensity * m_ShakeAngleMod );

            yield return null;
        }

        Camera.main.transform.position = m_CameraPos;
        Camera.main.transform.eulerAngles = m_CameraEulers;
    }





}
