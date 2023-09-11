using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class RandomMesh : MonoBehaviour
{
    Mesh _mesh;
    Vector2 _widthRange = new Vector2(.3f, .5f);
    Vector2 _depthRange = new Vector2(.2f, .5f);
    Vector2 _heightRange = new Vector2 (.5f, 1f);

    void Start()
    {
        _mesh = new Mesh();

        _mesh.vertices = GenerateVerts();
        _mesh.triangles = GenerateTries();

        _mesh.RecalculateNormals();

        GetComponent<MeshFilter>().mesh = _mesh;
        GetComponent<MeshCollider>().sharedMesh = _mesh;

        // Calculate normals (you might want to recalculate normals for more complex shapes)
    }
    Vector3[] GenerateVerts()
    {
        Vector3 v1 = new Vector3(-Random.Range(_widthRange.x, _widthRange.y), 0, Random.Range(_depthRange.x, _depthRange.y));
        Vector3 v2 = new Vector3(Random.Range(_widthRange.x, _widthRange.y), 0, Random.Range(_depthRange.x, _depthRange.y));
        Vector3 v3 = new Vector3(Random.Range(_widthRange.x, _widthRange.y), 0, -Random.Range(_depthRange.x, _depthRange.y));
        Vector3 v4 = new Vector3(-Random.Range(_widthRange.x, _widthRange.y), 0, -Random.Range(_depthRange.x, _depthRange.y));

        Vector3 v5 = new Vector3(-Random.Range(_widthRange.x, _widthRange.y), Random.Range(_heightRange.x, _heightRange.y), Random.Range(_depthRange.x, _depthRange.y));
        Vector3 v6 = new Vector3(Random.Range(_widthRange.x, _widthRange.y), Random.Range(_heightRange.x, _heightRange.y), Random.Range(_depthRange.x, _depthRange.y));
        Vector3 v7 = new Vector3(Random.Range(_widthRange.x, _widthRange.y), Random.Range(_heightRange.x, _heightRange.y), -Random.Range(_depthRange.x, _depthRange.y));
        Vector3 v8 = new Vector3(-Random.Range(_widthRange.x, _widthRange.y), Random.Range(_heightRange.x, _heightRange.y), -Random.Range(_depthRange.x, _depthRange.y));

        Vector3[] verts = new Vector3[]
            {
            // Bottom
            v1,
            v2,
            v3,
            v4,
            
            // Top
            v5,
            v6,
            v7,
            v8,

            // Left
            v1,
            v4,
            v5,
            v8,

            // Right
            v2,
            v3,
            v6,
            v7,

            // Front
            v3,
            v4,
            v7,
            v8,

            // Back
            v1,
            v2,
            v5,
            v6,
            };
        return verts;
    }


    int[] GenerateTries()
    {
        int[] triangles = new int[]
        {
            // Front
            1, 0, 2,
            2, 0, 3,

            // Back
            4, 5, 6,
            4, 6, 7,

            // Left
            9, 10, 11,
            8, 10, 9,

            // Right
            12, 13, 15,
            14, 12, 15,

            // Top
            16, 17, 19,
            18, 16, 19,

            // Bottom
            20, 21, 23,
            22, 20, 23
        };

        return triangles;
    }
}
