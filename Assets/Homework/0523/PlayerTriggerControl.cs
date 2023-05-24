using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTriggerControl : MonoBehaviour
{
    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float movePower;
    [SerializeField]
    private float jumpPower;
    private Vector2 inputDir;
    private new Rigidbody2D rigidbody;
    private Animator animator;
    private new SpriteRenderer renderer;
    [SerializeField]
    private LayerMask groundMask;

    [SerializeField]
    private bool isCollision;




    private void Awake()
    {
        boxCollider = GetComponentInChildren<BoxCollider2D>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();

    }
    private void Update()
    {
        Move();
    }

    public void Move()
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
    }
    private void OnMove(InputValue value)
    {
        inputDir = value.Get<Vector2>();
    }
    public void Jump()
    {
        if (boxCollider.isTrigger)
        {
            rigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }
    private void OnJump(InputValue value)
    {
        Jump();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
