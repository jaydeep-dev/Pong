using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPaddleController : PaddleController
{
    private BallController ball;
    [SerializeField] private float speedMultiplier;

    protected override void Awake()
    {
        base.Awake();
        ball = FindObjectOfType<BallController>();
    }

    private void FixedUpdate()
    {
        if (isFrozen) return;

        float error = Random.Range(-1.0f, 1.0f);
        var targetPos = Vector2.zero;

        if (isHorizontalMovement)
            targetPos = new Vector2(ball.transform.position.x + error, transform.position.y);
        else
            targetPos = new Vector2(transform.position.x, ball.transform.position.y + error);

        if(isHorizontalMovement)
            targetPos.x = Mathf.Clamp(targetPos.x, -moveLimit, moveLimit);
        else
            targetPos.y = Mathf.Clamp(targetPos.y, -moveLimit, moveLimit);

        transform.position = Vector2.Lerp(transform.position, targetPos, Time.fixedDeltaTime * speedMultiplier);
    }
}
