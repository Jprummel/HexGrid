using UnityEngine;
using System.Collections;

public class HexGrid : MonoBehaviour {
	[SerializeField]private GameObject _hexagon;
    [SerializeField]private float _hexCellSize;
	[SerializeField]private float _gridWidth;
	[SerializeField]private float _gridHeight;
	private HexMetrics _hex;
	private Vector3 _position;
    public static float Size;

    void Awake()
    {
        Size = _hexCellSize;
    }

	private void Start () {
		for (int x = 0; x < _gridWidth; x++) {
			for (int z = 0; z < _gridHeight; z++) {
				
				GameObject newHexCell = Instantiate (_hexagon, Vector3.zero, Quaternion.identity) as GameObject; //Creates new hex cell as a gameobject
				_hex = newHexCell.GetComponent<HexMetrics> ();
				_position = new Vector3 ((x + z * 0.5f - z / 2) * _hex.CellWidth - _gridWidth / 2, 0, z * _hex.CellHeight * 0.75f - _gridHeight / 2);
				newHexCell.transform.position = _position;  //Sets the newly created hex cell to the position defined above
				_hex.CreateHex (_position);
			}
		}			
	}
}
