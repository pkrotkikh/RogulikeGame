using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerStats : MonoBehaviour
{
    public Text strengthText;
    public Text dexterityText;
    public Text intelligenceText;
    public Text armourText;
    public Text damageText;

    public Player player;

    private void Update()
    {
        strengthText.text = "Strength: " + player.playerStats[Item.Stats.Strength];
        dexterityText.text = "Strength: " + player.playerStats[Item.Stats.Dexterity];
        intelligenceText.text = "Intelligence: " + player.playerStats[Item.Stats.Intelligence];
        armourText.text = "Armour: " + player.playerStats[Item.Stats.Armour];
        damageText.text = "Damage: " + player.playerStats[Item.Stats.MinDamage] + "-" + player.playerStats[Item.Stats.MaxDamage];
    }
}
