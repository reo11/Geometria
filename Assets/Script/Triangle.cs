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

    public void SetTriangle(Vector3[] positions)
    {
        CheckClockwise(positions);
        var mesh = new Mesh();
        mesh.vertices = positions;
        if (CheckClockwise(positions))
        {
            mesh.triangles = new int[] {
                0, 1, 2
            };
        }
        else
        {
            mesh.triangles = new int[] {
                2, 1, 0
            };
        }
        mesh.RecalculateNormals();
        var filter = GetComponent<MeshFilter>();
        filter.sharedMesh = mesh;

        var renderer = GetComponent<MeshRenderer>();
        renderer.material = _mat;
    }

    bool CheckClockwise(Vector3[] pos)
    {
        Vector3 side1 = pos[1] - pos[0];
        Vector3 side2 = pos[2] - pos[0];

        Vector3 ans = Vector3.Cross(side1, side2).normalized;
        Debug.Log(ans[2] < 0);
        return (ans[2] < 0);
    }
}
