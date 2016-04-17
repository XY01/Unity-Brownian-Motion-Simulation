using UnityEngine;
using System.Collections;

/// <summary>
/// Particle System - Emit on collision
/// </summary>
public class ParticleSystemEmitOnCollision : MonoBehaviour
{
    // Particle system reference
    public ParticleSystem m_PSys;

    // Whether or not to posiiton the particle system at the collision point
    public bool m_PositionAtCollision = true;

    // Whether or not the particle system rotates to face the collision
    public bool m_FaceObject = true;

    // The range (min , max) count that the particle system will emit
    public Vector2 m_EmissionRange = new Vector2(1, 100);


    // On collision enter event
    void OnCollisionEnter(Collision collision)
    {
        // If position at collision, move the particle system
        if (m_PositionAtCollision)
        {
            m_PSys.transform.position = collision.contacts[0].point;
        }

        // If face object, the particle rotates to face this object
        if (m_FaceObject)
        {
            m_PSys.transform.LookAt(transform.position);         
        }

        // Generate an emit count from ranges
        int emitCount = (int)Random.Range(m_EmissionRange.x, m_EmissionRange.y);

        // Emit particles 
        m_PSys.Emit(emitCount);       
    }
}
