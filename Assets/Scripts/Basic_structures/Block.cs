using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Block")]
public class Block : Base_object
{
    [SerializeField] private float resistance;
    [SerializeField] private int maximum_count_dropped_blocks;
    [SerializeField] private int id;
    private void OnDestroy()
    {
        
    }
    public override void use()
    {
        throw new System.NotImplementedException();
    }
    public Block[] GetAllBlocks()
    {
        // ������� ��� ������� � ����������� .asset
        string[] guids = AssetDatabase.FindAssets("t:Block");

        // ������� ������ ��� �������� ���� ��������� ������
        Block[] blocks = new Block[guids.Length];

        for (int i = 0; i < guids.Length; i++)
        {
            // �������� ���� �� ������� �� ��� GUID
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);

            // ��������� ������
            blocks[i] = AssetDatabase.LoadAssetAtPath<Block>(path);
        }

        return blocks;
    }
    public override Type GetType()
    {
        return typeof(Block);
    }
}
