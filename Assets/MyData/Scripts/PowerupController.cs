using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour
{
    [SerializeField] private PowerupType powerupType;

    private Transform spawnPos;

    public Transform GetSpawnPos() => spawnPos;

    public void SetSpawnPos(Transform pos) => spawnPos = pos;

    public PowerupType GetPowerupType() => powerupType;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out BallController ball))
        {
            PowerupManager.Instance.ApplyPowerupToPlayers(ball.LastPaddleTouched, this);
            Destroy(gameObject);
        }
    }
}

public enum PowerupType
{
    FastMove,
    Freeze,
    Resize,
}
