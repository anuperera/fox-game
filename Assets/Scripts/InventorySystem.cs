using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    [Header("General Fields")]
    public List<GameObject> items = new List<GameObject>();
    public bool isOpen;
    [Header("UI Items Section")]
    public GameObject ui_Window;
    public Image[] item_images;
    [Header("UI Item Description")]
    public GameObject ui_Description_Window;
    public Image description_Image;
    public Text description_Title;
    public Text description_Text;

    private void Update()
    {

       if(Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    void ToggleInventory()
    {
        isOpen = !isOpen;
        ui_Window.SetActive(isOpen);

        Update_UI();
    }

    public void PickUp(GameObject item)
    {
        items.Add(item);
        Update_UI();
    }

    void Update_UI()
    {
        HideAll();
        for(int i=0;i<items.Count;i++)
        {
            item_images[i].sprite = items[i].GetComponent<SpriteRenderer>().sprite;
            item_images[i].gameObject.SetActive(true);
        }
    }

    void HideAll()
    {
        foreach ( var i in item_images)
        {
            i.gameObject.SetActive(false);
            
        }
        HideDescription();
    }

    public void ShowDescription(int id)
    {
        description_Image.sprite = item_images[id].sprite;
        description_Title.text = items[id].name;
        description_Text.text = items[id].GetComponent<Item>().descriptionText;
        description_Image.gameObject.SetActive(true);
        description_Title.gameObject.SetActive(true);
        description_Text.gameObject.SetActive(true); 

    }

    public void HideDescription()
    {
        description_Image.gameObject.SetActive(false);
        description_Title.gameObject.SetActive(false);
        description_Text.gameObject.SetActive(false);
    }

    public void Consume(int id)
    {
        if(items[id].GetComponent<Item>().type == Item.ItemType.Consumable)
        {
            Debug.Log($"CONSUMED { items[id].name}");
            items[id].GetComponent<Item>().consumeEvent.Invoke();
            Destroy(items[id], 0.1f);
            items.RemoveAt(id);
            Update_UI();   

        }
    }
}
