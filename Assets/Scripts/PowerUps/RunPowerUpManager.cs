using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using xPoke.CustomLog;

public class RunPowerUpManager : Singleton<RunPowerUpManager>
{
    [SerializeField]
    private OneTimePowerUpList powerUpList;

    private OneTimePowerUp _currentPowerUp = null;

    public void ResetManager()
    {
        _currentPowerUp = null;
    }

    public void AddPowerUp(Type powerUpType)
    {
        Debug.Log(powerUpType.ToString());
       
        OneTimePowerUp newPowerUp = Instantiate(powerUpList.GetPowerUpByType(powerUpType));
        newPowerUp.gameObject.GetComponentInChildren<SpriteRenderer>().gameObject.SetActive(false);
        if (_currentPowerUp != null)
        {
            _currentPowerUp.Deactivate();
            _currentPowerUp = null;
        }

        _currentPowerUp = newPowerUp;
        CustomLog.Log(CustomLog.CustomLogType.GAMEPLAY, "PowerUp " + _currentPowerUp.GetType().ToString() + " Activated");
    }
    private void Start()
    {
        foreach(OneTimePowerUp powerUp in powerUpList.powerUpList)
        {
            powerUp.PowerUpData.UpdateData();
        }
    }

    private void Update()
    {
        if(_currentPowerUp != null)
        {
            _currentPowerUp.Activate();
        }
    }
}
