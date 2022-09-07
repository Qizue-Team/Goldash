using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using xPoke.CustomLog;

public class Player : MonoBehaviour
{
    private bool _isGameOverDeclared = false;
    private AudioManager _audioManager;

    private void Start()
    {
        _audioManager = GetComponentInChildren<AudioManager>();
    }

    private void Update()
    {
        if (transform.position.y < -6.0f && !_isGameOverDeclared)
        {
            _audioManager.PlayClibByName("Trasher_GameOverGeneral");
            CustomLog.Log(CustomLog.CustomLogType.GAMEPLAY, "GameOver for falling");
            GameController.Instance.GameOver();
            _isGameOverDeclared = true;
            Destroy(this.gameObject,1.0f);
        }
    }
}
