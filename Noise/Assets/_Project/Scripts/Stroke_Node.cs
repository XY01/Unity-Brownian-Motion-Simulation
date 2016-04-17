using UnityEngine;
using System.Collections;

public class Stroke_Node  
{
	Vector3 		m_BasePos;
	public Vector3 	BasePos { get{ return m_BasePos;}}
    public Vector3  m_CurrentPos;   

    Vector3         m_BaseRotation;
    public Vector3  BaseRotation { get { return m_BaseRotation; } }
    public Vector3  m_CurrentRot;   

    Vector3         m_BaseScale;
    public Vector3  BaseScale { get { return m_BaseScale; } }
    public Vector3  m_CurrentScale;   

	Vector3 	    m_Velocity;
	public Vector3 	Direction { get{ return m_Velocity.normalized; } }
    public Vector3  Velocity { get { return m_Velocity; } }

	Vector3 	    m_Acceleration;
    public Vector3  Acceleration { get { return m_Acceleration; } }

	public float 	m_TotalLifeTime = float.MaxValue;	// How long the node will live for *For timed nodes
	float 	        m_Timecode; 		                // Time.time that the node was created
    public float    Timecode { get { return m_Timecode; } }


	public float    m_NormalizedValue;
    public float    m_NormalizedLength;

    public Stroke_Node(Vector3 pos, Vector3 scale, Vector3 rot, Vector3 vel, Vector3 acc, float timeCode)
	{
		m_BasePos = pos;
        m_CurrentPos = pos;

		m_BaseScale = scale;
        m_CurrentScale = scale;

        m_BaseRotation = rot;
        m_CurrentRot = rot;

		m_Velocity = vel;
		m_Acceleration = acc;
		m_Timecode = timeCode;
	}	
}
