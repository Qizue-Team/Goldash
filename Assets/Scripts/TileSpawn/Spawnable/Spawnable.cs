using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawnable : MonoBehaviour
{
    public float Weight { get => weight; }
    public int ScorePoints { get => scorePoints; }
    public float AppearDistance { get => appearDistance; }

    [Header("Spawnable Settings")]
    [SerializeField]
    protected float appearDistance;
    [SerializeField]
    protected float weight;
    [SerializeField]
    protected int scorePoints;

}
