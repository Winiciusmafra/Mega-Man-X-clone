using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public enum GameState{
        Ready,
        Spawn,
        Gameplay
    }
public class GameController : MonoBehaviour
{
   
    public GameState currentState;

    #region Unity methods 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion

    #region My methods

    public void SetGameState(GameState newState){
        this.currentState = newState;
   }

    #endregion
}
