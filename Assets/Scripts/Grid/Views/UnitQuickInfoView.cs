using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitQuickInfoView : MonoBehaviour {

    public GameObject ArmorObject;
    public GameObject CombatStatsObject;
    public GameObject MovementObject;
    public Text ArmorText;
    public Text PowerText;
    public Text HealthText;
    public Text MovementText;

    public void Initialize(int armor, int power, int health, int movement)
    {
        ArmorText.text = armor.ToString();
        PowerText.text = power.ToString();
        HealthText.text = health.ToString();
        MovementText.text = movement.ToString();

        if (armor == 0)
            ArmorObject.SetActive(false);
        MovementObject.SetActive(false);
    }

    public void ShowMovement()
    {
        CombatStatsObject.SetActive(false);
        MovementObject.SetActive(true);
    }
    public void ShowCombatStats()
    {
        CombatStatsObject.SetActive(true);
        MovementObject.SetActive(false);
    }
}
