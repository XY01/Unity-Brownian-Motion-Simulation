using UnityEngine;
using System.Collections;

public class RandomForce : MonoBehaviour 
{
    public Vector3      m_Range = Vector3.one;
    public ForceMode    m_ForceMode = ForceMode.Impulse;
    public bool         m_ApplyForceOnEnable = true;
    Rigidbody           m_RB;

    void Awake()
    {
        m_RB = GetComponent<Rigidbody>();
    }

   void OnEnable()
    {
        if (m_ApplyForceOnEnable)
            ApplyForce();
    }

    void ApplyForce()
    {
        Vector3 force = new Vector3(Random.Range(-m_Range.x, m_Range.x), Random.Range(-m_Range.y, m_Range.y), Random.Range(-m_Range.z, m_Range.z));
        m_RB.AddForce( force, m_ForceMode );
    }
}
