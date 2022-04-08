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
    public Transform groundCheck;
    public LayerMask whatIsGround;
    private bool IsGround;

    [Header("Shot config")]
    public GameObject shotPrefab;
    public float speedShot;
    public Transform[] arm;
    public bool isChargeShot;
    private bool isShot;
    [Header("Charge Shot Config")]
    public Animator chargShotAnimator;
    public AudioSource chargAudioSource;
    public AudioClip chargA;
    public AudioClip chargB;


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
        anim.SetBool("isSpawn", true);
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
        if (Input.GetButtonDown("Jump") && IsGround == true)
        {
            rb.AddForce(new Vector2(0, jumpForce));
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Shot();
            isShot= true;
        }

        if (Input.GetButtonUp("Fire1"))
        {
            isShot = false;
            isChargeShot = false;
            chargShotAnimator.gameObject.SetActive(false);
           
        }
        rb.velocity = new Vector2(h * speed, rb.velocity.y);

        anim.SetInteger("idAnimation", idAnimation);
        anim.SetBool("isGrounded", IsGround);
        anim.SetFloat("SpeedY", rb.velocity.y);

    }

    private void FixedUpdate()
    {
        IsGround = Physics2D.OverlapCircle(groundCheck.position, 0.02f, whatIsGround);
    }

    #endregion

    public void Flip()
    {
        isLookLeft = !isLookLeft;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
    public void spawnStart()
    {
        anim.SetTrigger("Spawn");
    }
    private void SpawnDone()
    {
        anim.SetBool("isSpawn", false);
        rb.gravityScale = 1;
        _gameController.SetGameState(GameState.Gameplay);
    }

    public void SetRay()
    {
        anim.SetTrigger("Ray");
        
    }

    public void Shot(){
        float s = speedShot;
        int idArm=0;
        if (isLookLeft == true)
        {
            s = s * -1;
        }
        if (idAnimation == 1 && IsGround == true)
        {
            idArm = 1;
        }
        if (IsGround == false)
        {
            idArm = 2;
        }

    

        GameObject tempShot = Instantiate(shotPrefab, arm[idArm].position, transform.localRotation);
        tempShot.GetComponent<Rigidbody2D>().velocity = new Vector2(s,0);
        anim.SetLayerWeight(1,1);
        StopCoroutine("ShotCoroutine");
        StartCoroutine("ShotCoroutine");
    }

    IEnumerator ShotCoroutine(){
        yield return new WaitForSeconds(0.2f);
        if (isShot == false)
        {
            anim.SetLayerWeight(1,0);
        }
        else{
            if (isChargeShot == false)
            {
                isChargeShot = true;
                StartCoroutine("ChargeShot");
            }
            StartCoroutine("ShotCoroutine");
        }
        
    }

    IEnumerator ChargeShot()
    {
        chargShotAnimator.gameObject.SetActive(true);
        chargAudioSource.clip = chargA;
        chargAudioSource.Play();
        chargAudioSource.loop = false;
        yield return new WaitUntil(() => chargAudioSource.isPlaying == false);
        chargAudioSource.clip = chargB;
        chargAudioSource.Play();
        chargAudioSource.loop = true;
        chargShotAnimator.SetLayerWeight(1, 1);
    }
}
