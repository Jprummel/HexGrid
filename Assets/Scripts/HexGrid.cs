using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class HexGrid : MonoBehaviour {
    
    //Used to clear the hexcells and coordinates (if any exist already) before creating new ones
    private List<Text>          _hexCoordinatesLabelList    = new List<Text>();
    private List<GameObject>    _hexCellList                = new List<GameObject>();
    //Shows the cells Coordinates and prefabs to spawn
    [SerializeField]private Text        _hexCoorLabel;   //Label prefab to show coordinates of cells
    [SerializeField]private GameObject  _hexagon;               //Hex mesh prefab
                    private Canvas      _hexCoorCanvas;  //Canvas to show the label with coordinates
    //Grids interface    
    [SerializeField]private List<InputField> _inputfields = new List<InputField>();
    [SerializeField]private float _hexCellSize;
	[SerializeField]private float _gridWidth;
	[SerializeField]private float _gridHeight;
	
    private HexMetrics  _hex;
	private Vector3     _position;
    public static float Size; //Returns the _hexCellSize value to the hexagon script to adjust its size

    void Awake()
    {
        _hexCoorCanvas = GetComponentInChildren<Canvas>();
    }

    //Creates the grid with the settings provided by the user
    public void SpawnGrid()
    {
        ClearGrid(); //Clears the current grid(if any) before the new one is made
        for (int x = 0, i = 0; x < _gridWidth; x++)
        {
            for (int z = 0; z < _gridHeight; z++)
            {
                
                GameObject newHexCell = Instantiate(_hexagon, Vector3.zero, Quaternion.identity) as GameObject; //Creates new hex cell as a gameobject
                _hexCellList.Add(newHexCell);
                _hex = newHexCell.GetComponent<HexMetrics>();
                _position = new Vector3((x + z * 0.5f - z / 2) * _hex.CellWidth - _gridWidth / 2, 0, z * _hex.CellHeight * 0.75f - _gridHeight / 2); //Takes the cells number and determines its position with it
                newHexCell.transform.position = _position;  //Sets the newly created hex cell to the position defined above
                _hex.CreateHex(_position);
                ShowCoordinates(x, z, i++,newHexCell); // Shows the coordinates of each cell created
            }
        }
    }

    private void ShowCoordinates(int x, int z, int i,GameObject parentCell)
    {
        Text label = Instantiate<Text>(_hexCoorLabel);
        _hexCoordinatesLabelList.Add(label);
        label.rectTransform.SetParent(_hexCoorCanvas.transform, false);
        label.transform.position = new Vector3(parentCell.transform.localPosition.x,0.1f,parentCell.transform.localPosition.z);
        label.text = x.ToString() + "\n" + z.ToString();
    }

    private void ClearGrid()
    {
        for (int i = 0; i < _hexCellList.Count; i++)
        {
            Destroy(_hexCellList[i].gameObject); //Destroys all the cells currently in the list
        }

        for (int i = 0; i < _hexCoordinatesLabelList.Count; i++)
        {
            Destroy(_hexCoordinatesLabelList[i].gameObject); //Destroys all the coordinate labels in the current list
            
        }
        _hexCellList.Clear();
        _hexCoordinatesLabelList.Clear(); //resets the list (clears all index values)
    }
    
    public void ChangeHexSize()
    {
        //Changes the size of a hex cell units
        _hexCellSize = float.Parse(_inputfields[0].text) ;
        Size = _hexCellSize;
    }

    public void ChangeGridHeight()
    {
        //Changes the grids length / height
        _gridHeight = Convert.ToInt32(_inputfields[1].text);
    }

    public void ChangeGridWidth()
    {
        //Changes the grids width
        _gridWidth = Convert.ToInt32(_inputfields[2].text);
    }
}
