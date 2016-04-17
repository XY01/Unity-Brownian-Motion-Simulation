using UnityEngine;
using System.Collections;

/// <summary>
/// Volume
///  Volumes are used in conjunction with spawners to define spaces in which to spawn objects
/// </summary>
public class VolumeBox : Volume
{
    Vector3 m_VolumeInner = Vector3.one;
    Vector3 m_VolumeOuter = Vector3.one;

    // Returns a random position in the volume
    public override Vector3 GetRandomPosInVolume()
    {
        float randomX = Random.Range(m_VolumeInner.x / 2f, m_VolumeOuter.x / 2f);
        float randomY = Random.Range(m_VolumeInner.y / 2f, m_VolumeOuter.y / 2f);
        float randomZ = Random.Range(m_VolumeInner.z / 2f, m_VolumeOuter.z / 2f);

        float xDirection = Random.Range(0f, 1f);
        if (xDirection < .5f)
            randomX = -randomX;

        float yDirection = Random.Range(0f, 1f);
        if (yDirection < .5f)
            randomY = -randomY;

        float zDirection = Random.Range(0f, 1f);
        if (zDirection < .5f)
            randomZ = -randomZ;

        Vector3 pos = new Vector3(xDirection, yDirection, zDirection);

        return pos;
    }

    // Draw gizmos that define the volume
    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();

        Gizmos.DrawWireCube(Vector3.zero, m_VolumeInner);
        Gizmos.DrawWireCube(Vector3.zero, m_VolumeOuter);
    }
}
