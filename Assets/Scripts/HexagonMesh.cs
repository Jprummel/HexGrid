using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(HexMetrics))] //Adds these components to the object if they don't contain it yet object cant exist without these components
public class HexagonMesh : MonoBehaviour {
	
    private List<Vector3>   _vertices   = new List<Vector3>();
	private List<int>       _triangles  = new List<int>();
    private HexMetrics      _hex;
    private Mesh            _hexMesh;

	private void Start() 
    {
		_hexMesh    = GetComponent<MeshFilter> ().mesh;
		_hex        = GetComponent<HexMetrics> ();
        //Sets the vertice points for the mesh
		_vertices   = _hex.Corners;
        //Assigns a name to the created mesh
        _hexMesh.name = "Hex Mesh";
		Triangulate ();
        //Clears mesh data if any
        _hexMesh.Clear();  
        //Assigns vertices and triangles to the mesh
        _hexMesh.vertices   = _vertices.ToArray();
        _hexMesh.triangles  = _triangles.ToArray();
        //Recalculate the mesh's normals (shows the change to the changed/add vertices)
        _hexMesh.RecalculateNormals();		
		transform.position  = new Vector3(0, 0, 0);
	}
		

	private void Triangulate() 
    {
        //Fills the hexagon with triangles
		for (int i = 0; i < 6; i++) {
			_triangles.Add (0);
			_triangles.Add (i + 1);
			if (i + 2 > 6) 
            {
				_triangles.Add (1);
			} else 
            {
				_triangles.Add (i + 2);
			}
		}
	}
}
