using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moviment : MonoBehaviour
{

    [Header("Ground Properties")]
    public LayerMask groundLayer;
    public float groundDistance;
    public bool isGrounded;
    public Vector3[] footOffset;

    public float speed = 2f;
    public float jumpForce = 2f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private float xVelocity;

    private int direction = 1;
    private float originalXScale;

    RaycastHit2D leftCheck;
    RaycastHit2D rightCheck;

    // Start is called before the first frame update
    void Start()
    {
    rb = GetComponent<Rigidbody2D>();

    originalXScale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
    float horizontal = Input.GetAxisRaw("Horizontal");
    movement = new Vector2(horizontal, 0);

    if (xVelocity * direction < 0f)
        {
            Flip();
        }

        PhysicsCheck();
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

    }

    private void FixedUpdate()
    {
    xVelocity = movement.normalized.x * speed;
    rb.velocity = new Vector2(xVelocity, rb.velocity.y);
    }

    private void Flip()
    {
    direction *= -1;
    Vector3 scale = transform.localScale;
    scale.x = originalXScale * direction;
    transform.localScale = scale;

    }

    private void PhysicsCheck()
    {
    isGrounded = false;
    leftCheck = Raycast(new Vector2(footOffset[0].x, footOffset[0].y),
        Vector2.down, groundDistance, groundLayer);
    rightCheck = Raycast(new Vector2(footOffset[1].x, footOffset[1].y),
         Vector2.down, groundDistance, groundLayer);

    if (leftCheck || rightCheck)
    {
            isGrounded = true;
    }

    }

    private RaycastHit2D Raycast(Vector3 origin, Vector2 rayDirection, float lenght, LayerMask mask)
    {
    Vector3 pos = transform.position;

    RaycastHit2D hit = Physics2D.Raycast(pos + origin, rayDirection, lenght, mask);
    // Se quisermos mostrar o raycast em cena

        Color color = hit ? Color.red : Color.green;

        Debug.DrawRay(pos + origin, rayDirection * lenght, color);
    // Determina a cor baseado se o raycast se colidir ou não

    // retorna o resultado do hit
    return hit;
    }
}
