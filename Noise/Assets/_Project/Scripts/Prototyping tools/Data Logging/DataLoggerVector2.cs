using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class DataLoggerVector2
{
	public string 		m_DataName = "Data";
	List< Vector2 > 	m_Data = new List<Vector2>();

	public DataLoggerVector2( string dataName )
	{
		m_DataName = dataName;
	}

	public void LogData( Vector2 data )
	{
		m_Data.Add( data );
	}

	public Vector2[] GetData( )
	{
		return m_Data.ToArray();
	}

	public void Save()
	{
	}

	public  void Load()
	{
	}
}
