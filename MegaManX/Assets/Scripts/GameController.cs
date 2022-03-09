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
    public Transform spawnPoint;
    public float offSetYSpawn;
    public float speedRaySpawn;

    #region Unity methods 
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case GameState.Spawn:
           
            _player.transform.position = Vector2.MoveTowards(_player.transform.position, spawnPoint.position, speedRaySpawn * Time.deltaTime);
                //MoveTowards = move A to B
                if(_player.transform.position == spawnPoint.position)
                {
                    SetGameState(GameState.Gameplay);
                    _player.spawnDone();
                }
            break;
        }

        if(Input.GetButtonDown("Jump")){
            SetGameState(GameState.Spawn);
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
                _player.transform.position = new Vector3(spawnPoint.position.x, spawnPoint.position.y + offSetYSpawn, 0);
                break;

            case GameState.Gameplay:

                break;
        }
    }

    #endregion
}
