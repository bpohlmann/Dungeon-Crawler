using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RitterFolgenAI : MonoBehaviour {
    public float rotationsGeschwindigkeit = 5;
    public Transform Spieler;
    public float geschwindigkeit = 100;
    public float minimalAbstand = 3;

    private Animator anim;
    private int standBool;
    

    // Use this for initialization

    void Start () {
        anim = transform.GetComponent<Animator>();
        standBool = Animator.StringToHash("Stehen");
    }
	
	// Update is called once per frame
	void Update () {
        Rotieren(Spieler.position);
        Folgen(Spieler.position );
       
    }

    void Rotieren(Vector3 SpielerPosition)
    {
        //Zeitversetzt
        Vector3 posi = SpielerPosition - transform.position;  
        Quaternion.LookRotation(posi);
        Quaternion destRotation = Quaternion.LookRotation(posi);
        transform.rotation = Quaternion.Slerp(transform.rotation, destRotation, rotationsGeschwindigkeit * Time.deltaTime); 
    }

    void Folgen(Vector3 SpielerPosition)
    {
        Vector3 a;
        Vector3 abstand = SpielerPosition - transform.position;
        Vector3 richtung = transform.TransformDirection(Vector3.forward);

        if (abstand.magnitude > minimalAbstand) {
            anim.SetBool(standBool, false);
            a = richtung.normalized * geschwindigkeit * Time.deltaTime;
           
        }
        else
        {
            
            a = Vector3.zero;
            
        }
        if (abstand.magnitude <= minimalAbstand  )
        {
            anim.SetBool(standBool, true);
        }
        GetComponent<Rigidbody>().velocity = a; 
    }
}
