using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryController : Inventory
{
    [SerializeField] private Inventory upperInventory;
    [SerializeField] private Inventory hiddenInventory;
    private int z = 0;
    private Base_object earth;
    void Start()
    {
        
    }
    private void FixedUpdate()
    {
        if ((upperInventory.checkTrash() is null) == false)
        {
            Debug.Log(upperInventory.checkTrash() is null);
            hiddenInventory.AddItem(upperInventory.checkTrash());
        }
    }
    void Update()
    {
        if (z == 0)
        {
            earth = Base_object.find_by_name("Земля");
            AddItem(earth, 10000); 
            z++;
        }
    }

    public void AddItem(Base_object item, int count)
    {
        if (upperInventory.isIncludedIn(item))
        {
            upperInventory.AddItem(item, count);
            
        }
        else if (hiddenInventory.isIncludedIn(item))
        {
            hiddenInventory.AddItem(item, count);
        }
        else
        {
            Debug.Log("нет места в инвентаре");
        }

        Debug.Log("11");
        if (!(upperInventory.checkTrash() is null))
        {
            Debug.Log("go to hidden inventory");
            hiddenInventory.AddItem(item, count);
        }
    }
}
