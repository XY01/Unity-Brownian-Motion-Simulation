using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Object pool
/// - Instantiates a pool of objects on start to stop processing overhead of instantiating at run time.
/// - Maintains a list of objects that are active and inactive
/// </summary>
public class ObjectPool : MonoBehaviour 
{
    // The object you want to pool
    public GameObject   m_ObjectToPool;

    // Number of objects to pool
    public int          m_NumberToPool = 10;

    // Index of object being pooled
	int 				m_PooledObjectIndex = 0;

    // 
    bool                m_KeepToppedUp = true;

    public bool         m_IsDirty = false;

    List<GameObject>    m_ActiveObjects = new List<GameObject>();
    List<GameObject>    m_InctiveObjects = new List<GameObject>();
	
	void Start ()
    {
        InstantiateObjects(m_NumberToPool);
	}


    public void Init( GameObject poolObject, int numberToPool )
    {
        m_ObjectToPool = poolObject;
        m_NumberToPool = numberToPool;
    }

    void FixedUpdate()
    {
        if (m_IsDirty)
        {
            int numberToPool = 0;
            numberToPool += m_InctiveObjects.RemoveAll(item => item == null);
            numberToPool += m_ActiveObjects.RemoveAll(item => item == null);
            m_IsDirty = false;

            if (m_KeepToppedUp)
            {
                InstantiateObjects(numberToPool);
            }

        }
    }
		
	public GameObject GetInactiveObject( bool forceSpawn )
    {
        GameObject selectedGO = null;

        // If there is at least 1 object in the inactive list return that, else spawn a new object
        if (m_InctiveObjects.Count > 0)
        {
            selectedGO = m_InctiveObjects[0];
        }
		else if (forceSpawn)
        {
			m_NumberToPool++;
            selectedGO = InstantiateObject();
        }
     
        return selectedGO;
    }

    void InstantiateObjects( int count )
    {
        for (int i = 0; i < count; i++)
        {
            InstantiateObject();
        }
    }

    GameObject InstantiateObject()
    {
        // Instantiate object at a position far away
        GameObject newGo = Instantiate(m_ObjectToPool, Vector3.one * 999, Quaternion.identity) as GameObject;

        // Parent to the spawner to make sure your scene heirarchy doesn't get messy
        newGo.transform.SetParent(transform);

        // Set it to inactive
        newGo.SetActive(false);

        // Add pooled object script to it so it can talk to the object pool
        PooledObject pooledObj = newGo.AddComponent<PooledObject>();
        pooledObj.Init(this);

        // Add object to the inactive list
        m_InctiveObjects.Add(newGo);

		newGo.name = gameObject.name + " ( " + m_PooledObjectIndex + " )";
		m_PooledObjectIndex++;

        return newGo;
    }

    public void ObjectActivated( GameObject go )
    {
        m_ActiveObjects.Add(go);
        m_InctiveObjects.Remove(go);
    }

    public void ObjectDeactivated(GameObject go)
    {
        m_ActiveObjects.Remove(go);
        m_InctiveObjects.Add(go);
    }

    public void ObjectDestroyed(GameObject go)
    {
        if (m_ActiveObjects.Contains(go))       m_ActiveObjects.Remove(go);
        else if (m_InctiveObjects.Contains(go)) m_InctiveObjects.Remove(go);

        if (m_KeepToppedUp) InstantiateObject();
    }

	public void DestroyAll()
	{
		for (int i = 0; i < m_ActiveObjects.Count; i++) 
		{
			Destroy( m_ActiveObjects[i] );
		}
		m_ActiveObjects.Clear();

		for (int i = 0; i < m_InctiveObjects.Count; i++) 
		{
			Destroy( m_InctiveObjects[i] );
		}
		m_InctiveObjects.Clear();
	}
}


