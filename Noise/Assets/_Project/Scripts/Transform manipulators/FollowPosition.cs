using UnityEngine;
using System.Collections;

public class FollowPosition : MonoBehaviour 
{
	Transform 			m_Transform;
	public Transform 	m_TransformToFollow;
	public float 		m_Smoothing = 4;

	Vector3 			m_Position;

	public Space 		m_Space = Space.World;

	void Awake()
	{
		m_Transform = transform;

		if( m_Space == Space.Self )
			m_Position = m_Transform.localPosition;
		else
			m_Position = m_Transform.position;
	}

	void Update () 
	{
		Vector3 targetPosition;

		if( m_Space == Space.Self )
			targetPosition = m_TransformToFollow.localPosition;
		else
			targetPosition = m_TransformToFollow.position;


		if( m_Smoothing > 0 )
			m_Position = Vector3.Lerp( m_Position, targetPosition, m_Smoothing * Time.deltaTime );
		else
			m_Position = targetPosition;

		m_Transform.position = m_Position;
	}
}
