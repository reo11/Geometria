using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]

public class Triangle : MonoBehaviour {

    [SerializeField]
    private Material _mat;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetTriangle(Vector3[] position)
    {
        var mesh = new Mesh();
        mesh.vertices = position;
        mesh.triangles = new int[] {
            0, 1, 2
        };
        mesh.RecalculateNormals();
        var filter = GetComponent<MeshFilter>();
        filter.sharedMesh = mesh;

        var renderer = GetComponent<MeshRenderer>();
        renderer.material = _mat;
    }
}
