using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float movePower;
    [SerializeField]
    private float jumpPower;

    // 레이어 마스크(충돌 제어용)
    [SerializeField]
    private LayerMask groundMask;

    private new Rigidbody2D rigidbody;
    private Animator animator;
    private new SpriteRenderer renderer;
    private Vector2 inputDir;
    private bool isGrounded;
    private bool isHited;

    private Coroutine moveRoutine;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        moveRoutine = StartCoroutine(MoveRoutine());
    }

    private void FixedUpdate()
    {
        GroundCheck();
    }

    private void OnMove(InputValue value)
    {
        inputDir = value.Get<Vector2>();
    }

    private void OnJump(InputValue value)
    {
        if (!value.isPressed)
            return;
        if (!isGrounded)
            return;
        if (isHited)
            return;

        Jump();
    }

    private void Jump()
    {
        rigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }

    private void GroundCheck()
    {
        // 레이케스트 올은 부딧쳐도 모든 오브젝트 확인

        // 레이를 그려 땅바닥으로 닿았는지 확인

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.5f, groundMask);
        // 부딧혔다
        if (hit.collider != null)
        {
            isGrounded = true;
            animator.SetBool("IsGrounded", true);
            Debug.DrawRay(transform.position, new Vector3(hit.point.x, hit.point.y, 0) - transform.position, Color.red);

            // Smooth landing
            if (rigidbody.velocity.y < -3)
            {
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, -3);
            }
        }
        else
        {
            isGrounded = false;
            animator.SetBool("IsGrounded", false);
            Debug.DrawRay(transform.position, Vector3.down * 1.5f, Color.green);
        }
    }

    private IEnumerator MoveRoutine()
    {
        while (true)
        {
            if (inputDir.x < 0 && rigidbody.velocity.x > -maxSpeed)
                rigidbody.AddForce(Vector2.right * inputDir.x * movePower);
            else if (inputDir.x > 0 && rigidbody.velocity.x < maxSpeed)
                rigidbody.AddForce(Vector2.right * inputDir.x * movePower);

            animator.SetFloat("MoveDirX", Mathf.Abs(inputDir.x));
            if (inputDir.x > 0)
                renderer.flipX = false;
            else if (inputDir.x < 0)
                renderer.flipX = true;

            yield return null;
        }
    }

    public void Hit()
    {
        StartCoroutine(HitRoutine());
    }

    private IEnumerator HitRoutine()
    {
        StopCoroutine(moveRoutine);
        animator.SetBool("IsHit", true);
        isHited = true;
        yield return new WaitForSeconds(1f);
        animator.SetBool("IsHit", false);
        isHited = false;
        moveRoutine = StartCoroutine(MoveRoutine());
    }
}