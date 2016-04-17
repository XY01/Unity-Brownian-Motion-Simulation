using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class DataLoggerFloat
{
	public string 	m_DataName = "Data";
	List< float > 	m_Data = new List<float>();

	public DataLoggerFloat( string dataName )
	{
		m_DataName = dataName;
	}

	public void LogData( float data )
	{
		m_Data.Add( data );
	}

	public float[] GetData( )
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
