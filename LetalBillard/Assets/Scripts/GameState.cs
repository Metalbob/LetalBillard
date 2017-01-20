using System.Collections;
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
        LoopGame,
        EndGame,
        Restart
    }

    [SerializeField]
    private GameObject _prefabPlayer1 = null;
    [SerializeField]
    private GameObject _prefabPlayer2 = null;

    [SerializeField]
    private GameObject _spawnPlayer1 = null;
    [SerializeField]
    private GameObject _spawnPlayer2 = null;

    public State CurState { get { return _curState; } set { _curState = value; } }
    private State _curState = State.None;

    private GameObject _player1 = null;
    private GameObject _player2 = null;

    public GameObject Player1 { get { return _player1; } }
    public GameObject Player2 { get { return _player2; } }

    private void Awake()
    {
        InitPosPlayer();
    }

    private void InitPosPlayer()
    {
        _player1 = Instantiate(_prefabPlayer1, _spawnPlayer1.transform.position, _spawnPlayer1.transform.rotation) as GameObject;
        _player2 = Instantiate(_prefabPlayer2, _spawnPlayer2.transform.position, _spawnPlayer2.transform.rotation) as GameObject;
    }
}
