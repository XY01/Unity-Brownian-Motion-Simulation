using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LifeTimer))]
public class ScaleOverLifetime : MonoBehaviour
{
	public AnimationCurve m_Curve;
	public Vector3 	m_MinScale = Vector3.zero;
	public Vector3	m_MaxScale = Vector3.one;

	LifeTimer m_LifeTimer;
	Transform m_Transform;
	Vector3 m_Scale;


	void Awake () 
	{
		m_LifeTimer = GetComponent<LifeTimer>();
		m_Transform = transform;
		SetScale();
	}
	

	void Update () 
	{
		SetScale();
	}

	void SetScale()
	{
		m_Scale = Vector3.Lerp( m_MinScale, m_MaxScale, m_Curve.Evaluate( m_LifeTimer.NormalizedLifetime) );

		m_Transform.localScale = m_Scale;
	}
}
