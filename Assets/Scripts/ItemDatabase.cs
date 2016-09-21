using UnityEngine;
using System.Collections;
using System.Collections.Generic; // Allows us to use lists
using System.IO; //Acces tge file system

// Allows us to take JSON data and turn it into a c# object
using LitJson; 

public class Stat
{
    public int Power { get; set; }
    public int Defence { get; set; }
    public int Vitality { get; set; }
    public Stat(Stat stats)
    {
        this.Power = stats.Power;
        this.Defence = stats.Defence;
        this.Vitality = stats.Vitality;
    }
    public Stat(JsonData data)
    {
        Power = (int)data["stats"]["power"];
        Defence = (int)data["stats"]["defence"];
        Vitality = (int)data["stats"]["vitality"];
    }
}
public class Item
{
    public int ID { get; set; }
    public string Title { get; set; }
    public int Value { get; set; }
    public Stat Stats { get; set; }
    public string Description { get; set; }
    public bool Stackable { get; set; }
    public int Rarity { get; set; }
    public Sprite Sprite { get; set; }
    public GameObject gameObject { get; set; }

    public Item()
    {
        // Set the ID to -1 indicating that
        // The item has not been set
        ID = -1;
    }

    public Item(JsonData data)
    {
        ID = (int)data["id"];
        Title = data["title"].ToString();
        Value = (int)data["value"];
        Stats = new Stat(data);
        Description = data["description"].ToString();
        Stackable = (bool)data["stackable"];
        Rarity = (int)data["rarity"];
        string fileName = data["sprite"].ToString();
        Sprite = Resources.Load<Sprite>("Sprites/Items/" + fileName);
        
    }

}
public class ItemDatabase : MonoBehaviour
{
    private Dictionary<string, Item> database = new Dictionary<string, Item>();

    // Holds the JSON data we pull in from the scene
    private JsonData itemData;

    private static ItemDatabase instance = null;
    void Awake()
    {
        // Check if instance is null
        if(instance == null)
        {
            // Set instance to the current instance
            instance = this;
            // Obtain the file path for Items.jason
            string jsonFilePath = Application.dataPath + "/StreamingAssets/Items.json";
            // Read the entire file into astring
            string jsonText = File.ReadAllText(jsonFilePath);
            // Load in the data through JsonMapper
            itemData = JsonMapper.ToObject(jsonText);
            // Construct the item database
            ConstructDatabase();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void ConstructDatabase()
    {
        // Loop through all the items inside of itemData
        for (int i = 0; i < itemData.Count; i++)
        {
            // Obtain the data
            JsonData data = itemData[i];
            // Create a new item
            Item newItem = new Item(data);
            // Add item to database
            database.Add(newItem.Title, newItem);
        }
    }

    public static Item GetItem(string itemName)
    {
        // Store database into shorter name
        Dictionary<string, Item> database = instance.database;
        // CHeck if the item exists in database
        if(database.ContainsKey(itemName))
        {
            // Return the matched item
            return database[itemName];
        }
        // Otherwise, return null
        return null; 
    }

    public static Dictionary<string, Item> GetDatabase()
    {
        // Return the database from singleton
        return instance.database; 
    }
}
