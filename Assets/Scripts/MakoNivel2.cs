using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MakoNivel2 : MonoBehaviour
{
    public float velCorrer;
    public float velSaltar;
    Rigidbody2D rb2D;
   
    public Animator anim;

    public float puntosVidaPlayer; 
    public float vidaMaxPlayer;
    public Image barraDeVida;

    [Header("Sonidos")]
    public GameObject SaltoSonido;
    public GameObject[] HeridaSonido;



    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
         puntosVidaPlayer = vidaMaxPlayer;
    }

    // Update is called once per frame
    void Update()
    {

        barraDeVida.fillAmount = puntosVidaPlayer / vidaMaxPlayer;


        if (Input.GetKey("a")) 
    {
        rb2D.velocity = new Vector2 (-velCorrer  , rb2D.velocity.y); // en que direccion ir (eje Y se queda como esta)
        anim.SetBool("correr", true);
        anim.SetBool("deschipear", false);
        anim.SetBool("saltar", false);
        transform.eulerAngles = new Vector3 (0,180, 0); // para voltear al personaje             
    } 

    if (Input.GetKey("a") && Input.GetKey("space") ) {
        anim.SetBool("saltar", true);
    }

    if (Input.GetKey("d")) 
    {
         rb2D.velocity = new Vector2 (velCorrer , rb2D.velocity.y); // en que direccion ir (eje y se queda como esta)
        anim.SetBool("correr", true);
        anim.SetBool("deschipear", false);
        anim.SetBool("saltar", false);
        transform.eulerAngles = new Vector3 (0,0, 0); // para voltear al personaje
         
       
        
    } 

    if (Input.GetKey("d") && Input.GetKey("space") ) {
        anim.SetBool("saltar", true);
    }

    if (Input.GetKey ("space") && checkGround.estaEnSuelo ) { 
        
           rb2D.velocity = new Vector2 (rb2D.velocity.x, velSaltar);    
            anim.SetBool("saltar", true);
            NuevoSonido(SaltoSonido, 1f);
    
    }

    if (Input.GetKeyDown ("space")  && tocaPlataforma.enPlat ) // si esta sobre una plataforma puede saltar 
    {
        rb2D.velocity = new Vector2 (rb2D.velocity.x, velSaltar);
        
        anim.SetBool("correr", false);
        anim.SetBool("saltar", true);
         NuevoSonido(SaltoSonido, 1f);
        
    }


    if (!Input.GetKey("a") && !Input.GetKey("d") && !Input.GetKeyDown("w") && !Input.GetKey ("space") ) { // esto es para que las animaciones no sigan funcionando cuando se dejan d presionar las teclas
        anim.SetBool("correr", false);
        anim.SetBool("saltar", false);
           
    }

        if (Input.GetKey("mouse 0")) { //dispara con el mouse
        anim.SetBool("deschipear", true);
        

        
    }

     
    }

    private void OnCollisionEnter2D (Collision2D collision) { // verificar q colisionamos con la plataforma cuando se mueve
        if (collision.gameObject.tag == "plataformaMovible") {
            transform.parent = collision.transform;
            

        }
     if (collision.gameObject.tag == "balaEnemigo" ) { // animacion de daño
        anim.SetTrigger ("herida");
        
    }


    }


     private void OnCollisionExit2D (Collision2D collision) { // q el personaje ya no se mueva con la plataforma cuando ya se bajo
        if (collision.gameObject.tag == "plataformaMovible") {
            transform.parent = null;

        }
    }


    public void TakeHit (float golpe) { // personaje pierde vida
        puntosVidaPlayer -= golpe;
       NuevoSonido(HeridaSonido[0], 1f);
        gameObject.GetComponent <Animator>().SetBool("herida", true);
         if (puntosVidaPlayer <=0) {
            Destroy(gameObject);
        }


    }

    void NuevoSonido (GameObject prefab, float duracion = 5f) {
         Destroy (Instantiate(prefab), duracion);
    }

     
}
