using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementListPanel : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private GameObject triangleUp;
    [SerializeField]
    private GameObject triangleDown;
    [SerializeField]
    private RectTransform content;

    private void Start()
    {
        triangleUp.SetActive(false);
        triangleDown.SetActive(false);
    }

    private void Update()
    {
        if (content.localPosition.y <= 0)
            triangleUp.SetActive(false);
        else
            triangleUp.SetActive(true);

        if(content.localPosition.y >= (content.rect.height - 950.0f))
            triangleDown.SetActive(false);
        else
            triangleDown.SetActive(true);
    }
}
