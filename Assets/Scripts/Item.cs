using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System;

public class Item : MonoBehaviour {
    public int id;
    public string itemName;
    public float addHp;
    public float addAttack;
    public float addDefence;
    public int addGold;
}

public class ItemData
{
    public int id;
    public string name;
    public float addHp;
    public float addAttack;
    public float addDefence;
    public int addGold;

    static public ItemData GetItemDataFromNetwork(int id)
    {
        string message = CreateMessageGetItemDataFromNetwork(id);
        string result = SocketClient.send(message);
        //JsonReader reader = new JsonTextReader(new StringReader(result));
        JObject jo = (JObject)JsonConvert.DeserializeObject(result);
        ItemData itemData = new ItemData();
        if (jo["type"].ToString() == "item")
        {
            itemData.id = Int32.Parse(jo["body"]["id"].ToString());
            itemData.name = jo["body"]["name"].ToString();
            itemData.addHp = float.Parse(jo["body"]["hp"].ToString());
            itemData.addAttack = float.Parse(jo["body"]["attack"].ToString());
            itemData.addDefence = float.Parse(jo["body"]["defence"].ToString());
            itemData.addGold = Int32.Parse(jo["body"]["gold"].ToString());
        }
        return itemData;
    }

    static public string CreateMessageGetItemDataFromNetwork(int id)
    {
        string message = "{ \"type\": \"item\", \"body\":{\"id\":\"" + id + "\"}}";
        return message;
    }
}
