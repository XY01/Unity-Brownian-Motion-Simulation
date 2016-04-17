using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// Line mesh.
///  - Draws a line mesh from a list of transforms
/// </summary>
public class LineMesh : MonoBehaviour
{
    MeshRenderer m_Renderer;
    MeshFilter m_Filter;
    Mesh m_Mesh;

    Vector3[] m_Verts;
    Vector3[] m_Normals;
    Vector2[] m_UVs;
    Color[] m_Colors;

    public float m_Width = 1;

    int m_SegmentCount = 0;

    List<Transform> m_Transforms = new List<Transform>();

    public Material m_Material;

    void GenerateMesh()
    {
        if (m_Mesh != null)
            m_Mesh.Clear();

        m_Renderer = gameObject.AddComponent<MeshRenderer>();
        m_Filter = gameObject.AddComponent<MeshFilter>();
        m_Mesh = new Mesh();

        int vertCount = 2 * m_SegmentCount;

        m_Verts = new Vector3[vertCount];
        m_Normals = new Vector3[vertCount];
        m_UVs = new Vector2[vertCount];
        m_Colors = new Color[vertCount];

        int[] indices = new int[(m_SegmentCount - 1) * 6];

        int vertIndex = 0;
        int intIndex = 0;


        for (int i = 0; i < m_SegmentCount; ++i)
        {
            float t = (float)i / m_SegmentCount;	// 0.0 -> 1.0

            Vector3 pos = m_Transforms[i].position;
            Quaternion rot = m_Transforms[i].rotation;
            Vector3 forwardVec = rot * Vector3.forward;

            Transform trans = m_Transforms[i];

            m_Verts[vertIndex] = trans.TransformPoint(Vector3.up * m_Width);
            m_Verts[vertIndex + 1] = trans.TransformPoint(-Vector3.up *  m_Width);

            m_Normals[vertIndex] = Vector3.up;
            m_Normals[vertIndex + 1] = Vector3.up;

            m_UVs[vertIndex] = new Vector2(t, 0);
            m_UVs[vertIndex + 1] = new Vector2(t, 1);

            m_Colors[vertIndex] = Color.white;
            m_Colors[vertIndex + 1] = Color.white;

            if (i > 0)
            {
                indices[intIndex + 0] = vertIndex;
                indices[intIndex + 1] = vertIndex - 2;
                indices[intIndex + 2] = vertIndex - 1;
                indices[intIndex + 3] = vertIndex;
                indices[intIndex + 4] = vertIndex - 1;
                indices[intIndex + 5] = vertIndex + 1;


                intIndex += 6;
            }

            vertIndex += 2;
        }

        m_Mesh.vertices = m_Verts;
        m_Mesh.colors = m_Colors;
        m_Mesh.normals = m_Normals;
        m_Mesh.uv = m_UVs;
        m_Mesh.triangles = indices;
        
        m_Mesh.RecalculateNormals();
        m_Mesh.RecalculateBounds();
        
        m_Filter.mesh = m_Mesh;
        m_Renderer.material = m_Material;
    }

    void GenerateMesh(List<Stroke_Node> nodes )
    {
        if (m_Mesh != null)
            m_Mesh.Clear();

        m_SegmentCount = nodes.Count;

        if (m_Renderer == null)
        {
            m_Renderer = gameObject.AddComponent<MeshRenderer>();
            m_Filter = gameObject.AddComponent<MeshFilter>();
        }

        m_Mesh = new Mesh();

        int vertCount = 2 * m_SegmentCount;

        m_Verts = new Vector3[vertCount];
        m_Normals = new Vector3[vertCount];
        m_UVs = new Vector2[vertCount];
        m_Colors = new Color[vertCount];

        int[] indices = new int[(m_SegmentCount - 1) * 6];

        int vertIndex = 0;
        int intIndex = 0;

        Transform tempT = new GameObject().transform;


        for (int i = 0; i < m_SegmentCount; ++i)
        {
            float t = (float)i / m_SegmentCount;	// 0.0 -> 1.0

            Vector3 pos = nodes[i].m_CurrentPos;
            Quaternion rot = Quaternion.Euler(nodes[i].m_CurrentRot);
            //  Vector3 rightVec = rot * Vector3.right;
            Vector3 forwardVec = rot * Vector3.forward;

            tempT.position = nodes[i].m_CurrentPos;
            tempT.rotation = rot;

            m_Verts[vertIndex] = tempT.TransformPoint(Vector3.up * m_Width);
            m_Verts[vertIndex + 1] = tempT.TransformPoint(-Vector3.up * m_Width);

            m_Normals[vertIndex] = Vector3.up;
            m_Normals[vertIndex + 1] = Vector3.up;

            m_UVs[vertIndex] = new Vector2(t, 0);
            m_UVs[vertIndex + 1] = new Vector2(t, 1);

            m_Colors[vertIndex] = Color.white;
            m_Colors[vertIndex + 1] = Color.white;

            if (i > 0)
            {
                indices[intIndex + 0] = vertIndex;
                indices[intIndex + 1] = vertIndex - 2;
                indices[intIndex + 2] = vertIndex - 1;
                indices[intIndex + 3] = vertIndex;
                indices[intIndex + 4] = vertIndex - 1;
                indices[intIndex + 5] = vertIndex + 1;


                intIndex += 6;
            }

            vertIndex += 2;
        }

        m_Mesh.vertices = m_Verts;
        m_Mesh.colors = m_Colors;
        m_Mesh.normals = m_Normals;
        m_Mesh.uv = m_UVs;
        m_Mesh.triangles = indices;

        m_Mesh.RecalculateNormals();
        m_Mesh.RecalculateBounds();

        m_Filter.mesh = m_Mesh;
        m_Renderer.material = m_Material;

        Destroy(tempT.gameObject);
    }

    void UpdateMesh()
    {
        int vertIndex = 0;

        for (int i = 0; i < m_SegmentCount; ++i)
        {
            Vector3 pos = m_Transforms[i].position;
            Quaternion rot = m_Transforms[i].rotation;
            //  Vector3 rightVec = rot * Vector3.right;
            Vector3 forwardVec = rot * Vector3.forward;

            Transform trans = m_Transforms[i];

            // rightVec *= trackWidth * 0.5f;
            forwardVec *= 10.0f;


            m_Verts[vertIndex] = trans.TransformPoint(Vector3.up * m_Width);
            m_Verts[vertIndex + 1] = trans.TransformPoint(-Vector3.up * m_Width);

            m_Colors[vertIndex] = Color.white;
            m_Colors[vertIndex + 1] = Color.white;


            vertIndex += 2;
        }

        m_Mesh.vertices = m_Verts;
        m_Mesh.colors = m_Colors;
        m_Mesh.normals = m_Normals;
        m_Mesh.uv = m_UVs;

        m_Mesh.RecalculateNormals();
        m_Mesh.RecalculateBounds();
    }

    public void UpdateMesh( List<Stroke_Node> nodes )
    {
		if( nodes.Count < 2 )
			return;

        if ( m_SegmentCount != nodes.Count )
        {
            GenerateMesh(nodes);
            return;
        }


        int vertIndex = 0;

        Transform tempT = new GameObject().transform;

        for (int i = 0; i < m_SegmentCount; ++i)
        {
            Vector3 pos = nodes[i].m_CurrentPos;
            Quaternion rot = Quaternion.Euler(nodes[i].m_CurrentRot);
            //  Vector3 rightVec = rot * Vector3.right;
            Vector3 forwardVec = rot * Vector3.forward;

            tempT.position = nodes[i].m_CurrentPos;
            tempT.rotation = rot;

            // rightVec *= trackWidth * 0.5f;
            forwardVec *= 10.0f;


            m_Verts[vertIndex] = tempT.TransformPoint(Vector3.up * m_Width);
            m_Verts[vertIndex + 1] = tempT.TransformPoint(-Vector3.up * m_Width);

            m_Colors[vertIndex] = Color.white;
            m_Colors[vertIndex + 1] = Color.white;


            vertIndex += 2;
        }

        m_Mesh.vertices = m_Verts;
        m_Mesh.colors = m_Colors;
        m_Mesh.normals = m_Normals;
        m_Mesh.uv = m_UVs;

        m_Mesh.RecalculateNormals();
        m_Mesh.RecalculateBounds();

        Destroy(tempT.gameObject);
    }
}
