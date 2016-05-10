using UnityEngine;
using System.Collections;

public class testSocket : MonoBehaviour {

    void Awake()
    {
        SocketClient.setServer("127.0.0.1", 10019);
    }

	// Use this for initialization
	void Start () {
        //SocketClient.send("123");
        ItemData itemData =  ItemData.GetItemDataFromNetwork(4);
        print(itemData.id);
        print(itemData.name);
        print(itemData.addHp);
        print(itemData.addAttack);
        print(itemData.addDefence);
        print(itemData.addGold);
    }
	
	// Update is called once per frame
	void Update () {
	    
	}
}
