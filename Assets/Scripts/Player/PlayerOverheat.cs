using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOverheat : MonoBehaviour
{
    public float Overheat { get; private set; }

    [SerializeField]
    private GameObject overheatMask;

    public void IncreaseHeat(float amount)
    {
        if(amount < 0 || amount > 1)
            return;

        Overheat += amount;

        if(Overheat > 1)
            Overheat = 1;

        overheatMask.transform.localPosition = new Vector3(overheatMask.transform.localPosition.x, -Overheat, 0.0f);
    }

    public void DecreaseHeat(float amount)
    {
        if (amount < 0 || amount > 1)
            return;

        Overheat -= amount;

        if (Overheat < 0)
            Overheat = 0;

        overheatMask.transform.localPosition = new Vector3(overheatMask.transform.localPosition.x, -(1.0f-Overheat), 0.0f);
    }

    private void Start()
    {
        Overheat = 0.0f;
    }
}
