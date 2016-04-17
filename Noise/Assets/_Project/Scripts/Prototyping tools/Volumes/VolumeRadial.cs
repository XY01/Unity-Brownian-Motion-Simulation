using UnityEngine;
using System.Collections;
/// <summary>
/// Radial Volume
/// - Volumes are used in conjunction with spawners to define spaces in which to spawn objects
/// </summary>
public class VolumeRadial : Volume
{
    // The volumes inner radius 
    public float m_InnerRadius = 0;

    // The volumes outer radius 
    public float m_OuterRadius = 0;

    // Returns a random position in the volume
    public override Vector3 GetRandomPosInVolume()
    {
        Vector3 pos = Random.insideUnitSphere;
        pos *= Random.Range(m_InnerRadius, m_OuterRadius);
        pos += transform.position;

        return pos;
    }

    // Draw gizmos that define the volume
    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.DrawWireSphere(Vector3.zero, m_InnerRadius);
        Gizmos.DrawWireSphere(Vector3.zero, m_OuterRadius);
    }
}
