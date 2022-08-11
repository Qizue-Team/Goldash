using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using xPoke.CustomLog;

public abstract class OneTimePowerUp : Spawnable
{
    [Header("Color settings")]
    [SerializeField]
    protected Color32 color;
    [SerializeField]
    protected float intensity;

    [Header("Data")]
    [SerializeField]
    protected PowerUpData powerUpData;

    protected bool _isExecuted = false; 
    protected PowerUpParticles _particle;
    protected virtual void ExecuteOnce()
    {
        _isExecuted = true;
        Vector4 particleColor = new Vector4(color.r, color.g, color.b, intensity);
        _particle = FindObjectOfType<PowerUpParticles>();
        _particle.ActivateParticle(particleColor); 
    }

    public virtual void Activate()
    {
        if(!_isExecuted)
            ExecuteOnce();
    }

    public virtual void Deactivate()
    {
        _particle.DisableParticle();
        CustomLog.Log(CustomLog.CustomLogType.GAMEPLAY,"PowerUp "+this.GetType().ToString()+" Deactivated");
        Destroy(gameObject);
    }
}
