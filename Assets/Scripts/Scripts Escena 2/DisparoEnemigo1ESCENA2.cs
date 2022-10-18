using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoEnemigo1ESCENA2 : MonoBehaviour
{
    //este script acciona la bala
    public GameObject LaBala;
    public Transform PuntoDisparo;
   public float tiempoDisparoE;

   public static bool disparando = true;
    // Update is called once per frame
    void Update()
    {
        tiempoDisparoE += Time.deltaTime;

       if (disparando)
       {
        Disparar();
       }

    }

    private void Disparar() {
         if (tiempoDisparoE >= 2)
    {
       GameObject prefab = Instantiate(LaBala, PuntoDisparo.position, transform.rotation) as GameObject;
        tiempoDisparoE = 0;

        Destroy(prefab, 2f);
        
        
    }
    }
}
