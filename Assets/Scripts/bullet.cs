using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
   // private Rigidbody2D MyRB;
    public float speed;
    public float hit = 1;
    // Start is called before the first frame update
    void Start()
    {
        //MyRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //MyRB.velocity = new Vector2(+speed * Time.deltaTime, 0);
       // MyRB.velocity = transform.right * speed * Time.deltaTime;
       // Destroy(gameObject, 5f);
        transform.position += transform.right * speed * Time.deltaTime; //transforma y varia la posicion el objeto
       
    }

    private void OnCollisionEnter2D (Collision2D collision) {
        var enemy = collision.collider.GetComponent<enemigo>(); // el script enemigo se enlaza con este

        if (enemy) {
            enemy.TakeHit(hit); //nivel de daño que le pongamos
            Destroy(gameObject);
        }

        if (collision.transform.tag =="ground" ) {
            Destroy(gameObject);
        }
        
    }
    
}
