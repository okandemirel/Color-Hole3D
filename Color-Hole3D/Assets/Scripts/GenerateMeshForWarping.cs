using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class Extensions
{
    public static List<int> RemoveAllSpecifiedIndicesFromArray(this int[] a, bool[] indicesToRemove)
    {
        List<int> b = new List<int>();
        for (int i = 0; i < indicesToRemove.Length; ++i)
        {
            if (!indicesToRemove[i])
                b.Add(a[i]);
        }

        return b;
    }

    public static List<int> IndexOf(this Array a, object o)
    {
        List<int> result = new List<int>();
        for (int i = 0; i < a.Length; ++i)
        {
            if (a.GetValue(i).Equals(o))
            {
                result.Add(i);
            }
        }

        result.Sort();
        return result;
    }
}

public class GenerateMeshForWarping : MonoBehaviour
{
    #region Serialized Variables

    [SerializeField] private float minDistance;
    [SerializeField] private Transform trackedObject;

    [SerializeField] private MeshFilter filter;
    //[SerializeField] private Triangulator triangulator;

    #endregion


    #region Private Variables

    private Mesh _mesh;
    private MeshCollider _meshCollider;
    private Vector3[] _vertices;
    private Vector3[] _normals;
    private Vector2[] _uvs;
    private int[] _triangles;
    private bool[] _trianglesDisabled;
    private List<int>[] _trisWithVertex;

    private Vector3[] _origvertices;
    private Vector3[] _orignormals;
    private Vector2[] _origuvs;
    private int[] _origtriangles;

    #endregion


    private void Start()
    {
        //_triangulator = GetComponent<Triangulator>();  
        _mesh = new Mesh();
        filter = GetComponent<MeshFilter>();
        _meshCollider = GetComponent<MeshCollider>();
        var mesh = filter.mesh;
        _orignormals = mesh.normals;
        _origvertices = mesh.vertices;
        _origuvs = mesh.uv;
        _origtriangles = mesh.triangles;

        _vertices = new Vector3[_origvertices.Length];
        _normals = new Vector3[_orignormals.Length];
        _uvs = new Vector2[_origuvs.Length];
        _triangles = new int[_origtriangles.Length];
        _trianglesDisabled = new bool[_origtriangles.Length];

        _orignormals.CopyTo(_normals, 0);
        _origvertices.CopyTo(_vertices, 0);
        _origtriangles.CopyTo(_triangles, 0);
        _origuvs.CopyTo(_uvs, 0);

        _trisWithVertex = new List<int>[_origvertices.Length];

        for (var i = 0; i < _origvertices.Length; ++i)
        {
            _trisWithVertex[i] = _origtriangles.IndexOf(i);
        }

        filter.mesh = GenerateMeshWithHoles();
    }

    private void Update()
    {
        Remesh();
    }

    private void Remesh()
    {
        filter.mesh = GenerateMeshWithHoles();
    }

    private Mesh GenerateMeshWithHoles()
    {
        var trackPos = trackedObject.position;
        for (var i = 0; i < _origvertices.Length; ++i)
        {
            var localScale = transform.localScale;
            var v = new Vector3(_origvertices[i].x * localScale.x,
                _origvertices[i].y * localScale.y, _origvertices[i].z * localScale.z);
            if (!((v + transform.position - trackPos).magnitude < minDistance)) continue;
            for (var j = 0; j < _trisWithVertex[i].Count; ++j)
            {
                var value = _trisWithVertex[i][j];
                var remainder = value % 3;
                _trianglesDisabled[value - remainder] = true;
                _trianglesDisabled[value - remainder + 1] = true;
                _trianglesDisabled[value - remainder + 2] = true;
            }
        }
        
        _triangles = _origtriangles;
        _triangles = _triangles.RemoveAllSpecifiedIndicesFromArray(_trianglesDisabled).ToArray();


        _mesh.SetVertices(_vertices.ToList<Vector3>());
        _mesh.SetNormals(_normals.ToList());
        _mesh.SetUVs(0, _uvs.ToList());
        _mesh.SetTriangles(_triangles, 0);
        for (var i = 0; i < _trianglesDisabled.Length; ++i)
            _trianglesDisabled[i] = false;

        _meshCollider.sharedMesh = _mesh;
        return _mesh;
    }
}