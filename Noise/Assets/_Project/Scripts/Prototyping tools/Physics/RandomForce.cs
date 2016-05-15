using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class RandomForce : MonoBehaviour 
{
    // The range in magnitude in either direction along each axis
    public Vector3      m_Range = Vector3.one;

    // Force mode with which the force is applied
    public ForceMode    m_ForceMode = ForceMode.Impulse;

    // Whethere to apply the force on enable of the object
    public bool         m_ApplyForceOnEnable = true;

    // Reference to the rigidbody
    Rigidbody           m_RB;


    void Awake()
    {
        // Find the rigidbody
        m_RB = GetComponent<Rigidbody>();
    }

    // Apply force when object is enabled if flag is set
   void OnEnable()
    {
        if (m_ApplyForceOnEnable)
            ApplyForce();
    }

    void ApplyForce()
    {
        // Get the random force vector based on the range
        Vector3 force = new Vector3(Random.Range(-m_Range.x, m_Range.x), Random.Range(-m_Range.y, m_Range.y), Random.Range(-m_Range.z, m_Range.z));

        // Apply the force 
        m_RB.AddForce( force, m_ForceMode );
    }
}
