using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(10f, 25f);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    AudioSource climbAudio;
    AudioSource footstepAudio;

    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    float gravityScaleAtStart;
    BoxCollider2D myFeetCollider;
    bool isAlive = true;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        gravityScaleAtStart = myRigidbody.gravityScale;
        myFeetCollider = GetComponent<BoxCollider2D>();
        AudioSource[] audios = GetComponents<AudioSource>();
        climbAudio = audios[0];
        footstepAudio = audios[1];

    }

    void Update()
    {
        if (!isAlive) { return; }
        Run();
        FlipSprite();
        ClimbLadder();
        PlayFootstepSound();
        Die();
    }
    void OnMove(InputValue value)
    {
        if (!isAlive) { return; }
        moveInput = value.Get<Vector2>();
        print(moveInput);
    }
    void OnJump(InputValue value)
    {
        if (!isAlive) { return; }
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }

        if (value.isPressed)
        {
            SoundManager.instance.PlaySound(SoundManager.instance.jumpSFX);
            myRigidbody.linearVelocity += new Vector2(0f, jumpSpeed);
        }
    }
    void Run()
    {

        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, myRigidbody.linearVelocity.y);
        myRigidbody.linearVelocity = playerVelocity;
        bool hasHorizontalSpeed = Mathf.Abs(myRigidbody.linearVelocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("IsRunning", hasHorizontalSpeed);
    }
    void FlipSprite()
    {
        bool hasHorizontalSpeed = Mathf.Abs(myRigidbody.linearVelocity.x) > Mathf.Epsilon;
        if (hasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.linearVelocity.x), 1f);
        }
    }

    //void ClimbLadder()
    //{
    //    if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
    //    {
    //        myRigidbody.gravityScale = gravityScaleAtStart;
    //        myAnimator.SetBool("IsClimbing", false);
    //        return;
    //    }
    //    myRigidbody.gravityScale = 0f;

    //    Vector2 climbVelocity = new Vector2(myRigidbody.linearVelocity.x, moveInput.y * climbSpeed);
    //    myRigidbody.linearVelocity = climbVelocity;
    //    bool hasVerticalSpeed = Mathf.Abs(myRigidbody.linearVelocity.y) > Mathf.Epsilon;
    //    myAnimator.SetBool("IsClimbing", hasVerticalSpeed);
    //}
    void ClimbLadder()
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myRigidbody.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool("IsClimbing", false);

            if (climbAudio.isPlaying)
            {
                climbAudio.Stop();
            }

            return;
        }

        myRigidbody.gravityScale = 0f;

        Vector2 climbVelocity = new Vector2(myRigidbody.linearVelocity.x, moveInput.y * climbSpeed);
        myRigidbody.linearVelocity = climbVelocity;

        bool hasVerticalSpeed = Mathf.Abs(myRigidbody.linearVelocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("IsClimbing", hasVerticalSpeed);

        if (hasVerticalSpeed)
        {
            if (!climbAudio.isPlaying)
            {
                climbAudio.Play();
            }
        }
        else
        {
            if (climbAudio.isPlaying)
            {
                climbAudio.Stop();
            }
        }
    }
    void OnAttack(InputValue value)
    {
        if (!isAlive) { return; }
        SoundManager.instance.PlaySound(SoundManager.instance.shootSFX);
        Instantiate(bullet, gun.position, transform.rotation);
    }

    void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards", "Water")))
        {
            isAlive = false;
            SoundManager.instance.PlaySound(SoundManager.instance.playerDeathSFX);
            myAnimator.SetTrigger("Dying");
            myRigidbody.linearVelocity = deathKick;
            StartCoroutine(HandleDeath());
        }
        System.Collections.IEnumerator HandleDeath()
        {
            yield return new WaitForSeconds(1f);
            FindAnyObjectByType<GameSession>().ProcessPlayerDeath();
        }
    }
    void PlayFootstepSound()
    {
        bool hasHorizontalSpeed = Mathf.Abs(myRigidbody.linearVelocity.x) > Mathf.Epsilon;
        bool playerHasFeetOnGround = myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));

        if (hasHorizontalSpeed && playerHasFeetOnGround && isAlive && !myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            if (!footstepAudio.isPlaying)
            {
                footstepAudio.Play();
            }
        }
        else
        {
            if (footstepAudio.isPlaying)
            {
                footstepAudio.Stop();
            }
        }
    }
}