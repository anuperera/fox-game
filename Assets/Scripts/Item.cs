using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(BoxCollider2D))]
public class Item : MonoBehaviour
{
   public enum InteractionType { None,PickUp, Examine}

    public enum ItemType { Static, Consumable }
    [Header("Attributes")]
    public InteractionType interactType;
    public ItemType type;
    [Header("Examine")]
    public string descriptionText;
    [Header("Custom Events")]
    public UnityEvent customEvent;
    public UnityEvent consumeEvent;


    private void Reset()
    {
        GetComponent<Collider2D>().isTrigger = true;
        gameObject.layer = 7;
    }

    public void Interact()
    {
        switch(interactType)
        {
            case InteractionType.PickUp:
                FindObjectOfType<InventorySystem>().PickUp(gameObject);
                gameObject.SetActive(false);
                break;
            case InteractionType.Examine:
                FindObjectOfType<InteractionSystem>().ExamineItem(this);
                break;
            default:
                Debug.Log("NULL ITEM");
                break;
        }
        //invoke (call) the custom event(s)
        customEvent.Invoke();
    }
}
