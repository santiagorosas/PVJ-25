using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    [SerializeField] private GameObject _blockPrefab;
    [SerializeField] private List<Color> _colorsByRow;

    public GameObject BlockPrefab { get => _blockPrefab; }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int blockColumnCount = 8;
        int blockRowCount = 4;
        float leftmostBlockX = -7;
        float topBlockY = 3.55f;
        float xDistanceBetweenBlocks = _blockPrefab.transform.localScale.x + 0.2f;
        float yDistanceBetweenBLocks = _blockPrefab.transform.localScale.y + 0.2f;

        for (int i=0; i<blockColumnCount; i++)
        {
            for (int j=0; j<blockRowCount; j++)
            {
                float blockX = leftmostBlockX + xDistanceBetweenBlocks * i;
                float blockY = topBlockY - yDistanceBetweenBLocks * j;

                GameObject blockObject = Instantiate(
                    original: BlockPrefab,
                    parent: transform,
                    position: new Vector2(blockX, blockY),
                    rotation: Quaternion.identity);

                Color color = GetRowColor(rowIndex: j);
                blockObject.GetComponent<Block>().SetColor(color);
            }            
        }
    }

    private Color GetRowColor(int rowIndex)
    {
        return _colorsByRow[rowIndex];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
