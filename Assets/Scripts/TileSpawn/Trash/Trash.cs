using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public float Weight { get => weight; }
    public int ScorePoints { get => scorePoints; }

    [SerializeField]
    private float weight;
    [SerializeField]
    private int scorePoints;
}
