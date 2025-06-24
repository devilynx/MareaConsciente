using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    private float vel = 40f,jp=5f;
    private Rigidbody rbd;
    public bool grounded, j;
    
    void Start(){
        rbd = GetComponent<Rigidbody>();
    }
    void OnCollisionStay(Collision c)
    {
        if (c.gameObject.tag == "ground")
        {
            grounded = true;
            //Debug.Log("verdadero");
        }
    }
    void OnCollisionExit(Collision c)
    {
        if (c.gameObject.tag == "ground")
        {
            grounded = false;
            //Debug.Log("falso");
        }
    }
    void Update(){
        float d=Input.GetAxis("Jump");
        if (d>=0.1f)
        {
            j = true;
        }
    }
    void FixedUpdate(){
        
        float x = Input.GetAxis("Horizontal");//detecta cuando se presiona las flechas del pad o teclado
        float z = Input.GetAxisRaw("Vertical");//si es Raw es mas fluido
        Debug.Log(x);
        //Vector3 m=new Vector3(x,0,z).normalized; //obtiene los valores de la direccion en la que se mueve (con el teclado)
        Vector3 v = transform.right * x + transform.forward * z;
        rbd.MovePosition(transform.position + v *vel* Time.deltaTime);
        //Debug.Log(z);
        if (j)
        {
            rbd.AddForce(Vector3.up * jp, ForceMode.Impulse);
            j = false;
        }
    }
}
