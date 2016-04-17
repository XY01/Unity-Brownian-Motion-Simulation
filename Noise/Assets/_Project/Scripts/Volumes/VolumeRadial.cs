using UnityEngine;
using System.Collections;

public class VolumeRadial : Volume
{
    public float m_InnerRadius = 0;
    public float m_OuterRadius = 0;


    public override Vector3 GetRandomPosInVolume()
    {
        Vector3 pos = Random.insideUnitSphere;
        pos *= Random.Range(m_InnerRadius, m_OuterRadius);
        pos += transform.position;

        return pos;
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.DrawWireSphere(Vector3.zero, m_InnerRadius);
        Gizmos.DrawWireSphere(Vector3.zero, m_OuterRadius);
    }
}
