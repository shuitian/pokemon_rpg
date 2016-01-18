using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerShow : MonoBehaviour {

    public Text nameText;
    public Text hpText;
    public Text attackText;
    public Text defenceText;
    
    void UpdateImformation(Player player)
    {
        if (player)
        {
            if (nameText)
            {
                nameText.text = player.playerName;
            }
            if (hpText)
            {
                hpText.text = player.hp + "/" + player.maxHp;
            }
            if (attackText)
            {
                attackText.text = "" + player.attack;
            }
            if (defenceText)
            {
                defenceText.text = player.defence + "%";
            }
        }
    }

    void Update()
    {
        UpdateImformation(Player.Instance());
    }
}
