using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBehaviour : MonoBehaviour
{
    new Collider2D collider;
    
    private void Awake() {
        collider = GetComponent<Collider2D>();
        if(collider == null) Debug.LogError("Collider is not set!");
    }

    private void OnCollisionEnter2D(Collision2D other) {
        GameStateController.instance.OnWallHit.Invoke();
    }
}
