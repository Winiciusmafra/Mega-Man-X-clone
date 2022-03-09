using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameController _gameController;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    #region Unity methods 
    // Start is called before the first frame update
    void Start()
    {
        _gameController = FindObjectOfType(typeof(GameController))as GameController;
        _gameController._player = this;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameController.currentState != GameState.Gameplay)
        {
            return;
        }

    }

    #endregion

    public void spawnDone(){
        anim.SetTrigger("Spawn");
    }

    public void SetRay(){
        anim.SetTrigger("Ray");
    }
}
