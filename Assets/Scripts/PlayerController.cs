using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float jumpForce = 8;

    private bool isGrounded = true;
    public bool isFever = false;
    
    private Rigidbody2D player_rigidBody;
    private BoxCollider2D player_collider;
    private Animator player_animator;
    private SpriteRenderer player_spriteRenderer;

    void OnEnable()
    {
        player_rigidBody = GetComponent<Rigidbody2D>();
        player_collider = GetComponent<BoxCollider2D>();
        player_animator = GetComponent<Animator>();
        player_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        if (Input.GetMouseButtonDown(0) && isGrounded)
        {
            player_rigidBody.AddForceY(jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            player_animator.SetBool("isGrounded", isGrounded);
        }
    }

    // ContactFilter2D - 바닥 닿은 여부 확인하는 콜라이더 함수
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            isGrounded = true;
            player_animator.SetBool("isGrounded", isGrounded);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (!isFever)
                Destroy(collision.gameObject);

            Hit();
        }
        else if (collision.gameObject.tag == "Food")
        {
            Destroy(collision.gameObject);
            Heal();
        }
        else if (collision.gameObject.tag == "GoldenBaechu")
        {
            Destroy(collision.gameObject);
            StartFever();
        }
    }

    private void StartFever()
    {
        isFever = true;
        player_spriteRenderer.color = Color.yellow;
        Invoke("StopFever", 5f);
    }

    private void StopFever()
    {
        isFever = false;
        player_spriteRenderer.color = Color.white;
    }

    private void Heal()
    {
        GameManager.Instance.hp = Mathf.Min(3, GameManager.Instance.hp + 1);
    }

    private void Hit()
    {
        if (!isFever)
        {
            GameManager.Instance.hp -= 1;
        }

    }

    public void Died()
    {
        player_collider.enabled = false;
        player_animator.enabled = false;
        player_rigidBody.AddForceY(jumpForce, ForceMode2D.Impulse);
    }
}
