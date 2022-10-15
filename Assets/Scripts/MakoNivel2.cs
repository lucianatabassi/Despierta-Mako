using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakoNivel2 : MonoBehaviour
{
    public float velCorrer;
    public float velSaltar;
    Rigidbody2D rb2D;
   
    public Animator anim;
    public bool isAttacking = false;
    public static MakoNivel2 scriptMako;


private void Awake() { // para acceder al script desde cualquier parte
        scriptMako = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("mouse 0")) { //dispara con el mouse
        gameObject.GetComponent <Animator>().SetBool("shoot", true);
        gameObject.GetComponent <Animator>().SetBool("fight", false);

        
    }

    // Ataque();
    }
}
