using System;

[Serializable]
public class InventoryItem
{
    public ItemData data;
    public int stackSize;

    public InventoryItem(ItemData _newItemdata)
    {
        data = _newItemdata;
        AddStack();
    }

    public void AddStack() => stackSize++;

    public void RemoveStack() => stackSize--;
}
