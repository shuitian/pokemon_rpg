using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerShow : MonoBehaviour {

    public Text nameText;
    public Text hpText;
    public Text attackText;
    public Text defenceText;
    public Text goldText;

    void UpdateImformation(Player player)
    {
        if (player)
        {
            if (nameText)
            {
                nameText.text = player.characterName;
            }
            if (hpText)
            {
                hpText.text = "" + player.hp;
            }
            if (attackText)
            {
                attackText.text = "" + player.attack;
            }
            if (defenceText)
            {
                defenceText.text = player.defence + "%";
            }
            if (goldText)
            {
                goldText.text = "" + player.gold;
            }
        }
    }

    void Update()
    {
        UpdateImformation(Player.Instance());
    }
}
