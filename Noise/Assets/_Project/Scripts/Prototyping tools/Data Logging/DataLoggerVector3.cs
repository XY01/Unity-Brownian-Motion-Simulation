using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class DataLoggerVector3
{
	public string 		m_DataName = "Data";
	List< Vector3 > 	m_Data = new List<Vector3>();

	public DataLoggerVector3( string dataName )
	{
		m_DataName = dataName;
	}

	public void LogData( Vector3 data )
	{
		m_Data.Add( data );
	}

	public Vector3[] GetData( )
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
