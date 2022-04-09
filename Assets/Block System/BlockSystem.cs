using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSystem : MonoBehaviour
{
    public BlockType[] BlockTypes;

    [HideInInspector]
    public Dictionary<int, Block> Blocks = new Dictionary<int, Block>();

    private void Awake()
    {
        for (int i = 0; i < BlockTypes.Length; i++)
        {
            BlockType blockType = BlockTypes[i];
            Block block = new Block(i, blockType.BlockName, blockType.BlockMaterial);
            Blocks.Add(i, block);
        }
    }
}

public class Block
{
    public int BlockID;
    public string BlockName;
    public Material BlockMaterial;

    public Block(int id, string name, Material mat)
    {
        BlockID = id;
        BlockName = name;
        BlockMaterial = mat;
    }
}

[Serializable]
public struct BlockType
{
    public string BlockName;
    public Material BlockMaterial;
}