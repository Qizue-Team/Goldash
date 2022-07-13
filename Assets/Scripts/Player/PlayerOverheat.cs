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

    [Header("Settings")]
    [SerializeField]
    private float heatIncreaseAmount = 0.1f;
    [SerializeField]
    private float heatDecreaseAmount = 0.1f;

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

        overheatMask.transform.localPosition = new Vector3(overheatMask.transform.localPosition.x, -(1.0f - Overheat), 0.0f);
    }

    public void DecreaseHeat(float amount)
    {
        if (amount < 0 || amount > 1)
            return;

        Overheat -= amount;

        if (Overheat < 0)
            Overheat = 0;

        CustomLog.Log(CustomLog.CustomLogType.PLAYER, "Overheat: " + Overheat);

        overheatMask.transform.localPosition = new Vector3(overheatMask.transform.localPosition.x, -(1.0f-Overheat), 0.0f);
    }

    private void Start()
    {
        Overheat = 0.0f;
    }
}
