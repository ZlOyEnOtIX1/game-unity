using System;
using System.Diagnostics;
using UnityEditor;


public abstract class Base_object : UnityEngine.ScriptableObject
{
    new public string name;
    public int maxStack;
    public UnityEngine.GameObject go;
    public abstract void use();
    public abstract System.Type GetType();
    public static Base_object find_by_name(string name)
    {
        // Находим все ресурсы с расширением .asset
        string[] guids = AssetDatabase.FindAssets("t:Base_object");
        // Создаем массив для хранения всех найденных блоков
        Base_object[] base_objects = new Base_object[guids.Length];
        for (int i = 0; i < guids.Length; i++)
        {
            // Получаем путь до ресурса по его GUID
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);

            // Загружаем ресурс
            base_objects[i] = AssetDatabase.LoadAssetAtPath<Base_object>(path);
            if (base_objects[i].name.Equals(name))
            {
                return base_objects[i];
            }
        }
        return null;
    }

    public void Instantiate(Base_object bo, int x, int y)
    {
        Instantiate(bo.go, new UnityEngine.Vector3(x, y, 0), new UnityEngine.Quaternion());
    }
    public void Instantiate(int x, int y)
    {
        Instantiate(this.go, new UnityEngine.Vector3(x, y, 0), new UnityEngine.Quaternion());
    }
    public void Instantiate(Tile tile_map)
    { 
        UnityEngine.Debug.Log(tile_map.bo.name);
        Instantiate(tile_map.bo.go, new UnityEngine.Vector3(tile_map.pos_x, tile_map.pos_y, 0), new UnityEngine.Quaternion());
    }
    public static bool operator ==(Base_object lhs, object rhs)
    {
        if (lhs is null || rhs is null)
        {
            return false;
        }
        return lhs.Equals(rhs);
    }
    public static bool operator !=(Base_object lhs, object rhs)
    {
        if (lhs is null || rhs is null)
        {
            return false;
        }
        return !lhs.Equals(rhs);
    }

    public override bool Equals(object other)
    {
        if (other == null) return false;
        if (base.GetType() != other.GetType())
        {
            return false;
        }
        else
        {
            return base.Equals(other);
        }
    }

}
