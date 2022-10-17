using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovEnemigoPlataforma : MonoBehaviour
{
     //Video 3
 //   public float WalkSpeed;
    /* public float distancia; */
   /*
    [HideInInspector]
    public bool mustPatrol;
    private bool mustTurn;

    public Rigidbody2D rb;
    public Transform controladorSuelo;
    public LayerMask groundLayer;
    void Start()
    {
        mustPatrol = true;
    }

    void Update()
    {
        if(mustPatrol)
        {
            Patrol();
        }
       /*  RaycastHit2D groundInfo = Physics2D.Raycast(controladorSuelo.position, Vector2.down, distancia);
        if(groundInfo.collider == false)
        {
            Flip();
        } */
    //}
    /*
    private void FixedUpdate()
    {
        if(mustPatrol == true)
        {
            mustTurn = !Physics2D.OverlapCircle(controladorSuelo.position, 0.1f, groundLayer);
        }
    }

    void Patrol()
    {
        if(mustTurn)
        {
            Flip();
        }
        rb.velocity = new Vector2(WalkSpeed * Time.fixedDeltaTime, rb.velocity.y);

    }

    void Flip()
    {
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        WalkSpeed *= -1;
        mustPatrol = true;
        
    } */
     
    //VIDEO 2
    //Mov sobre plataforma
    private Rigidbody2D m_rig;
    public float speed;
    

    void Start(){
        m_rig = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        m_rig.velocity = new Vector2(speed,m_rig.velocity.y);
        gameObject.GetComponent <Animator>().SetBool("enemyWalk", true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag=="platform")
        {
            speed *= -1;
            this.transform.localScale = new Vector2(this.transform.localScale.x * -1,this.transform.localScale.y);
        }
    }
}
/*     [SerializeField] private float velocidad;
    [SerializeField] private Transform controladorSuelo;
    [SerializeField] private float distancia;
    [SerializeField] private bool moviendoDerecha;
    private Rigidbody2D rb;
     Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit2D informacionSuelo = Physics2D.Raycast(controladorSuelo.position, Vector2.down, distancia);
        rb.velocity = new Vector2(velocidad, rb.velocity.y);
        if(informacionSuelo == false)
        {
            //Girar
            Girar();
        }
    }
    private void Girar()
    {
        moviendoDerecha = !moviendoDerecha;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        velocidad *= -1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(controladorSuelo.transform.position, controladorSuelo.transform.position + Vector3.down * distancia);

    } 
}
*/

