using UnityEngine;
using System.Collections;

public class BrownianMotionSimulation : MonoBehaviour
{
    //public ParticleSystem m_CollisionPSys;

    DataLoggerVector3 m_PositionDataLogger;
    DataLoggerVector3 m_VelocityDataLogger;
    DataLoggerVector3 m_RotationDataLogger;
    DataLoggerVector3 m_AngularVelocityDataLogger;

    DataLoggerFloat m_FloatLogger;

    public Material m_Mat;

    Rigidbody m_RB;
    Transform m_Transform;

    void Start()
    {
        m_PositionDataLogger = new DataLoggerVector3("Position");
        m_VelocityDataLogger = new DataLoggerVector3("Velocity");
        m_RotationDataLogger = new DataLoggerVector3("Rotation");
        m_AngularVelocityDataLogger = new DataLoggerVector3("Rotational Velocity");

        m_FloatLogger = new DataLoggerFloat("Test");

        m_RB = GetComponent<Rigidbody>();
        m_Transform = transform;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            m_FloatLogger.Save();
        }

        m_FloatLogger.LogData(Time.time);

        m_Mat.SetVector("_DistFrom", new Vector4(m_Transform.position.x, m_Transform.position.y, m_Transform.position.z, 0));
    }

    void FixedUpdate()
    {
        m_PositionDataLogger.LogData(m_Transform.position);
        m_VelocityDataLogger.LogData(m_RB.velocity);
        m_RotationDataLogger.LogData(m_Transform.rotation.eulerAngles);
        m_AngularVelocityDataLogger.LogData(m_RB.angularVelocity);


    }


}
