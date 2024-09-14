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
        dropList.Clear(); // Clear the list before generating new items

        for (int i = 0; i < possibleDrop.Length; i++)
        {
            if (Random.Range(0, 100) <= possibleDrop[i].dropChance)
            {
                dropList.Add(possibleDrop[i]);
            }
        }

        if (dropList.Count < guaranteedMinDrops)
        {
            // If the dropList is too small, add random items from possibleDrop to ensure the minimum
            while (dropList.Count < guaranteedMinDrops)
            {
                ItemData randomItem = possibleDrop[Random.Range(0, possibleDrop.Length)];
                dropList.Add(randomItem);
            }
        }

        // Adjust the final drop count to be within possibleItemDrop or the size of dropList
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
