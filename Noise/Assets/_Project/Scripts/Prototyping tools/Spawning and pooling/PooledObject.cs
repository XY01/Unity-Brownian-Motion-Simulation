using UnityEngine;
using System.Collections;

public class PooledObject : MonoBehaviour 
{
    ObjectPool m_ObjectPool;

    
    public void Init( ObjectPool objPool )
    {
        m_ObjectPool = objPool;
    }
   
    void OnEnable()
    {
        m_ObjectPool.ObjectActivated(gameObject);
    }

    void OnDisable()
    {
        m_ObjectPool.ObjectDeactivated(gameObject);
    }

    void OnDestroy()
    {
        m_ObjectPool.m_IsDirty = true;
    }
}
