using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;    

public class InteractionSystem : MonoBehaviour
{ 
    [Header("Detection Parameters")]
    //Detection Point
    public Transform detectionPont;
    //Detection Radius
    private const float detectionRadius = 0.2f;
    //Detection Layer
    public LayerMask detectionLayer;
    //Cached Trigger Object
    public GameObject detectedObject;
    [Header("Examine Fields")]
    //Examine window object
    public GameObject examinWindow;
    public Image examinImage;
    public Text examintText;
    public bool isExamining; 
    
    

    void Update()
    {
        if(DetectObject())
        {
            if(InteractInput())
            {
                detectedObject.GetComponent<Item>().Interact();
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(detectionPont.position, detectionRadius);
    }


    bool InteractInput()
    {
        return Input.GetKeyDown(KeyCode.E);

    }

    bool DetectObject()
    {
        Collider2D obj= Physics2D.OverlapCircle(detectionPont.position,detectionRadius,detectionLayer);
        if(obj == null)
        {
            detectedObject = null;
            return false;
        }
        else
        {
            detectedObject = obj.gameObject;
            return true;
        }

        
    }

    
    

    public void ExamineItem(Item item)
    {
        if (isExamining)
        {
            Debug.Log("CLOSE");
            examinWindow.SetActive(false);
            isExamining = false; 
        }
        else
        {
            Debug.Log("EXAMINE");
            examinImage.sprite = item.GetComponent<SpriteRenderer>().sprite;
            examintText.text = item.descriptionText;
            examinWindow.SetActive(true);
            isExamining = true;
        }
        
    }
}
