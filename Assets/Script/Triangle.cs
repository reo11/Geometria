using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]

public class Triangle : MonoBehaviour {

    [SerializeField]
    private Material _mat;

    static int circulation = 0;

    Color[] colors = new Color[]
    {
        new Color(248/255f, 243/255f, 214/255f, 1.0f),
        new Color(227/255f, 63/255f, 35/255f, 1.0f),
        new Color(3/255f, 179/255f, 202/255f, 1.0f),
    };

    // Use this for initialization
    void Start () {
        var pos = this.transform.position;
        pos.z = 120;
        this.transform.position = pos;
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

        // いったん三角形の色は、3色を循環させとく
        renderer.material.color = colors[circulation++];
        circulation = circulation % colors.Length;
        Debug.Log(circulation);
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
