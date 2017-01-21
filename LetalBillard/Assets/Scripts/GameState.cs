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
        _player1.GetComponent<PlayerController>().playerIndex = 1;
        _player2 = Instantiate(_prefabPlayer2, _spawnPlayer2[randomPlayer2].transform.position, _spawnPlayer2[randomPlayer2].transform.rotation * Quaternion.Euler(0, 0, 64)) as GameObject;
        _player2.GetComponent<PlayerController>().playerIndex = 2;
    }

    public void respawn(GameObject player)
    {
        if(player.GetComponent<PlayerController>().playerIndex == 1)
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
        Debug.Log("StartGame");

        _curState = State.StartGame;

        yield return new WaitForSeconds(3);

        StartCoroutine(StartRound());
    }

    private IEnumerator StartRound()
    {
        Debug.Log("StartRound");
        InitPosPlayer();

        _curState = State.StartRound;

        yield return new WaitForSeconds(3);
        RoundInProgress();
    }

    private void RoundInProgress()
    {
        Debug.Log("RoundInProgress");

        _curState = State.RoundInProgress;
    }

    public IEnumerator StopRound(int indexPlayer)
    {
        Debug.Log("EndRound");

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
        Debug.Log("EndGame");

        _curState = State.EndRound;
    }
}
