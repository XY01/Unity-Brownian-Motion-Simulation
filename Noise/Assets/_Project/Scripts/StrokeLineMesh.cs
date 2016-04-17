using UnityEngine;
using System.Collections;

public class StrokeLineMesh : MonoBehaviour
{
	public LineMesh m_LineMesh;
	public Stroke m_Stroke;



	// Use this for initialization
	void Start () 
	{
		m_Stroke.BeginStroke(m_Stroke.m_BrushTransform);
	}
	
	// Update is called once per frame
	void Update () 
	{
		m_Stroke.UpdateStroke();
		m_LineMesh.UpdateMesh( m_Stroke.m_Nodes );

	
	}
}
