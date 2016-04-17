using UnityEngine;
using System.Collections;

public class FollowRotation : MonoBehaviour 
{
	Transform m_Transform;
	public Transform m_TransformToFollow;
	public float m_Smoothing = 4;

	Quaternion m_Rotation;

	void Awake()
	{
		m_Transform = transform;
		m_Rotation = m_Transform.rotation;
	}

	void Update () 
	{
		if( m_Smoothing > 0 )
			m_Rotation = Quaternion.Slerp( m_Rotation, m_TransformToFollow.rotation, m_Smoothing * Time.deltaTime );
		else
			m_Rotation = m_TransformToFollow.rotation;

		m_Transform.rotation = m_Rotation;
	}
}
