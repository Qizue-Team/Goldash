using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : Spawnable
{
    public int Gear { get => gearDrop; }
    public string Name { get => trashName; }

    [SerializeField]
    private string trashName;
    [SerializeField]
    private int gearDrop;
}
