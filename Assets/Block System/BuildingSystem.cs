using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    public LayerMask Mask;
    public GameObject BlockGUIPrefab;
    public GameObject BlockPrefab;

    private Camera _camera;
    private GameObject _blockGUI;
    private bool _canBuild = false;
    private BlockSystem _blockSystem;
    private Vector3 _buildPos;
    private int typeSelect = 0;

    // Start is called before the first frame update
    private void Start()
    {
        _camera = Camera.main;
        _blockSystem = GetComponent<BlockSystem>();
        _blockGUI = Instantiate(BlockGUIPrefab, _buildPos, Quaternion.identity);
    }

    // Update is called once per frame
    private void Update()
    {
        //find block build location
        RaycastHit hit;
        if (Physics.Raycast(_camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)), out hit, 10, Mask))
        {
            Vector3 pos = hit.point;
            _buildPos = new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y), Mathf.Round(pos.z));
            _canBuild = true;
        }
        else
        {
            _canBuild = false;
        }

        //loop through types
        if (Input.GetMouseButtonDown(1))
        {
            typeSelect++;
            if (typeSelect >= _blockSystem.Blocks.Count) { typeSelect = 0; }
        }

        //build block
        if (_canBuild)
        {
            _blockGUI.transform.position = _buildPos; //update transparent location

            if (Input.GetMouseButtonDown(0))
            {
                PlaceBlock();
            }
        }
    }

    private void PlaceBlock()
    {
        GameObject block = Instantiate(BlockPrefab, _buildPos, Quaternion.identity);
        Block type = _blockSystem.Blocks[typeSelect];
        block.name = type.BlockName;
        block.GetComponent<MeshRenderer>().material = type.BlockMaterial;
    }
}