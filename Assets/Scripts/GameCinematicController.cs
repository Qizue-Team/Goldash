using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCinematicController : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private CameraFollowPoint cameraFollowPoint;
    [SerializeField]
    private PauseButton pauseButton;
    [SerializeField]
    private GameInfoPanel gameInfoPanel;

    private void Start()
    {
        InitialTransitionMenuToGame();
    }

    private void OnEnable()
    {
        CameraFollowPoint.onMoveFinished += ShowUI;
    }

    private void OnDisable()
    {
        CameraFollowPoint.onMoveFinished -= ShowUI;
    }

    private void InitialTransitionMenuToGame()
    {
        cameraFollowPoint.MoveToYValue(0.0f);
    }

    private void ShowUI()
    {
        pauseButton.Enter();
        gameInfoPanel.Enter();
    }
}
