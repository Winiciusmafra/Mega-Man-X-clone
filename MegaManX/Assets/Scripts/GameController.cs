using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Ready,
    Spawn,
    Gameplay
}
public class GameController : MonoBehaviour
{

    [HideInInspector] public PlayerController _player;
    public GameState currentState;

    [Header("Cut Initial")]
    public GameObject ready;
    public Transform spawnPoint;
    public float offSetYSpawn;
    public float speedRaySpawn;

    #region Unity methods 
    // Start is called before the first frame update
    void Start()
    {
        ready.SetActive(false);
        StartCoroutine("ReadyText");
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case GameState.Spawn:

                _player.transform.position = Vector2.MoveTowards(_player.transform.position, spawnPoint.position, speedRaySpawn * Time.deltaTime);
                //MoveTowards = move A to B
                if (_player.transform.position == spawnPoint.position)
                {
                    SetGameState(GameState.Gameplay);
                    _player.spawnStart();
                }
                break;
        }



    }

    #endregion

    #region My methods

    public void SetGameState(GameState newState)
    {
        currentState = newState;

        switch (currentState)
        {
            case GameState.Spawn:
                SetPlayerSpawnPosition();
                break;

            case GameState.Gameplay:

                break;
        }
    }

    public void SetPlayerSpawnPosition()
    {
        _player.transform.position = new Vector3(spawnPoint.position.x, spawnPoint.position.y + offSetYSpawn, 0);
    }
     IEnumerator ReadyText()
    {
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i <= 4; i++)
        {
            ready.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            ready.SetActive(false);
            yield return new WaitForSeconds(0.1f);
        }
        ready.SetActive(false);
        SetGameState(GameState.Spawn);
    }

    #endregion
}
