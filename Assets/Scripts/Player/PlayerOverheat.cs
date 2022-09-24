using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using xPoke.CustomLog;
using System;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerOverheat : MonoBehaviour
{
    public float Overheat { get; private set; }
    public bool IsOverheated { get { return Overheat >= slowDownLevel; } }

    [Header("References")]
    [SerializeField]
    private GameObject overheatMask;
    [SerializeField]
    private Light2D light2D;
   
    [SerializeField]
    private PlayerJump playerJump;

    [Header("VFX")]
    [SerializeField]
    private GameObject[] overheatSmokes;

    [Header("Audio")]
    [SerializeField]
    private AudioClip gameOverExplosionClip;

    [Header("Settings")]
    [SerializeField]
    private float heatIncreaseAmount = 0.1f;
    [SerializeField]
    private float heatDecreaseAmount = 0.1f;
    [SerializeField]
    private float slowDownLevel = 0.7f;

    private bool _isSlowedDown = false;
    private TerrainTileSpawner _terrainTileSpawner;

    private bool _isIncreaseActive = true;
    private bool _isDecreaseActive = true;

    public void SetActiveOverheating(bool active)
    {
        _isIncreaseActive = active;
        _isDecreaseActive = active;
    }

    public void SetActiveIncreaseOverheating(bool active)
    {
        _isIncreaseActive = active;
    }

    public void SetActiveDecreaseOverheating(bool active)
    {
        _isDecreaseActive = active;
    }

    public void IncreaseHeat()
    {
        if (!_isIncreaseActive)
            return;
        IncreaseHeat(heatIncreaseAmount);
    }

    public void DecreaseHeat()
    {
        if (!_isDecreaseActive)
            return;
        DecreaseHeat(heatDecreaseAmount);
    }

    public void IncreaseHeat(float amount)
    {
        if (!_isIncreaseActive)
            return;

        if (amount < 0 || amount > 1)
            return;

        Overheat += amount;

        if(Overheat > 1)
            Overheat = 1;

        CustomLog.Log(CustomLog.CustomLogType.PLAYER, "Overheat: " + Overheat);
        light2D.intensity = Overheat;

        if(Overheat >= slowDownLevel && (!_isSlowedDown))
        {
            CustomLog.Log(CustomLog.CustomLogType.PLAYER, "Slow Down Triggered ");

            _terrainTileSpawner.ApplyOverheatSlowDown();

            _isSlowedDown = true;
            SetSmokeEffectsActive(true);
        }

        overheatMask.transform.localPosition = new Vector3(overheatMask.transform.localPosition.x, -(1.0f - Overheat), 0.0f);

        
        if(Overheat >= 1 && playerJump.IsGrounded)
        {
            // GameOver
            AudioController.Instance.PlaySFX(gameOverExplosionClip);
            GameController.Instance.GameOver();
            CustomLog.Log(CustomLog.CustomLogType.GAMEPLAY, "GAME OVER for Overheating");
            GetComponent<Animator>().SetTrigger("Explode");
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Destroy(gameObject,1.0f);
        }
        
    }

    public void DecreaseHeat(float amount)
    {
        if (amount < 0 || amount > 1)
            return;

        Overheat -= amount;

        if (Overheat < 0)
            Overheat = 0;

        CustomLog.Log(CustomLog.CustomLogType.PLAYER, "Overheat: " + Overheat);
        light2D.intensity = Overheat;

        if (Overheat < slowDownLevel && _isSlowedDown)
        {
            CustomLog.Log(CustomLog.CustomLogType.PLAYER, "Speed to normal ");

            _terrainTileSpawner.ApplyNormalSpeed();

            _isSlowedDown = false;
            SetSmokeEffectsActive(false);
        }

        overheatMask.transform.localPosition = new Vector3(overheatMask.transform.localPosition.x, -(1.0f-Overheat), 0.0f);
    }

    private void Awake()
    {
        _terrainTileSpawner = FindObjectOfType<TerrainTileSpawner>();
    }

    private void Start()
    {
        Overheat = 0.0f;
        SetSmokeEffectsActive(false);
        light2D.intensity = 0;
    }

    private void SetSmokeEffectsActive(bool active)
    {
        if (overheatSmokes.Length <= 0)
            return;

        foreach(GameObject obj in overheatSmokes)
        {
            obj.SetActive(active);
        }
    }
}
