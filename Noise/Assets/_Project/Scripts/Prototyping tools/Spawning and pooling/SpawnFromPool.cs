using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
///  Spawn from pool 
///  - Pools x amount of objects and spawns objects from teh instantiated pool
///  - Spawning from a pool of objects decreases the amount of ovehead in instantiating objects at spawn time
/// </summary>
public class SpawnFromPool : MonoBehaviour 
{
    public bool     m_SpawnOnTimer = true;
    public float    m_SpawnPerSecond = 1;
    float           m_SpawnInterval;

    public Volume[] m_Volume;
    

    public Vector3  m_RotationRange = Vector3.zero;
    public Vector2  m_ScaleRange = Vector3.one;

    public GameObject[] m_ObjectsToSpawn;

    ObjectPool[]        m_ObjectPools;

    public int m_NumberToPool = 30;
	public bool m_ForceSpawn = false;

	public bool m_Debug = false;

    void Start()
    {
        m_SpawnInterval = 1f / m_SpawnPerSecond;

        m_ObjectPools = new ObjectPool[m_ObjectsToSpawn.Length];

        for (int i = 0; i < m_ObjectsToSpawn.Length; i++)
        {
            ObjectPool newPool = new GameObject(i + " Object pool - " + m_ObjectsToSpawn[i].name ).AddComponent<ObjectPool>();
            newPool.Init(m_ObjectsToSpawn[i], m_NumberToPool);
            newPool.transform.SetParent(transform);
            m_ObjectPools[i] = newPool;
        }

        if( m_SpawnOnTimer )
        {
            StartCoroutine(SpawnOnTimer());
        }
    }

    void Update()
    {      
		if( m_SpawnPerSecond <= 0 )
			m_SpawnOnTimer = false;
		else
			m_SpawnInterval = 1f / m_SpawnPerSecond;

        if (Input.GetKeyDown(KeyCode.A))
            Spawn();
    }
	
	void Spawn() 
    {
        // Select the type of object to spawn
        int objTypeIndex = 0;// Random.Range(0, m_ObjectsToSpawn.Length - 1);

        // Get inactive object from the pool
		GameObject newGo = m_ObjectPools[objTypeIndex].GetInactiveObject(m_ForceSpawn);
		if( newGo == null )
		{
			if( m_Debug ) print( "Pool is empty" );
			return;
		}
        

        // Position
        int volumeIndex = Random.Range(0, m_Volume.Length);
        Vector3 pos = m_Volume[volumeIndex].GetRandomPosInVolume();
        
        Vector3 rot = new Vector3(Random.Range(-m_RotationRange.x / 2f, m_RotationRange.x / 2f), Random.Range(-m_RotationRange.y / 2f, m_RotationRange.y / 2f), Random.Range(-m_RotationRange.z / 2f, m_RotationRange.z / 2f));
        float   scale = Random.Range(m_ScaleRange.x, m_ScaleRange.y);
        
        // Set the objects transform variables
        newGo.transform.position = pos;
        newGo.transform.rotation = Quaternion.Euler(rot);
        newGo.transform.localScale = scale * Vector3.one;
        
        // Set the object to active
        newGo.SetActive(true);
	}

    IEnumerator SpawnOnTimer()
    {
        while (m_SpawnOnTimer)
        {            
            yield return new WaitForSeconds(m_SpawnInterval);
			Spawn();
        }
    }

    
	void OnApplicationQuit()
	{		
		StopAllCoroutines();
		for (int i = 0; i < m_ObjectPools.Length; i++) 
		{
			m_ObjectPools[i].DestroyAll();
		}
	}
}
