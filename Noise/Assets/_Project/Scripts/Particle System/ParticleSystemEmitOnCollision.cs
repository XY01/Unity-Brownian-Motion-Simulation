using UnityEngine;
using System.Collections;

public class ParticleSystemEmitOnCollision : MonoBehaviour
{
    public ParticleSystem m_PSys;

    public bool m_PositionAtCollision = true;
    public bool m_FaceObject = true;

    public Vector2 m_EmissionRange = new Vector2(1, 100);


    void OnCollisionEnter(Collision collision)
    {
        if (m_PositionAtCollision)
        {
            m_PSys.transform.position = collision.contacts[0].point;
        }

        if (m_FaceObject)
        {
            m_PSys.transform.LookAt(transform.position);
          //  m_PSys.startRotation3D = m_PSys.transform.rotation.eulerAngles * Mathf.Deg2Rad; // add back in after updating to 5.3
        }

        int emitCount = (int)Random.Range(m_EmissionRange.x, m_EmissionRange.y);
        m_PSys.Emit(emitCount);
       
    }
}
