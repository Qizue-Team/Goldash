using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using xPoke.CustomLog;

public abstract class OneTimePowerUp : Spawnable
{
    protected bool _isExecuted = false; 
    protected virtual void ExecuteOnce()
    {
        _isExecuted = true;
    }

    public virtual void Activate()
    {
        if(!_isExecuted)
            ExecuteOnce();
    }

    public virtual void Deactivate()
    {
        CustomLog.Log(CustomLog.CustomLogType.GAMEPLAY,"PowerUp "+this.GetType().ToString()+" Deactivated");
        Destroy(gameObject);
    }
}
