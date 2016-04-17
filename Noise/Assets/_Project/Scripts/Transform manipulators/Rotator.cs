using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour
{
    public Vector3 m_Speed;	
	
	void Update () 
    {
        transform.Rotate(m_Speed * Time.deltaTime);	
	}
}
