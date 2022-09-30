using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadePanel : MonoBehaviour
{
    private void Start()
    {
        if(NavigationManager.PrevScene!=null && NavigationManager.PrevScene.Equals("Init"))
            this.gameObject.SetActive(true);
        else
            this.gameObject.SetActive(false);
    }
}
