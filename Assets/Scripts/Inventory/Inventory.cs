using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public enum type_of_inventory
{
    hidden_inventory,
    upper_inventory,
    chest_inventory
}

public class Inventory : MonoBehaviour
{

    [SerializeField] private Inventory_Generator inventory;
    private List<CellInventory> cells;
    private bool inProgress = false;
    private List<CellInventory> queueAddItem;
    private bool isCantAdd = false;
    private bool inCheckTrash = false;
    void Start()
    {
        cells = new List<CellInventory>();
        queueAddItem = new List<CellInventory>();
        inventory.AddGraphic();
        for (int i = 0; i < inventory.maxSize; i++)
        {
            cells.Add(new CellInventory());
        }
    }
    public bool isIncludedIn(Base_object item)
    {
        if (isCantAdd)
        {
            return false;
        }
        foreach (CellInventory cell in cells)
        {
            if (cell.count == 0)
            {
                return true;
            }
            else if (cell.item == item && !cell.isCompleted())
            {
                return true;
            }
        }

        return false;
    }
    public CellInventory checkTrash()
    {
        inCheckTrash = true;
        if (isCantAdd == true)
        {
            isCantAdd = false;
            CellInventory retVallue = queueAddItem[0];
            queueAddItem.RemoveAt(0);
            Debug.Log("what the fuck");
            inCheckTrash = false;
            return retVallue;
        }
        inCheckTrash = false;
        return null;
    }
    public void AddItem(Base_object item, int count)
    {
        queueAddItem.Add(new CellInventory(item, count));
    }
    public void AddItem(CellInventory item)
    {
        queueAddItem.Add(item);
    }
    
    private void FixedUpdate()
    {
        if (!(queueAddItem is null) && !inCheckTrash)
        {
            if (queueAddItem.Count > 0)
            {
                CellInventory ci = queueAddItem[0];
                bool isAdd = false;
                int position = 0;
                foreach (var cell in cells)
                {
                    if (!(cell.item is null) && ci is not null)
                    {

                        if (cell.item.name == ci.item.name)
                        {
                            if (cell.count == cell.item.maxStack)
                            {
                                continue;
                            }
                            if (ci.item.maxStack >= cell.count + ci.count)
                            {
                                Debug.Log("enter1");
                                cell.upgradeValue(ci.count);
                                inventory.RemoveGraphic(position, cell);
                                isAdd = true; 
                                break;
                            }
                            else
                            {
                                Debug.Log("enter2");
                                ci.count = ci.count - (ci.item.maxStack - cell.count);
                                cell.upgradeValue(ci.item.maxStack);
                                inventory.RemoveGraphic(position, cell);

                            }
                        }
                    }
                    position++;
                }
                position = 0;
                if (!isAdd && ci is not null)
                {
                    foreach (var cell in cells)
                    {
                        if (cell.item is null)
                        {
                            if (ci.item.maxStack >= ci.count)
                            {
                                Debug.Log("enter3");
                                cell.setValue(ci.item, ci.count);
                                inventory.RemoveGraphic(position, cell);
                                break;
                            }
                            else
                            {
                                Debug.Log("enter4");
                                cell.setValue(ci.item, ci.item.maxStack);
                                inventory.RemoveGraphic(position, cell);
                                ci.count -= ci.item.maxStack;
                            }
                        }
                        position++;
                    }
                }
                if (ci.count == 0)
                {
                    queueAddItem.RemoveAt(0);
                }
                else
                {
                    Debug.Log("isCantAdd");
                    isCantAdd = true;
                }
            }
        }
    }
}
