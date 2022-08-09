using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoOverheating : OneTimePowerUp
{
    [Header("Power Up Settings")]
    [SerializeField]
    private int numberOfJumps = 3;

    private PlayerJump _playerJump;
    private PlayerOverheat _playerOverheat;

    // OnActivate once
    protected override void ExecuteOnce()
    {
        base.ExecuteOnce();
        _playerJump = FindObjectOfType<PlayerJump>();
        _playerOverheat = FindObjectOfType<PlayerOverheat>();

        _playerOverheat.SetActiveOverheating(false);
    }

    // Update
    public override void Activate()
    {
        base.Activate();
        
        if(_playerJump.JumpCount >= numberOfJumps)
            Deactivate();
    }

    // On Destroy / Deactivation
    public override void Deactivate()
    {
        _playerOverheat.SetActiveOverheating(true);
        base.Deactivate();
    }
}
