using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private ItemDrop itemDrop;
    private Animator anim;
    private bool isOpen = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isOpen)
        {
            Debug.Log("Press 'E' to open the chest.");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("You left the chest area.");
        }
    }

    private void Update()
    {
        if (isOpen) return; 

        if (Input.GetKeyDown(KeyCode.E))
        {
            OpenChest();
        }
    }

    private void OpenChest()
    {
        isOpen = true; 
        Debug.Log("Chest opened!");

        anim.SetBool("Open", true);
        AudioManager.instance.PlaySFX(34, transform);

        itemDrop.GenerateDrop();
    }
}
