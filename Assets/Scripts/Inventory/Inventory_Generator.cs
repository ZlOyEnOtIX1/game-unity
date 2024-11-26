using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellInventory : ScriptableObject
{
    public Base_object item;
    public int count;

    public CellInventory()
    {
        item = null;
        count = 0;
    }
    public CellInventory(Base_object item, int count)
    {
        this.item = item;
        this.count = count;
    }
    public void setValue(Base_object item, int count)
    {
        this.item = item;
        this.count = count;
    }
    public void upgradeValue(int count)
    {
        this.count = count;
    }
    public bool isCompleted()
    {
        return count == item.maxStack;
    }
}

[System.Serializable]

public class Inventory_Generator
{
    [SerializeField] private GameObject cellItem;
    [SerializeField] private GameObject panel;
    [SerializeField] private int countRows;
    [SerializeField] private int countColumns;
    [SerializeField] private type_of_inventory type;
    [SerializeField] private int cellSize;
    [SerializeField] public int maxSize;
    private List<GameObject> inventory;

    public void AddGraphic()
    {
        inventory = new List<GameObject>();
        int counterSize = 0;
        for (int i = 0; i < countRows; i++)
        {
            for (int x = 0; x < countColumns; x++)
            {
                if (counterSize >= maxSize)
                {
                    break;
                }
                GameObject newItem = MonoBehaviour.Instantiate(cellItem, panel.transform);
                newItem.name = i.ToString() + ' ' + x.ToString();

                RectTransform rt = newItem.GetComponent<RectTransform>();
                rt.localPosition = new Vector3(0, 0, 0);
                rt.localScale = new Vector3(1, 1, 1);
                newItem.GetComponentInChildren<RectTransform>().localScale = new Vector3(1, 1, 0);
                inventory.Add(newItem);

                panel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(cellSize, cellSize);
                panel.GetComponent<GridLayoutGroup>().constraint = GridLayoutGroup.Constraint.FixedColumnCount;
                panel.GetComponent<GridLayoutGroup>().constraintCount = countColumns;
                counterSize++;

            }
        }
    }

    public void RemoveGraphic(int position, CellInventory ci)
    {
        inventory[position].GetComponentInChildren<Text>().text = ci.count.ToString();
        inventory[position].GetComponent<Image>().sprite = ci.item.go.GetComponent<SpriteRenderer>().sprite;
    }
}
