using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoOverheating : OneTimePowerUp
{
    private PlayerJump _playerJump;
    private PlayerOverheat _playerOverheat;

    private int _startJumpNumber = 0;

    // Stat
    private int _numberOfJumps = 3;

    // OnActivate once
    protected override void ExecuteOnce()
    {
        base.ExecuteOnce();

        // Read CurrentStat
        _numberOfJumps = (int)powerUpData.CurrentStat; 

        _playerJump = FindObjectOfType<PlayerJump>();
        _playerOverheat = FindObjectOfType<PlayerOverheat>();

        _playerOverheat.SetActiveIncreaseOverheating(false);
        _startJumpNumber = _playerJump.JumpCount;
    }

    // Update
    public override void Activate()
    {
        base.Activate();
        
        if((_playerJump.JumpCount - _startJumpNumber) >= _numberOfJumps)
            Deactivate();
    }

    // On Destroy / Deactivation
    public override void Deactivate()
    {
        _playerOverheat.SetActiveIncreaseOverheating(true);
        base.Deactivate();
    }
}
