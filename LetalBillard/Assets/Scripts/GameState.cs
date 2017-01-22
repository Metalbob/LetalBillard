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
        Pause,
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

    [SerializeField]
    private GameObject[] LDs;
    private int previousLD = -1;

    public State CurState { get { return _curState; } set { _curState = value; } }
    private State _curState = State.None;

    [SerializeField]
    private int _scoreGoal = 3;

    private int _nbrRound = 0;
    private int _scoreP1 = 0;
    private int _scoreP2 = 0;

    private bool _isPause = false;


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

    private void Update()
    {
        if (_curState == State.RoundInProgress || _curState == State.Pause)
        {
            if (Input.GetButtonDown("StartAll"))
            {
                PauseGame();
            }
        }
    }

    private IEnumerator StartGame()
    {

        previousLD = Random.Range(0, LDs.Length);
        LDs[previousLD].SetActive(true);
        _curState = State.StartGame;
        PanelState.Instance.StartGamePanel();
        
        yield return new WaitForSeconds(3);

        StartCoroutine(StartRound());
    }

    private IEnumerator StartRound()
    {
        InitPosPlayer();
        KillCam.Reset();
        _nbrRound++;

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

            if (indexPlayer == 1)
            {
                _scoreP2++;
            }
            else if (indexPlayer == 2)
            {
                _scoreP1++;
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
                LDs[previousLD].SetActive(false);
                previousLD = Random.Range(0, LDs.Length);
                LDs[previousLD].SetActive(true);
                StartCoroutine(StartRound());
            }
        }
    }

    public void PauseGame()
    {
        if (!_isPause)
        {
            _curState = State.Pause;
            PanelState.Instance.PausePanel();
            StopAllCoroutines();
            _isPause = true;
            Time.timeScale = 0;
        }
        else
        {
            RoundInProgress();
            _isPause = false;
            PanelState.Instance.EndRoundPanel();
            Time.timeScale = 1;
        }
    }

    public void DestroyAllPlayers()
    {

        Destroy(_player1);
        Destroy(_player2);

        GameObject[] dest = GameObject.FindGameObjectsWithTag("ToDestroy");
        foreach (var des in dest)
            Destroy(des);
    }

    public void EndGame()
    {

        _curState = State.EndGame;
        PanelState.Instance.EndRoundPanel();
    }
}
