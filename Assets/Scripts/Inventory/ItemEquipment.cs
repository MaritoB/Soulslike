using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEquipment : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform RightHand;
    public Transform LeftHand;
    void Start()
    {
        
    }
   
    // Update is called once per frame
    void Update()
    {
        
    }
    public void EquipOnRightHand(Transform item)
    {
        item.parent = RightHand;
        item.transform.position = RightHand.position;
    }
}
