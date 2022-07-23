using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : Singleton<TutorialController>
{
    [Header("References")]
    [SerializeField]
    private TutorialSpawner tutorialSpawner;
    [SerializeField]
    private TerrainTileSpawner tileSpawner;

    
}
