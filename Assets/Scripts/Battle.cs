using UnityEngine;
using System.Collections;

public class Battle {

    public static void battle(Player player, Monster monster)
    {
        while (player.hp > 0 && monster.hp > 0)
        {
            if (player.hp > 0 && monster.hp > 0)
            {
                monster.Attack(player);
            }
            if (player.hp > 0 && monster.hp > 0)
            {
                player.Attack(monster);
            }
        }
    }
}
