using UnityEngine;
using System.Collections;

public class Volume : MonoBehaviour
{
    public virtual Vector3 GetRandomPosInVolume()
    {
        return Vector3.zero;
    }

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
        Gizmos.color = new Color(0, 0, 1, .4F);
    }
}
