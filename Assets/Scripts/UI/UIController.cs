using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : Singleton<UIController>
{
    [Header("References")]
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI trashCountText;
    [SerializeField]
    private GameMenuPanel gameMenuPanel;

    public void UpdateScore(int value)
    {
        if(value < 0)
            return;
        scoreText.text = value.ToString("0000000");
    }

    public void UpdateTrashCount(int value)
    {
        if (value < 0)
            return;
        trashCountText.text = value.ToString("00000");
    }

    public void SetOpenGameMenuPanel(bool open)
    {
        if(open)
            gameMenuPanel.Open();
        else
            gameMenuPanel.Close();
    }
}
