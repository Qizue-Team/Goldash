using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName ="RunPowerUpList", menuName = "PowerUp List")]
public class OneTimePowerUpList : ScriptableObject
{
    public List<OneTimePowerUp> powerUpList;

    public OneTimePowerUp GetPowerUpByType(Type powerUpType)
    {
        foreach(var powerUp in powerUpList)
        {
            if (powerUpType.Equals(powerUp.GetType()))
            {
                return powerUp;
            }
        }
        return null;
    }
}
