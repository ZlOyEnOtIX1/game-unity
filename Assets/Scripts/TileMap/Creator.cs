using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creator : MonoBehaviour
{
    [SerializeField] private TileMapGenerator tilemap;
    void Start()
    {
        tilemap.create_new_map();

        foreach (var tile in tilemap.tile_map)
        {
            tile.bo.Instantiate(tile.pos_x, tile.pos_y);
        }
    }
}
