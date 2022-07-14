using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using xPoke.CustomLog;

public class PlayerOverheat : MonoBehaviour
{
    public float Overheat { get; private set; }

    [Header("References")]
    [SerializeField]
    private GameObject overheatMask;
   
    [SerializeField]
    private PlayerJump playerJump;

    [Header("VFX")]
    [SerializeField]
    private GameObject[] overheatSmokes;

    [Header("Settings")]
    [SerializeField]
    private float heatIncreaseAmount = 0.1f;
    [SerializeField]
    private float heatDecreaseAmount = 0.1f;
    [SerializeField]
    private float slowDownLevel = 0.7f;
    [SerializeField]
    private float slowDownMultiplier = 2.2f;

    private bool _isSlowedDown = false;
    private TerrainTileSpawner _terrainTileSpawner;

    public void IncreaseHeat()
    {
        IncreaseHeat(heatIncreaseAmount);
    }

    public void DecreaseHeat()
    {
       DecreaseHeat(heatDecreaseAmount);
    }

    public void IncreaseHeat(float amount)
    {
        if(amount < 0 || amount > 1)
            return;

        Overheat += amount;

        if(Overheat > 1)
            Overheat = 1;

        CustomLog.Log(CustomLog.CustomLogType.PLAYER, "Overheat: " + Overheat);

        if(Overheat >= slowDownLevel && (!_isSlowedDown))
        {
            CustomLog.Log(CustomLog.CustomLogType.PLAYER, "Slow Down Triggered ");
            _terrainTileSpawner.SpeedDown(TerrainTileSpawner.DEFAULT_SPEEDUP_MULTIPLIER);
            _terrainTileSpawner.SpeedUp(slowDownMultiplier);
            _isSlowedDown = true;
            SetSmokeEffectsActive(true);
        }

        overheatMask.transform.localPosition = new Vector3(overheatMask.transform.localPosition.x, -(1.0f - Overheat), 0.0f);

        
        if(Overheat >= 1 && playerJump.IsGrounded)
        {
            // GameOver - Animation here?
            GameController.Instance.GameOver();
            CustomLog.Log(CustomLog.CustomLogType.GAMEPLAY, "GAME OVER for Overheating");
            Destroy(gameObject);
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


        if (Overheat < slowDownLevel && _isSlowedDown)
        {
            CustomLog.Log(CustomLog.CustomLogType.PLAYER, "Speed to normal ");
            _terrainTileSpawner.SpeedDown(slowDownMultiplier);
            _terrainTileSpawner.SpeedUp(TerrainTileSpawner.DEFAULT_SPEEDUP_MULTIPLIER);
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
