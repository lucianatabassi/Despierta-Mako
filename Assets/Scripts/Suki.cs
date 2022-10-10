using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suki : MonoBehaviour
{
    public Transform rayCast;
    public LayerMask raycastMask;
    public float rayCastLength;
    public float distAtaque; //dist min de ataque
    public float vel;
    public float timer; // tiempo entre ataques
    public float hit = 1;

    private RaycastHit2D golpe;
    private GameObject mako;
    private Animator ani;
    private float dist; // guardar dist entre mako y suki
    private bool sukiAtaque;
    private bool enRango; // chequear si mako esta dentro del rango de ataque
    private bool cooling; // chequear si suki se calmo
    private float intTimer;


    void Awake () {

        intTimer = timer;
        ani = GetComponent<Animator>();
    }

    void Update()
    {
        if (enRango) {
            golpe = Physics2D.Raycast(rayCast.position, Vector2.left, rayCastLength, raycastMask);
            RaycastDebugger();
        }


        //cuando mako es detectada
        if (golpe.collider !=null) {
            sukiLogic();
        } else if (golpe.collider == null) {
            enRango = false;
        }

        if (enRango == false) {
            StopAttack();
        }
    }


    void sukiLogic() {
        dist = Vector2.Distance (transform.position, mako.transform.position); // calcula dist entre mako y suki

        if (dist > distAtaque) {
            Move();
            StopAttack();
        } 
        else if (distAtaque >= dist && cooling == false) {
            Ataque();
        }

        if (cooling) {
            Cooldown();
            ani.SetBool ("sukiAttack", false);
        }
    }

    void OnTriggerEnter2D(Collider2D trig) {

        var player = trig.GetComponent<PlayerControler>();

        if (trig.gameObject.tag == "Player") {
            mako = trig.gameObject; // guarda la pos de mako
            enRango = true;
        }

        

       if (player) {
            player.golpeSuki (hit);
           
            
        }
    }

    void Move () {
        if (!ani.GetCurrentAnimatorStateInfo(0).IsName ("suki-peleando_2")) {
            Vector2 makoPosition = new Vector2 (mako.transform.position.x, transform.position.y );

            transform.position = Vector2.MoveTowards(transform.position, makoPosition, vel * Time.deltaTime);
        }
    }

    void Ataque () {
        timer = intTimer; // resetea el tiempo cuando mako entra en rango
        sukiAtaque = true;

        ani.SetBool ("sukiAttack", true);
    }


    void Cooldown() {
        timer -= Time.deltaTime;

        if (timer <= 0 && cooling && sukiAtaque ) {
            cooling = false;
            timer = intTimer;

        }
    }
    void StopAttack() {
        cooling = false;
        sukiAtaque = false;
        ani.SetBool("sukiAttack", false);
    }

    void RaycastDebugger() {
        if (dist > distAtaque) {
            Debug.DrawRay(rayCast.position, Vector2.left * rayCastLength, Color.red);
        }
        else if (distAtaque > dist) {
            Debug.DrawRay(rayCast.position, Vector2.left * rayCastLength, Color.green);
        }
    }

    public void TriggerCooling () {
        cooling = true;
    }

 
}
