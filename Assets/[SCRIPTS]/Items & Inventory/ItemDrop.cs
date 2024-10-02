using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] private int guaranteedMinDrops = 1; // Minimum guaranteed drops
    [SerializeField] private int possibleItemDrop; // Maximum number of items to drop
    [SerializeField] private ItemData[] possibleDrop; // Array of possible items to drop
    private List<ItemData> dropList = new List<ItemData>();

    [SerializeField] private GameObject dropPrefab;

    public virtual void GenerateDrop()
    {
        // Safety check: if no possible items, return without generating drops
        if (possibleDrop == null || possibleDrop.Length == 0)
        {
            Debug.LogWarning("No items to drop for this enemy.");
            return;
        }

        dropList.Clear(); // Clear the list before generating new items

        for (int i = 0; i < possibleDrop.Length; i++)
        {
            if (Random.Range(0, 100) <= possibleDrop[i].dropChance)
            {
                dropList.Add(possibleDrop[i]);
            }
        }

        // Ensure minimum guaranteed drops
        if (dropList.Count < guaranteedMinDrops)
        {
            // Add random items to ensure the minimum drop count
            while (dropList.Count < guaranteedMinDrops)
            {
                ItemData randomItem = possibleDrop[Random.Range(0, possibleDrop.Length)];
                dropList.Add(randomItem);
            }
        }

        // Limit the drop count to possibleItemDrop or the size of dropList
        int dropCount = Mathf.Min(possibleItemDrop, dropList.Count);

        for (int i = 0; i < dropCount; i++)
        {
            ItemData randomItem = dropList[Random.Range(0, dropList.Count)];

            dropList.Remove(randomItem);
            DropItem(randomItem);
        }
    }

    protected void DropItem(ItemData _itemData)
    {
        GameObject newDrop = Instantiate(dropPrefab, transform.position, Quaternion.identity);

        Vector2 randomVelocity = new Vector2(Random.Range(-5, 5), Random.Range(15, 20));

        newDrop.GetComponent<ItemObject>().SetupItem(_itemData, randomVelocity);
    }
}
