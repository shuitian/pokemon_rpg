using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityTool.Libgame;

public class PlayerShow : MonoBehaviour {

    public Text nameText;
    public Text hpText;
    public Text attackText;
    public Text defenceText;
    public Text goldText;

    void Awake()
    {
        Message.RegeditMessageHandle<Player>("PlayerShow", UpdateInformation);
    }

    void Destroy()
    {
        Message.UnregeditMessageHandle<Player>("PlayerShow", UpdateInformation);
    }

    public void UpdateInformation(string messageName, object sender, Player player)
    {
        if (player)
        {
            //if (nameText)
            //{
            //    nameText.text = player.characterName;
            //}
            if (hpText)
            {
                hpText.text = "" + player.GetCurrentHp();
            }
            if (attackText)
            {
                attackText.text = "" + player.GetAttack();
            }
            if (defenceText)
            {
                defenceText.text = player.GetDefence() + "%";
            }
            if (goldText)
            {
                goldText.text = "" + player.GetCurrentGold();
            }
        }
    }
}
