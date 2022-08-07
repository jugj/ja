using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 12f;
    public bool isFacingRight = true;

    public AudioClip[] footsounds;
    public AudioSource sound;

    public float Health = 3f;

    private bool isJumping;
    private bool doubleJump;

    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    public AudioSource JumpSound;

    public float knockBack = 6;
    public AudioSource DamageSound;

    public UnityEngine.UI.Text TextHealth;


    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator animator;

    public ParticleSystem Dust;

    private void Update()
    {
        if (Health < 1)
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            animator.SetBool("Death", true);
        }

        if (transform.position.y < -10)
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            animator.SetBool("Death", true);
        }

            horizontal = Input.GetAxisRaw("Horizontal2");

        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
            animator.SetBool("Jump", false);
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
            animator.SetBool("Jump", true);
        }

        if (Input.GetButtonDown("Jump2"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f && !isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);

            jumpBufferCounter = 0f;

            StartCoroutine(JumpCooldown());
        }

        if (Input.GetButtonDown("Jump2") && IsGrounded())
        {
            JumpSound.Play();
        }

        if (Input.GetButtonDown("Jump2") && doubleJump)
        {
            JumpSound.Play();
        }

        if (Input.GetButtonUp("Jump2") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);

            coyoteTimeCounter = 0f;
        }

        if (IsGrounded() && !Input.GetButton("Jump2"))
        {
            doubleJump = false;
        }

        if (Input.GetButtonDown("Jump2"))
        {
            if (IsGrounded() || doubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);

                doubleJump = !doubleJump;
            }
        }

        if (Input.GetButtonUp("Jump2") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            CreateDust();
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }

        animator.SetFloat("Speed", Mathf.Abs(horizontal));
    }

    private IEnumerator JumpCooldown()
    {
        isJumping = true;
        yield return new WaitForSeconds(0.4f);
        isJumping = false;
    }

    void CreateDust()
    {
        Dust.Play();
    }

    public void footsteps(int _num)
    {
        sound.clip = footsounds[_num];
        sound.Play();
    }

    public void TakeDamage()
    {
        Debug.Log("TakeDamage");
        Health -= 1;
        TextHealth.text = Health.ToString() + ("x");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log($"BoxCollider {collision.gameObject.name}");
            
            
                DamageSound.Play();
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * knockBack, ForceMode2D.Impulse);
                TakeDamage();
            

        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("BlueHealth"))
        {
            Health++;
        }
    }

    void Start()
    {
        animator.SetBool("Death", false);
    }

    public void Death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}