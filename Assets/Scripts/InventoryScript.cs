using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryScript : MonoBehaviour
{
    [Header("Slots")]
    public GameObject slotPrefab;
    public int slotAmount; 
    public GameObject slotPanel;

    void Start()
    {
        Dictionary<string, Item> database = ItemDatabase.GetDatabase();
        print(database);
        for (int i = 0; i < slotAmount; i++)
        {
            //Instantiate new slot
            GameObject slot = Instantiate(slotPrefab);
            //Set it's position to be relative to slot panel
            slot.transform.position = slotPanel.transform.position;
            //Set slot's parent to be slot panel
            slot.transform.SetParent(slotPanel.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
