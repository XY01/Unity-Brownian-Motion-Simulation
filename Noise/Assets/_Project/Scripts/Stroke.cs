using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Stroke : MonoBehaviour 
{	
	public delegate void NodeRemoved( Stroke stroke );
	public static event NodeRemoved onNodeRemoved;	
	
	public enum StrokeType
	{
		Timed,
		Distance,
		Velocity,
	}
	
	public StrokeType m_StrokeType = StrokeType.Distance;
	
	// Distance type variables
	public float m_DistanceCutoff = .3f;
	
	// Timed type variables
	public float m_TimeBetweenNodes = .1f;
	float m_NodeTimer = 0;
	
	float m_NodeRemoveTimer = 0;
	
	public Transform m_BrushTransform;	// The point that the stroke follows
	
	public List< Stroke_Node > m_Nodes = new List<Stroke_Node>();
    public List<Stroke_Node> m_PopulatedNodes = new List<Stroke_Node>();
	public List< Vector3 > m_NodePositions = new List< Vector3 >();

    public int m_NodeCount { get { return m_PopulatedNodes.Count;  } }

    
	
	public bool m_RecordingInput = false;	// Is teh stroke curently recording input
	
	float m_Duration; 	// Time the stroke took
	float m_StartTime;	// The time the stroke was started
	public float m_Length; 	// Length of stroke
	public float Length { get{ return m_Length; } }
	
	public float m_MaxLength = 1000;
	
	bool m_RemoveNodeTimerRunning = false;
	public float m_RemoveNodeAfterTime = 0;	
	
	
	
	float VelocitySmoothing = 10;		
	Vector3 m_CurrentVelocity;
	public Vector3 CurrentVelocity { get{ return m_CurrentVelocity; } }
	float m_Speed;
	public float Speed { get{ return m_Speed; } }
	
	public int m_MaxNodeCount = 2000;

    public Color m_Col;
	

	// Use this for initialization
	void Start ()
	{
		//m_OSCHandler = (OSCHandler) FindObjectOfType(typeof(OSCHandler));
	}


	
	// Update is called once per frame
	public void UpdateStroke ( )
	{
        if (!m_RecordingInput) return;

		//print ( "Adding new node. Current count " +m_Nodes.Count +"/"+ m_MaxStrokeLength);
		if( m_Nodes.Count >= m_MaxNodeCount - 1 )
		{
			while( m_Nodes.Count >= m_MaxNodeCount)
			{
				//print ( "Removing node. Current count ");
				m_Nodes.RemoveAt( 0 );
				m_NodePositions.RemoveAt( 0 );
			}
		}
		else if( m_Length >= m_MaxLength )
		{
            while (m_Length >= m_MaxLength && m_Nodes.Count > 0 )
			{
				m_Nodes.RemoveAt( 0 );
				m_NodePositions.RemoveAt( 0 );
                CalculateLength();
			}
            CalculateLength();
		}
		else
		{
            /*
			m_NodeRemoveTimer += Time.deltaTime;
					
			if( m_NodeRemoveTimer > m_RemoveNodeAfterTime && m_Nodes.Count > 0 )
			{
                print("Removing based on time");
				m_NodeRemoveTimer -= m_RemoveNodeAfterTime;
				m_Nodes.RemoveAt( 0 );
				m_NodePositions.RemoveAt( 0 );
			}
             * */
		}


        if( Vector3.Distance( m_BrushTransform.position, m_Nodes[m_Nodes.Count - 1 ].BasePos) > m_DistanceCutoff )
            AddNewNode();
		

		// Calculate length
		CalculateLength();	
		
						
		m_Speed = Mathf.Lerp( m_Speed, 0, Time.deltaTime * 8 );
		m_CurrentVelocity =  Vector3.Lerp( m_CurrentVelocity, Vector3.zero, Time.deltaTime * 8 );
	}
	
	void CalculateLength()
	{
        if (m_Nodes.Count > 1)
        {
            m_Length = 0;
            for (int i = 1; i < m_Nodes.Count; i++)
            {
                m_Length += Vector3.Distance(m_Nodes[i].BasePos, m_Nodes[i - 1].BasePos);
            }
        }
        else
            m_Length = 0;
	}

    bool m_Stroking = false;
	public void BeginStroke( Transform input )
	{
        m_BrushTransform = input;

        ClearAll();
		m_RecordingInput = true;
		m_StartTime = Time.time;
		
		AddNewNode( );
		
		print ( "Stroke started" );
	}

    void ClearAll()
    {
        m_Nodes.Clear();
    }

    public Stroke_Node CreateNodeAtLength( float normLength )
    {
        Stroke_Node prevNode = m_Nodes[0];
        Stroke_Node nextNode = m_Nodes[1]; 
        
        for (int i = 1; i < m_Nodes.Count; i++)
        {
            if( normLength > m_Nodes[i-1].m_NormalizedLength && normLength < m_Nodes[i].m_NormalizedLength )
            {
                prevNode = m_Nodes[i - 1];
                nextNode = m_Nodes[i];
                break;
            }
        }

        float lerpBetweenNodes = normLength.ScaleTo01(prevNode.m_NormalizedLength, nextNode.m_NormalizedLength);

        Stroke_Node newNode = new Stroke_Node(
            Vector3.Lerp(prevNode.BasePos, nextNode.BasePos, lerpBetweenNodes),
            Vector3.one,
            Vector3.Lerp(prevNode.BaseRotation, nextNode.BaseRotation, lerpBetweenNodes),
            Vector3.Lerp(prevNode.Velocity, nextNode.Velocity, lerpBetweenNodes),
            Vector3.Lerp(prevNode.Acceleration, nextNode.Acceleration, lerpBetweenNodes),
            Mathf.Lerp(prevNode.Timecode, nextNode.Timecode, lerpBetweenNodes)
            );

        newNode.m_NormalizedValue = normLength;

        return newNode;
    }
	
	public void EndStroke()
	{
		m_RecordingInput = false;

        // Update normalized values for nodes
        CalculateLength();

        float length = 0;
        for (int i = 0; i < m_Nodes.Count; i++)
        {
            float norm = (float)i / (float)m_Nodes.Count;
            m_Nodes[i].m_NormalizedValue = norm;

            if (i == 0)
                m_Nodes[i].m_NormalizedLength = 0;
            else
            {
                length += Vector3.Distance( m_Nodes[i].BasePos, m_Nodes[i-1].BasePos );
                m_Nodes[i].m_NormalizedLength = length / m_Length;
            }
        }

        PopulateWithNodes((int)(m_Length / m_DistanceCutoff));


		print ( "Stroke ended" );
	}

    void PopulateWithNodes( int count )
    {
        m_PopulatedNodes.Clear();

        for (int i = 0; i < count; i++)
        {
            float norm = (float)i / (float)count;
            m_PopulatedNodes.Add(CreateNodeAtLength(norm));
        }

        print("Populated with nodes: " + count);
    }
	
	public void AddNewNode(  )
	{
        Vector3 pos = m_BrushTransform.transform.position;
        Vector3 rot = m_BrushTransform.transform.rotation.eulerAngles;
        Vector3 scale = Vector3.one;


		// Calculate time code from start time
		float timeCode = Time.time - m_StartTime;
		Vector3 acceleration = Vector3.zero;
		
		// If not the first node in the stroke
		if( m_Nodes.Count > 1 )
		{
			Stroke_Node lastNode =  m_Nodes[ m_Nodes.Count - 1 ];
			
			// Calculate velocity and acceleration
			Vector3 direction = pos - lastNode.BasePos;
			float timeFromLastNode = timeCode - lastNode.Timecode ;
			m_Speed =  Mathf.Lerp( lastNode.Velocity.magnitude, direction.magnitude / timeFromLastNode,  VelocitySmoothing * Time.deltaTime );
			m_CurrentVelocity = direction.normalized * m_Speed;		
			
			//velocity = Utils.LerpVector3( lastNode.Velocity, velocity, VelocitySmoothing * Time.deltaTime, .01f );
			
			acceleration = ( m_CurrentVelocity - lastNode.Velocity ) / timeFromLastNode;
			
			// Update length
			m_Length += Vector3.Distance( pos, lastNode.BasePos );
			//print ( "New node added @ : " + pos +  " .Stroke length: " + m_Length );
		}
		
		Stroke_Node newNode = new Stroke_Node( pos, Vector3.one,rot, m_CurrentVelocity, acceleration , timeCode );		
		m_Nodes.Add( newNode );
		m_NodePositions.Add( newNode.BasePos );

        for (int i = 0; i < m_Nodes.Count; i++)
        {
            float norm = (float)i / (float)m_Nodes.Count;
            m_Nodes[i].m_NormalizedValue = norm;
        }
	}
	
	public void RemoveNode( int index )
	{
		if( m_Nodes.Count == 1 ) return;
		m_Nodes.RemoveAt( index );
		m_NodePositions.RemoveAt( index );
		
		if( onNodeRemoved != null ) onNodeRemoved( this );
	}
	
	IEnumerator RemoveNodeTimer()
	{
		m_RemoveNodeTimerRunning = true;
		//yield return new WaitForSeconds(  m_RemoveNodeAfterTime / m_MaxStrokeLength );
		yield return new WaitForSeconds(  m_RemoveNodeAfterTime );
		//print("Removed Node");
		if( m_Nodes.Count > 0 )
			RemoveNode( 0 );
		
		if( m_RemoveNodeAfterTime > 0 )
		{
			StartCoroutine( RemoveNodeTimer() );
		}
		else
		{
			m_RemoveNodeTimerRunning = false;
		}
	}
	
	public void Save( string prefix )
	{
		PlayerPrefs.SetFloat( prefix + name + "m_RemoveNodeAfterTime", m_RemoveNodeAfterTime );
		PlayerPrefs.SetInt( prefix + name + "m_MaxStrokeLength", m_MaxNodeCount );
		PlayerPrefs.SetFloat( prefix + name + "m_DistanceCutoff", m_DistanceCutoff );
	}
	
	public void Load( string prefix )
	{
		m_RemoveNodeAfterTime = PlayerPrefs.GetFloat( prefix + name + "m_RemoveNodeAfterTime" );
		m_MaxNodeCount = PlayerPrefs.GetInt( prefix + name + "m_MaxStrokeLength" );
		m_DistanceCutoff = PlayerPrefs.GetFloat( prefix + name + "m_DistanceCutoff" );
	}
	
	/*	
	public Stroke_Node GetNodeFromNormTime( float time )
	{
		
		
	}
	
	public Stroke_Node GetNodeFromNormLength( float time )
	{
		
	}
	*/
    public bool m_DrawGizmos = false;
	public void OnDrawGizmos()
	{
        if (!Application.isPlaying || !m_DrawGizmos) return;

        if (m_RecordingInput)
        {
            foreach (Stroke_Node node in m_Nodes)
            {
                Gizmos.color = new Color(0, 0, 0, .5f);
                Gizmos.DrawWireSphere(node.BasePos, .05f);
                Gizmos.color = new Color(255, 0, 0, .5f);
                Gizmos.DrawLine(node.BasePos, node.BasePos + (node.Velocity * .1f));
            }
        }
        else
        {
            foreach (Stroke_Node node in m_PopulatedNodes)
            {
                Gizmos.color = new Color(0, 0, 0, .5f);
                Gizmos.DrawWireSphere(node.BasePos, .05f);
                Gizmos.color = new Color(255, 0, 0, .5f);
                Gizmos.DrawLine(node.BasePos, node.BasePos + (node.Velocity * .1f));
            }
        }
	}
}
