using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelState : MonoBehaviour
{
    #region Instance
    static PanelState instance;

    public static PanelState Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(PanelState)) as PanelState;
            }

            return instance;
        }
    }

    #endregion

    [SerializeField]
    private GameObject StartGame;
    [SerializeField]
    private GameObject StartRound;
    [SerializeField]
    private GameObject RoundInProgress;
    [SerializeField]
    private GameObject Pause;
    [SerializeField]
    private GameObject EndRound;
    [SerializeField]
    private GameObject EndGame;

    public void StartGamePanel()
    {
        StartGame.SetActive(true);
        StartRound.SetActive(false);
        RoundInProgress.SetActive(false);
        Pause.SetActive(false);
        EndRound.SetActive(false);
        EndGame.SetActive(false);
    }

    public void StartRoundPanel()
    {
        StartGame.SetActive(false);
        StartRound.SetActive(true);
        RoundInProgress.SetActive(false);
        Pause.SetActive(false);
        EndRound.SetActive(false);
        EndGame.SetActive(false);
    }

    public void RoundInProgressPanel()
    {
        StartGame.SetActive(false);
        StartRound.SetActive(false);
        RoundInProgress.SetActive(true);
        Pause.SetActive(false);
        EndRound.SetActive(false);
        EndGame.SetActive(false);
    }

    public void EndRoundPanel()
    {
        StartGame.SetActive(false);
        StartRound.SetActive(false);
        RoundInProgress.SetActive(false);
        Pause.SetActive(false);
        EndRound.SetActive(true);
        EndGame.SetActive(false);
    }

    public void EndGamePanel()
    {
        StartGame.SetActive(false);
        StartRound.SetActive(false);
        RoundInProgress.SetActive(false);
        Pause.SetActive(false);
        EndRound.SetActive(false);
        EndGame.SetActive(true);
    }

    public void PausePanel()
    {
        StartGame.SetActive(false);
        StartRound.SetActive(false);
        RoundInProgress.SetActive(false);
        Pause.SetActive(true);
        EndRound.SetActive(false);
        EndGame.SetActive(false);
    }

}
