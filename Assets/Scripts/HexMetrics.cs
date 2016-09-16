using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HexMetrics : MonoBehaviour {

	private float   _degrees      = 60;   //The degrees of a hex corner
	private float   _maxDegrees   = 360; //Degrees of a full circle
    private float   _hexSize      = HexGrid.Size; // Gets the size value back from the interface value defined in the Hexgrid script
	public List<Vector3> Corners; 

	public float CellHeight 
    {
		get { return _hexSize * 2;}
	}

	public float CellWidth 
    {
		get { return ( Mathf.Sqrt (3) / 2) * CellHeight;} //Square root of 3 divided by 2 times the cells height defines the cells width
	}

	public void CreateHex (Vector3 center) {
		Corners.Add (center);
		for (int i = 0; i < _maxDegrees / _degrees; i++) {
			float x = center.x + _hexSize * Mathf.Sin ((_degrees * i) * Mathf.Deg2Rad);
			float z = center.z + _hexSize * Mathf.Cos ((_degrees * i) * Mathf.Deg2Rad);
			Vector3 newCorner = new Vector3 (x, 0, z);
			Corners.Add (newCorner); //adds the new cornerpoint to the list
		}
	}
}
