using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawnable : MonoBehaviour
{
    public float Weight { get => weight; }
    public int ScorePoints { get => scorePoints; }

    [Header("Spawnable Settings")]
    [SerializeField]
    protected float weight;
    [SerializeField]
    protected int scorePoints;

}
