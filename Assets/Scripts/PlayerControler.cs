using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControler : MonoBehaviour
{
   // public Transform bala;
    public Transform PuntoDisparo;  // desde donde sale la bala
    public bullet Bullet; // img bala
    //bool  canJump;

    public float velCorrer;
    public float velSaltar = 3;
    Rigidbody2D rb2D;

    public float puntosVidaPlayer; 
    public float vidaMaxPlayer = 3;
    public Image barraDeVida;

    public Image energia;
    public Image  nivelEnergia;
    public int cantEnergia;
    public RectTransform posPrimerBarrita; // transform dentro del canvas para manjear ui
    public Canvas MyCanvas; // para dibujar mas energia (hacer hijos)
    public int Offset; // donde dibujar las barritas


    

 
    void Start()
    {

        puntosVidaPlayer = vidaMaxPlayer;
        rb2D = GetComponent<Rigidbody2D>(); // mete el componente rigidbody dentro de la variable

        nivelEnergia.GetComponent<Image>().color = new Color (0, 240, 255 );
        energia.GetComponent<Image>().color = new Color (0, 240, 255 );

    // QUE ARRANQUE CON 8 BARRITAS DE ENTRADA
        for (int i = 0; i < cantEnergia; i++)
        
        {
            //crea una var llamado newenergia. es una instancia de energia  y la ubica en la primera posicion de la barra
            Image NewEnergia = Instantiate(energia,posPrimerBarrita.position, Quaternion.identity );

                NewEnergia.transform.parent = MyCanvas.transform;
                posPrimerBarrita.position = new Vector2 (posPrimerBarrita.position.x , posPrimerBarrita.position.y + Offset);
                
        }

    }

    void Update()
    {
        barraDeVida.fillAmount = puntosVidaPlayer / vidaMaxPlayer;

    if (Input.GetKey("a")) 
    {
        rb2D.velocity = new Vector2 (-velCorrer, rb2D.velocity.y); // en que direccion ir (eje Y se queda como esta)
        //gameObject.GetComponent <Rigidbody2D>().AddForce(new Vector2(-800f * Time.deltaTime, 0));
        gameObject.GetComponent <Animator>().SetBool("mooving", true);
        gameObject.GetComponent <Animator>().SetBool("shoot", false);
       //gameObject.GetComponent <SpriteRenderer>().flipX = true;
       transform.eulerAngles = new Vector3 (0,180, 0); // para voltear al personaje
        
    }
   

    if (Input.GetKey("d")) 
    {
         rb2D.velocity = new Vector2 (velCorrer, rb2D.velocity.y); // en que direccion ir (eje y se queda como esta)
       // gameObject.GetComponent <Rigidbody2D>().AddForce(new Vector2(800f * Time.deltaTime, 0));
        gameObject.GetComponent <Animator>().SetBool("mooving", true);
        gameObject.GetComponent <Animator>().SetBool("shoot", false);
        //gameObject.GetComponent <SpriteRenderer>().flipX = false;
        transform.eulerAngles = new Vector3 (0,0, 0); // para voltear al personaje
    }
    

   /* if (Input.GetKey ("space") && canJump) {
        canJump = false;
        rb2D.velocity = new Vector2 (rb2D.velocity.x, velSaltar);
        //gameObject.GetComponent <Rigidbody2D>().AddForce(new Vector2(0, velSaltar));
       // gameObject.GetComponent <Animator>().SetBool("jumping", true);
        gameObject.GetComponent <Animator>().SetBool("mooving", false);
    }*/
      if (Input.GetKey ("space")  && tocaPlataforma.enPlat )
    {
        rb2D.velocity = new Vector2 (rb2D.velocity.x, velSaltar);
        gameObject.GetComponent <Animator>().SetBool("mooving", false);
    }

    if (!Input.GetKey("a") && !Input.GetKey("d") && !Input.GetKeyDown("w") && !Input.GetKey ("space") ) { // esto es para que las animaciones no sigan funcionando cuando se dejan d presionar las teclas
         gameObject.GetComponent <Animator>().SetBool("mooving", false);
         gameObject.GetComponent <Animator>().SetBool("jumping", false);
           
    }


    if (Input.GetKey("mouse 0")) { //dispara con el mouse
        gameObject.GetComponent <Animator>().SetBool("shoot", true);

        
    }
   
    if (Input.GetKeyDown ("mouse 0")) { //dispara con el mouse
        if (cantEnergia > 0 ) { 
             Instantiate(Bullet, PuntoDisparo.position, transform.rotation);// crea objeto en base a la rotacion           
             Destroy(MyCanvas.transform.GetChild(cantEnergia + 1).gameObject);
                cantEnergia -= 1;
            posPrimerBarrita.position = new Vector2 (posPrimerBarrita.position.x , posPrimerBarrita.position.y - Offset); // cuando se elimina una barrita, tambien se elimina su posicion. Esto es para que las nuevas se dibujen a partir de esa ultima que se elimino
                
        } 
       
    }

   /* if (cantEnergia <=0) { //si no tiene energia, no dispara
        Destroy(energia);
       
    }*/

    if (cantEnergia <=5 ) {
        //esto le cambia el color al hud de la energia
        nivelEnergia.GetComponent<Image>().color = new Color (255, 0, 255);
        

      
        
    }


    // esto es para cambiarle el color a las barritas. En realidad cambia cuando le quedan 4 o menos pero si ponia eso me funcaba mal, por eso puse 7. Cuando las gastas todas tira errores porque hay un problema con los objetos hijos (las barritas xd) del canvas y al parecer esta detectando menos de las que hay pero igualmente funciona (obviamente no es la mejor opcion pero mientras funque). No se a largo plazo si va  a afectar en algo o no
    if (MyCanvas.transform.childCount < 7  ) {
        
        MyCanvas.transform.GetChild(2).gameObject.GetComponent<Image>().color = new Color (255, 0, 255);
        MyCanvas.transform.GetChild(3).gameObject.GetComponent<Image>().color = new Color (255, 0, 255);
        MyCanvas.transform.GetChild(4).gameObject.GetComponent<Image>().color = new Color (255, 0, 255);
        MyCanvas.transform.GetChild(5).gameObject.GetComponent<Image>().color = new Color (255, 0, 255);

    }

    if (cantEnergia >=5) {
        nivelEnergia.GetComponent<Image>().color = new Color (0, 240, 255);
       /*MyCanvas.transform.GetChild(2).GetComponent<Image>().color = new Color (255, 0, 255);
      MyCanvas.transform.GetChild(3).GetComponent<Image>().color = new Color (255, 0, 255);
      MyCanvas.transform.GetChild(4).GetComponent<Image>().color = new Color (255, 0, 255);
      MyCanvas.transform.GetChild(5).GetComponent<Image>().color = new Color (255, 0, 255);*/
    }
    
    }

    

//SOLO SALTA CUANDO TOCA EL PISO 
  /* private void OnCollisionEnter2D (Collision2D collision) {
        if (collision.transform.tag =="ground" ) {
            canJump = true;
            
        }
        /*if (collision.transform.tag =="platform" ) {
            canJump = true;
            
        }
           
    }*/


    //ESTO NO SE Ni COMO FUNCA LA VERDAD 
   private void OnTriggerEnter2D (Collider2D col) { //cuando collider entra en contacto con otro collider

   Transform posBarrita = posPrimerBarrita; 
   int cantEnergiaRecogida = 1;


        //JUNTAR BALAS (ENERGIA)
       if (col.gameObject.tag == "balas" && cantEnergia < 8 ) {
              Destroy(col.gameObject);
    
              cantEnergia +=1;
    
        for (int i = 0; i < cantEnergiaRecogida; i++)
        
        {
            //crea una var llamado newenergia. es una instancia de energia  y la ubica en la primera posicion de la barra
            Image NewEnergia = Instantiate(energia, posBarrita.position, Quaternion.identity );

                NewEnergia.transform.parent = MyCanvas.transform;
                posBarrita.position = new Vector2 (posBarrita.position.x , posBarrita.position.y + Offset);
                
        }
           
        
        } 
       
    }

    private void OnCollisionEnter2D (Collision2D collision) { // verificar q colisionamos con la plataforma cuando se mueve
        if (collision.gameObject.tag == "plataformaMovible") {
            transform.parent = collision.transform;

        }
    }

    private void OnCollisionExit2D (Collision2D collision) { // q el personaje ya no se mueva con la plataforma cuando ya se bajo
        if (collision.gameObject.tag == "plataformaMovible") {
            transform.parent = null;

        }
    }


    public void TakeHit (float golpe) { // personaje pierde vida
        puntosVidaPlayer -= golpe;
       if (puntosVidaPlayer <=0) {
            Destroy(gameObject);
        }


    }



   
}
