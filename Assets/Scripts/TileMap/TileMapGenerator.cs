using System;
using UnityEngine;

[System.Serializable]
public struct Map
{
    public int Height;
    public int Width;
    public float AirPercent;

}

[Serializable]
public struct Tile
{
    public Base_object bo;
    public int pos_x;
    public int pos_y;

    public Tile(Base_object bo, int pos_x, int pos_y)
    {
        this.bo = bo;
        this.pos_x = pos_x;
        this.pos_y = pos_y;
    }
}

public class TileMapGenerator : MonoBehaviour 
{
    [SerializeField] private Map tile_map_params;

    public System.Collections.Generic.List<Tile> tile_map;

    public int Height;
    public int Width;
    public float AirPercent;

    private void Awake()
    {
        this.Height = tile_map_params.Height; //вниз
        this.Width = tile_map_params.Width; //вправо
        this.AirPercent = tile_map_params.AirPercent;
        tile_map = new System.Collections.Generic.List<Tile>();
        //LoadMap();
    }

    private bool create_empty_map(int Height, int Width)
    {
        try
        {
            Base_object earth = Base_object.find_by_name("Земля");
            for (int i = 0; i < Width; i++)
            {
                for (int x = 0; x < Height; x++)
                {
                    this.tile_map.Add(new Tile(earth, i, x));
                }
            }
            return true;
        }
        catch
        {
            return false;
        }
    }

    private bool add_air()
    {
        try
        {
            var air = Base_object.find_by_name("Воздух");
            for (int x = 0; x < this.Width * this.AirPercent/100; x++)
            {
                for (int y = 0; y < this.Height; y++)
                {
                    this.tile_map.Add(new Tile(air, x, y));
                }
            }
            return true;
        }
        catch
        {
            Console.WriteLine("AddAirFunction");
            return false;
        }
    }

    public bool create_new_map()
    {
        create_empty_map(Height, Width);
        add_air();
        return true;
    }

    private bool load_map()
    {
        return true;
    }

    //private void OnDestroy()
    //{
    //    for (int i = 0; i < Width; i++)
    //    {
    //        for (int x = 0; x < Height; x++)
    //        {
    //            PlayerPrefs.SetString($"tm{i}_{x}", (string)tile_map[i, x].name);
    //        }
    //    }
    //}
    //public void LoadMap()
    //{
    //    for (int i = 0; i < Width; i++)
    //    {
    //        for (int x = 0; x < Height; x++)
    //        {
    //            //tile_map[i, x] = Base_object.find_by_name(PlayerPrefs.GetString($"tm{i}_{x}"));
    //        }
    //    }
    //    //InstantiateWorld();
    //}

}
