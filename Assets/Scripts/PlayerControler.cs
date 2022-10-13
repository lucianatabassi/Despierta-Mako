using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PlayerControler : MonoBehaviour
{

    public Transform PuntoDisparo;  // desde donde sale la bala
    public bullet Bullet; // img bala

    public float velCorrer;
    public float velSaltar;
    Rigidbody2D rb2D;

    public float puntosVidaPlayer; 
    public float vidaMaxPlayer = 3;
    public Image barraDeVida;

    public Image  nivelEnergia;
    public int cantEnergia;
    public GameObject[] Barras;

    Animator anim;

    public AudioSource clipPU;
    public AudioSource clipBala;
    public AudioSource clipDolor;
    public GameObject Dolor;
    //public AudioSource clipCorrer;
    //public GameObject Pasos;
   
     

    

 
    void Start()
    {

        anim = GetComponent<Animator>();
        puntosVidaPlayer = vidaMaxPlayer;
        rb2D = GetComponent<Rigidbody2D>(); // mete el componente rigidbody dentro de la variable
        clipDolor = GetComponent<AudioSource>();
        

        nivelEnergia.GetComponent<Image>().color = new Color (0, 240, 255 );
        //energia.GetComponent<Image>().color = new Color (0, 240, 255  );

        // BARRITAS DE ENERGIA
    for (int i = 0; i < cantEnergia; i++) {
        Barras [i].gameObject.SetActive(true);
        Barras[i].GetComponent<Image>().color = new Color (0, 240, 255  );

    }


    }

    void Update()
    {
        barraDeVida.fillAmount = puntosVidaPlayer / vidaMaxPlayer;

    if (Input.GetKey("a")) 
    {
        rb2D.velocity = new Vector2 (-velCorrer  , rb2D.velocity.y); // en que direccion ir (eje Y se queda como esta)
        //gameObject.GetComponent <Rigidbody2D>().AddForce(new Vector2(-800f * Time.deltaTime, 0));
        gameObject.GetComponent <Animator>().SetBool("mooving", true);
        gameObject.GetComponent <Animator>().SetBool("shoot", false);
        gameObject.GetComponent <Animator>().SetBool("fight", false);
       //gameObject.GetComponent <SpriteRenderer>().flipX = true;
       transform.eulerAngles = new Vector3 (0,180, 0); // para voltear al personaje

       
       
        
    }
   

    if (Input.GetKey("d")) 
    {
         rb2D.velocity = new Vector2 (velCorrer , rb2D.velocity.y); // en que direccion ir (eje y se queda como esta)
       // gameObject.GetComponent <Rigidbody2D>().AddForce(new Vector2(800f * Time.deltaTime, 0));
        gameObject.GetComponent <Animator>().SetBool("mooving", true);
        gameObject.GetComponent <Animator>().SetBool("shoot", false);
        gameObject.GetComponent <Animator>().SetBool("fight", false);
        //gameObject.GetComponent <SpriteRenderer>().flipX = false;
        transform.eulerAngles = new Vector3 (0,0, 0); // para voltear al personaje
        
    }
    

    if (Input.GetKey ("space") && checkGround.estaEnSuelo ) { 
        
           rb2D.velocity = new Vector2 (rb2D.velocity.x, velSaltar);

        
        gameObject.GetComponent <Animator>().SetBool("jump", true);

    
    }

      if (Input.GetKey ("space")  && tocaPlataforma.enPlat ) // si esta sobre una plataforma puede saltar 
    {
        rb2D.velocity = new Vector2 (rb2D.velocity.x, velSaltar);
        
        gameObject.GetComponent <Animator>().SetBool("mooving", false);
        gameObject.GetComponent <Animator>().SetBool("jump", true);
        
    } /*else if (Input.GetKey ("space"))
    {
        gameObject.GetComponent <Animator>().SetBool("jump", true);
    }*/


    if (!Input.GetKey("a") && !Input.GetKey("d") && !Input.GetKeyDown("w") && !Input.GetKey ("space") ) { // esto es para que las animaciones no sigan funcionando cuando se dejan d presionar las teclas
         gameObject.GetComponent <Animator>().SetBool("mooving", false);
         gameObject.GetComponent <Animator>().SetBool("jump", false);
           
    }


    if (Input.GetKey("mouse 0")) { //dispara con el mouse
        gameObject.GetComponent <Animator>().SetBool("shoot", true);
        gameObject.GetComponent <Animator>().SetBool("fight", false);

        
    }
   
    if (Input.GetKeyDown ("mouse 0")) { //dispara con el mouse
        if (cantEnergia > 0 ) { 
             Instantiate(Bullet, PuntoDisparo.position, transform.rotation);// crea objeto en base a la rotacion           
                cantEnergia -= 1;
               clipBala.Play();
                
        } 
        
        Barras [cantEnergia].gameObject.SetActive(false);
       
    }

   /* if (cantEnergia <=0) { //si no tiene energia, no dispara
        Destroy(energia);
       
    }*/

    if (Input.GetKeyDown ("mouse 1")) { // con boton derecho del mouse pelea
        gameObject.GetComponent <Animator>().SetBool("fight", true);
    }


    // hud de energia se pone rosa
    if (cantEnergia <=4 ) {
       for (int i = 0; i < cantEnergia; i++) {
        Barras [i].gameObject.SetActive(true);
        Barras[i].GetComponent<Image>().color = new Color (255, 0, 255 );

    }

    nivelEnergia.GetComponent<Image>().color = new Color (255, 0, 255);
      
    }

    //vuelve a celeste
    if (cantEnergia >=5) {
        for (int i = 0; i < cantEnergia; i++) {
        Barras [i].gameObject.SetActive(true);
        Barras[i].GetComponent<Image>().color = new Color (0, 240, 255);

    }
        nivelEnergia.GetComponent<Image>().color = new Color (0, 240, 255);
    }
    
    }




   private void OnTriggerEnter2D (Collider2D col) { //cuando collider entra en contacto con otro collider


        //JUNTAR BALAS (ENERGIA)
       if (col.gameObject.tag == "balas" && cantEnergia < 8 ) {
              Destroy(col.gameObject);
    
              cantEnergia +=2;
              clipPU.Play();


    for (int i = 0; i < cantEnergia; i++) {
        Barras [i].gameObject.SetActive(true);

    } 

    
       } 

       if (col.gameObject.tag == "balas" && cantEnergia == 8) {
        Destroy(col.gameObject);
    
              cantEnergia +=1;
              clipPU.Play();


    for (int i = 0; i < cantEnergia; i++) {
        Barras [i].gameObject.SetActive(true);

    } 
       }

       
    }

    private void OnCollisionEnter2D (Collision2D collision) { // verificar q colisionamos con la plataforma cuando se mueve
        if (collision.gameObject.tag == "plataformaMovible") {
            transform.parent = collision.transform;
            

        }
     if (collision.gameObject.tag == "balaEnemigo" ) { // animacion de daño
        anim.SetTrigger ("hurt");
        
    }


    }

    private void OnCollisionExit2D (Collision2D collision) { // q el personaje ya no se mueva con la plataforma cuando ya se bajo
        if (collision.gameObject.tag == "plataformaMovible") {
            transform.parent = null;

        }
    }


    public void TakeHit (float golpe) { // personaje pierde vida
        puntosVidaPlayer -= golpe;
        Instantiate(Dolor);
        gameObject.GetComponent <Animator>().SetBool("hurt", true);
         if (puntosVidaPlayer <=0) {
            Destroy(gameObject);
        }


    }

    public void golpeSuki (float daño) { // daño de suki
        puntosVidaPlayer -= daño;
    if (puntosVidaPlayer <=0) {
         Destroy(gameObject);
    }



    }



    

}
