﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    #region Instance
    static GameState instance;

    public static GameState Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(GameState)) as GameState;
            }

            return instance;
        }
    }

    #endregion

    public enum State
    {
        None = 0,
        StartGame,
        StartRound,
        RoundInProgress,
        EndRound,
        EndGame,
    }

    [SerializeField]
    private GameObject _prefabPlayer1 = null;
    [SerializeField]
    private GameObject _prefabPlayer2 = null;

    [SerializeField]
    private GameObject[] _spawnPlayer1;
    [SerializeField]
    private GameObject[] _spawnPlayer2;

    public State CurState { get { return _curState; } set { _curState = value; } }
    private State _curState = State.None;

    [SerializeField]
    private int _scoreGoal = 3;

    private int _nbrRound = 0;
    private int _scoreP1 = 0;
    private int _scoreP2 = 0;


    private GameObject _player1 = null;
    private GameObject _player2 = null;

    public GameObject Player1 { get { return _player1; } }
    public GameObject Player2 { get { return _player2; } }

    private void InitPosPlayer()
    {
        int randomPlayer1 = (int)Mathf.Floor(Random.value * _spawnPlayer1.Length);
        int randomPlayer2 = (int)Mathf.Floor(Random.value * _spawnPlayer2.Length);
        _player1 = Instantiate(_prefabPlayer1, _spawnPlayer1[randomPlayer1].transform.position, _spawnPlayer1[randomPlayer1].transform.rotation * Quaternion.Euler(0, 0, 64)) as GameObject;
        _player1.GetComponent<PlayerInput>().playerIndex = 1;
        _player2 = Instantiate(_prefabPlayer2, _spawnPlayer2[randomPlayer2].transform.position, _spawnPlayer2[randomPlayer2].transform.rotation * Quaternion.Euler(0, 0, 64)) as GameObject;
        _player2.GetComponent<PlayerInput>().playerIndex = 2;
    }

  

    public void respawn(GameObject player)
    {
        if(player.GetComponent<PlayerInput>().playerIndex == 1)
        {
            int randomPlayer1 = (int)Mathf.Floor(Random.value * _spawnPlayer1.Length);
            player.transform.position = _spawnPlayer1[randomPlayer1].transform.position;
        }
        else
        {
            int randomPlayer2 = (int)Mathf.Floor(Random.value * _spawnPlayer2.Length);
            player.transform.position = _spawnPlayer2[randomPlayer2].transform.position;
        }
    }

    private void Start()
    {
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {

        _curState = State.StartGame;
        PanelState.Instance.StartGamePanel();

        yield return new WaitForSeconds(3);

        StartCoroutine(StartRound());
    }

    private IEnumerator StartRound()
    {
        InitPosPlayer();

        _curState = State.StartRound;
        PanelState.Instance.StartRoundPanel();

        yield return new WaitForSeconds(3);
        RoundInProgress();
    }

    private void RoundInProgress()
    {

        _curState = State.RoundInProgress;
        PanelState.Instance.RoundInProgressPanel();
    }

    public IEnumerator StopRound(int indexPlayer)
    {

        if (_curState == State.RoundInProgress)
        {
            _player1.GetComponent<PlayerController>().StopVelocityPlayer();
            _player2.GetComponent<PlayerController>().StopVelocityPlayer();

            if (indexPlayer == 0)
            {
                _scoreP1++;
            }
            else
            {
                _scoreP2++;
            }

            _curState = State.EndRound;
            PanelState.Instance.EndRoundPanel();

            yield return new WaitForSeconds(2);

            DestroyAllPlayers();

            if (_scoreP1 >= _scoreGoal || _scoreP2 >= _scoreGoal)
            {
                EndGame();
            }
            else
            {
                StartCoroutine(StartRound());
            }
        }
    }

    public void DestroyAllPlayers()
    {
        Destroy(_player1);
        Destroy(_player2);
    }

    public void EndGame()
    {

        _curState = State.EndGame;
        PanelState.Instance.EndRoundPanel();
    }
}
