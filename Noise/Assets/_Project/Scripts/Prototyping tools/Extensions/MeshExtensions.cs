using UnityEngine;
using System.Collections;

public static class MeshExtensions 
{
	public static Mesh ScaleVerts( this Mesh mesh, float scaler )
	{
		for (int i = 0; i < mesh.vertexCount; i++) 
		{
			mesh.vertices[i] = mesh.vertices[i] * scaler;
		}

		return mesh;
	}

}
