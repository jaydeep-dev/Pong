using UnityEngine;
using UnityEngine.InputSystem;

public class PaddleController : MonoBehaviour
{
    [SerializeField] protected bool isHorizontalMovement;

    [SerializeField] private InputAction moveAction;

    public int UserId { get; private set; }

    [SerializeField] protected float moveLimit;
    [SerializeField] private float moveSpeed;

    protected bool isFrozen;
    private float defaultSpeed = 10;

    protected virtual void Awake()
    {
        GameManager.RegisterPaddle(this);
        defaultSpeed = moveSpeed;
        moveAction.Enable();
    }

    private void Update()
    {
        if (isFrozen || GetComponent<AIPaddleController>()) return;

        HandleInput();

        //Debug.Log((moveSpeed * Time.deltaTime * Vector3.up).magnitude);
    }

    private void HandleInput()
    {
        var moveVector = moveAction.ReadValue<float>();
        if (isHorizontalMovement)
        {
            transform.Translate(moveSpeed * Time.deltaTime * moveVector * Vector3.right);
            transform.position = new Vector2(Mathf.Clamp(transform.position.x, -moveLimit, moveLimit), transform.position.y);
        }
        else
        {
            transform.Translate(moveSpeed * Time.deltaTime * moveVector * Vector3.up);
            transform.position = new Vector2(transform.position.x, Mathf.Clamp(transform.position.y, -moveLimit, moveLimit));
        }
    }

    GameObject ghost;
    public void ResizePaddle(float duration = 10f)
    {
        CancelInvoke(nameof(ResetPaddleSize));
        Invoke(nameof(ResetPaddleSize), duration);

        // Make empty object and disable and enable it as this will be more effective
        var paddleGFX = transform.GetChild(0);
        if (ghost == null)
            ghost = Instantiate(paddleGFX.gameObject, transform);

        ghost.SetActive(true);
        float spacing = GameManager.Is2PGame ? 5f : 7f;
        if (isHorizontalMovement)
        {
            paddleGFX.transform.position = new Vector2(transform.position.x + spacing, transform.position.y);
            ghost.transform.position = new Vector2(transform.position.x - spacing, transform.position.y);
        }
        else
        {
            paddleGFX.transform.position = new Vector2(transform.position.x, transform.position.y + spacing);
            ghost.transform.position = new Vector2(transform.position.x, transform.position.y - spacing);
        }
    }

    private void ResetPaddleSize()
    {
        if (ghost == null)
            return;

        ghost.SetActive(false);
        transform.GetChild(0).transform.localPosition = Vector2.zero;
    }

    public void SpeedUp(float duration = 10f)
    {
        CancelInvoke(nameof(ResetSpeed));
        Invoke(nameof(ResetSpeed), duration);

        moveSpeed *= 4;

        foreach (var paddleGFX in GetComponentsInChildren<SpriteRenderer>())
        {
            paddleGFX.color = Color.red + Color.yellow;
        }
    }

    private void ResetSpeed()
    {
        moveSpeed = defaultSpeed;
        foreach (var paddleGFX in GetComponentsInChildren<SpriteRenderer>())
        {
            paddleGFX.color = Color.white;
        }
    }

    public void FreezePaddle(float duration = 3f)
    {
        CancelInvoke(nameof(UnfreezePaddle));
        Invoke(nameof(UnfreezePaddle), duration);

        isFrozen = true;
        Debug.Log(name + " Is Frozen");

        foreach (var paddleGFX in GetComponentsInChildren<SpriteRenderer>())
        {
            paddleGFX.color = Color.cyan;
        }
    }

    private void UnfreezePaddle()
    {
        isFrozen = false;
        foreach (var paddleGFX in GetComponentsInChildren<SpriteRenderer>())
        {
            paddleGFX.color = Color.white;
        }
    }
}
