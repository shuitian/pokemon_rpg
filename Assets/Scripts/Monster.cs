using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

[RequireComponent(typeof(MonsterHpComponent))]
public class Monster : Character
{ 
    public int id;
    public int gold;
}

public class MonsterData
{
    public int id;
    public string name;
    public float hp;
    public float attack;
    public float defence;
    public int gold;

    static public MonsterData GetMonsterDataFromNetwork(int id)
    {
        string message = CreateMessageGetMonsterDataFromNetwork(id);
        string result = SocketClient.send(message);
        if (result == null)
        {
            return null;
        }
        JObject jo = (JObject)JsonConvert.DeserializeObject(result);
        MonsterData monsterData = new MonsterData();
        if (jo["type"].ToString() == "monster")
        {
            monsterData.id = Int32.Parse(jo["body"]["id"].ToString());
            monsterData.name = jo["body"]["name"].ToString();
            monsterData.hp = float.Parse(jo["body"]["hp"].ToString());
            monsterData.attack = float.Parse(jo["body"]["attack"].ToString());
            monsterData.defence = float.Parse(jo["body"]["defence"].ToString());
            monsterData.gold = Int32.Parse(jo["body"]["gold"].ToString());
        }
        return monsterData;
    }

    static public string CreateMessageGetMonsterDataFromNetwork(int id)
    {
        return JsonConvert.SerializeObject(new { type = "monster", body = new { id = id } });
    }
}
