using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashMultiplier : OneTimePowerUp
{
    [Header("Power Up Settings")]
    [SerializeField]
    private int multiplier = 2;
    [SerializeField]
    private float duration = 10.0f;

    private PlayerCollision _playerCollision;
    private float _timer = 0.0f;
    private bool _stop = false;
    protected override void ExecuteOnce()
    {
        base.ExecuteOnce();
        _playerCollision = FindObjectOfType<PlayerCollision>();

        _playerCollision.SetTrashCountMultiplier(multiplier);
    }

    public override void Activate()
    {
        base.Activate();
        _timer += Time.deltaTime;
        if(_timer >= duration && !_stop)
        {
            Deactivate();
            _stop = true;
        }
    }

    public override void Deactivate()
    {
        _playerCollision.SetTrashCountMultiplier(1);
        base.Deactivate();
    }
}
