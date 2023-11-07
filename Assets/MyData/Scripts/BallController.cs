using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BallController : MonoBehaviour
{
    [Header("Ball Settings")]
    [SerializeField] private float ballForce = 10f;
    [SerializeField] private bool useRandomColor = false;

    [field: SerializeField] public PaddleController LastPaddleTouched { get; private set; }

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Vector2 lastVelocity;

    private bool applySpeedCheck;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        Invoke(nameof(ThrowBall), GameSetup.CountDown);
    }

    private void Update()
    {
        lastVelocity = rb.velocity;
    }

    private void FixedUpdate()
    {
        if (applySpeedCheck)
        {
            if (lastVelocity == Vector2.zero)
            {
                ThrowBall();
            }
            rb.velocity = lastVelocity * ballForce;
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, ballForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (useRandomColor)
            spriteRenderer.color = Random.ColorHSV();

        var player = collision.transform.GetComponentInParent<PaddleController>();
        if (player != null)
        {
            LastPaddleTouched = player;
        }

        var reflectDir = Vector2.Reflect(lastVelocity, collision.contacts[0].normal).normalized;
        //Debug.Log(lastVelocity + "----" + collision.contacts[0].normal + "----" + reflectDir);
        rb.velocity = Vector3.zero;

        if (collision.transform.TryGetComponent(out BorderController borderController))
        {
            applySpeedCheck = false;
            borderController.BorderHit();
            Invoke(nameof(ResetBall), 2f);
        }
        else
        {
            rb.AddForce(reflectDir * ballForce, ForceMode2D.Impulse);
        }
    }

    private void ResetBall()
    {
        rb.position = Vector3.zero;
        LastPaddleTouched = null;
        Invoke(nameof(ThrowBall), 2f);
    }

    private void ThrowBall()
    {
        var xDirection = Random.Range(-1f, 1f);
        var yDirection = Random.Range(-1f, 1f);
        Vector2 force = new Vector2(xDirection , yDirection) * ballForce;
        rb.AddForce(force, ForceMode2D.Impulse);
        lastVelocity = rb.velocity;
        Debug.Log("Ball's Dir: " + force);

        applySpeedCheck = true; // Make sure this is the last line so after setting the velocity the fixed update starts velocity checks
    }
}
