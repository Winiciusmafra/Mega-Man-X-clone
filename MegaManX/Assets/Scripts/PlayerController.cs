using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameController _gameController;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    [Header("Player config")]
    public float speed;
    public float jumpForce;
    private int idAnimation;
    public bool isLookLeft;

    #region Unity methods 
    // Start is called before the first frame update
    void Start()
    {
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
        _gameController._player = this;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        rb.gravityScale = 0;
        _gameController.SetPlayerSpawnPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameController.currentState != GameState.Gameplay)
        {
            return;
        }

        float h = Input.GetAxisRaw("Horizontal");

        if (h > 0 && isLookLeft == true)
        {
            Flip();
        }
        else if (h < 0 && isLookLeft == false)
        {
            Flip();
        }
        idAnimation = (h != 0) ? 1 : 0;
        // if (h!= 0)
        // {
        //     idAnimation = 1;
        // }else{
        //     idAnimation = 0;
        // }

        rb.velocity = new Vector2(h * speed, rb.velocity.y);

        anim.SetInteger("idAnimation", idAnimation);

    }

    #endregion

    public void Flip()
    {
        isLookLeft = !isLookLeft;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
    public void spawnDone()
    {
        anim.SetTrigger("Spawn");
    }

    public void SetRay()
    {
        anim.SetTrigger("Ray");
    }
}
