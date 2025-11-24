using UnityEngine;
using StateMachine;

public class GameManager : GamePlayBehaviour
{
    public static System.Action<Vector2> OnSpawnPlayer;

    [SerializeField] GameObject _player;

    GameObject _currentPlayer;

    private void Start()
    {
        OnSpawnPlayer = SpawnPlayer;

        OnNextGameState(GamePlayStates.INITIALIZING);
    }

    private void Update()
    {
        StateBehaviour(GamePlayCurrentState);

        UpdateState();
    }

    void StateBehaviour(GamePlayStates state)
    {
        switch(state)
        {
            case GamePlayStates.INITIALIZING:
                {
                    CameraBehaviour.OnSearchingPlayer?.Invoke();

                    break;
                }
            case GamePlayStates.START:
                {

                    OnNextGameState.Invoke(GamePlayStates.GAMEPLAY);

                    break;
                }
            case GamePlayStates.GAMEPLAY:
                {
                    Time.timeScale = 1;

                    PauseGame();

                    break;
                }
            case GamePlayStates.HISTORY:
                {
                    Time.timeScale = 0;


                    break;
                }
            case GamePlayStates.MECHANIC_APRESENTATION:
                {
                    Time.timeScale = 0;


                    break;
                }
            case GamePlayStates.SHOWINFO:
                {
                    Time.timeScale = 0;


                    break;
                }
            case GamePlayStates.PAUSE:
                {
                    Time.timeScale = 0;

                    PauseGame();

                    break;
                }
            case GamePlayStates.GAMEOVER:
                {
                    Time.timeScale = 0;

                    break;
                }
        }
    }

    void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GetCurrentGameState() != GamePlayStates.PAUSE)
            {
                OnNextGameState?.Invoke(GamePlayStates.PAUSE);
            }
            else
            {
                OnNextGameState?.Invoke(GamePlayStates.GAMEPLAY);
            }
        }
    }

    void SpawnPlayer(Vector2 pos)
    {
        Instantiate(_player, pos, Quaternion.identity);

        GameObject[] temp = GameObject.FindGameObjectsWithTag("Player");

        if (temp.Length > 0)
            for (int i = 1; i < temp.Length; i++)
            {
                Destroy(temp[i]);
            }

        temp[0].transform.position = pos;
    }
}
