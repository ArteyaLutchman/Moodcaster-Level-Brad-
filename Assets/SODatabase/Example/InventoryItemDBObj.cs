using System.Collections;
using System.Collections.Generic;
using SODatabase;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory Item", menuName = "SODatabase/Examples/InventoryItemDBO")]
public class InventoryItemDBObj : DatabaseObject
{
    public int MaxStackSize = 16;
}
