using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehaviour : MonoBehaviour
{
    private void FixedUpdate() {
        Vector3 targetPosition = transform.position;
        targetPosition.y = GameStateController.instance.TouchWorldPosition.y * 8f;
        targetPosition.y = targetPosition.y - 4f;
        transform.SetPositionAndRotation(targetPosition, Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        GameStateController.instance.OnRocketHit.Invoke();
    }
}
