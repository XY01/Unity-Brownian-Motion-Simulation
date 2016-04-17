using UnityEngine;
using System.Collections;

/// <summary>
/// Volume
///  Volumes are used in conjunction with spawners to define spaces in which to spawn objects
/// </summary>
public class Volume : MonoBehaviour
{
    // Returns a random position in the volume
    public virtual Vector3 GetRandomPosInVolume()
    {
        return transform.position + Vector3.zero;
    }

    // Draw gizmos that define the volume
    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
        Gizmos.color = new Color(0, 0, 1, .4F);
    }
}
