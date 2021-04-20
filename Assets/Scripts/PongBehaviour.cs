using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongBehaviour : MonoBehaviour
{
    new Rigidbody2D rigidbody;
    [SerializeField] SpriteRenderer sr;
    Vector2 initForce;

    private void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();

        Color c;
        ColorUtility.TryParseHtmlString("#"+GameManager.colors[GameManager.GetPrefInt("color")], out c);
        sr.color = c;
    }

    public void Respawn(){
        transform.position = Vector3.zero;

        float initSize = Random.Range(0.05f, 1.5f);
        gameObject.transform.localScale = new Vector3(initSize, initSize, 1);

        float rangex = Random.Range(0.1f, 1);
        rangex = Random.Range(0, 2) == 1 ? -rangex : rangex;
        
        float rangey = Random.Range(0.1f, 1);
        rangey = Random.Range(0, 2) == 1 ? -rangey : rangey;

        initForce = new Vector2(rangex, rangey).normalized;
    }

    public void Launch(){
        rigidbody.simulated = true;
        rigidbody.velocity = initForce * Random.Range(5f, 25f);
    }
    
    public void Stop(){
        rigidbody.velocity = Vector2.zero;
        rigidbody.simulated = false;
    }
}