using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] private int guaranteedMinDrops = 1; 
    [SerializeField] private int possibleItemDrop = 3;
    [SerializeField] private ItemData[] possibleDrop; 
    private List<ItemData> dropList = new List<ItemData>();

    [SerializeField] private GameObject dropPrefab;

    public virtual void GenerateDrop()
    {
        if (possibleDrop == null || possibleDrop.Length == 0)
        {
            Debug.LogWarning("No items to drop for this enemy.");
            return;
        }

        dropList.Clear(); 

        foreach (var item in possibleDrop)
        {
            if (Random.Range(0, 100) < item.dropChance) 
            {
                dropList.Add(item);
            }
        }

        // Ensure minimum guaranteed drops
        while (dropList.Count < guaranteedMinDrops)
        {
            ItemData randomItem = possibleDrop[Random.Range(0, possibleDrop.Length)];
            // Ensure no duplicates
            if (!dropList.Contains(randomItem))
            {
                dropList.Add(randomItem);
            }
        }

        // Limit the drop count to possibleItemDrop or the size of dropList
        int dropCount = Mathf.Min(possibleItemDrop, dropList.Count);

        for (int i = 0; i < dropCount; i++)
        {
            int randomIndex = Random.Range(0, dropList.Count); 
            ItemData randomItem = dropList[randomIndex];

            dropList.RemoveAt(randomIndex); 
            DropItem(randomItem);
        }
    }

    protected void DropItem(ItemData _itemData)
    {
        GameObject newDrop = Instantiate(dropPrefab, transform.position, Quaternion.identity);
        Vector2 randomVelocity = new Vector2(Random.Range(-5, 5), Random.Range(15, 20));

        newDrop.GetComponent<ItemObject>().SetupItem(_itemData, randomVelocity);
        Debug.Log($"Dropped item: {_itemData.name} with velocity: {randomVelocity}");
    }
}
