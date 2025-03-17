using System.Collections;
using System.Collections.Generic;
using SODatabase;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory Item DB", menuName = "SODatabase/Examples/InventoryItemDB")]
public class InventoryItemDB : ScriptableObjectDatabase<InventoryItemDBObj>
{
    [ContextMenu("Find")]
    public override void Find()
    {
        FindAll();
    }
}
