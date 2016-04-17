using UnityEngine;
using System.Collections;

public class VolumeBox : Volume
{
    Vector3 m_VolumeInner = Vector3.one;
    Vector3 m_VolumeOuter = Vector3.one;


    public override Vector3 GetRandomPosInVolume()
    {
        Vector3 pos = Random.insideUnitSphere;
      //  pos.Scale(m_VolumeOuter);
        return pos;
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();

        Gizmos.DrawWireCube(Vector3.zero, m_VolumeInner);
        Gizmos.DrawWireCube(Vector3.zero, m_VolumeOuter);

    }
}
