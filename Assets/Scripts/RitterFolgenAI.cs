using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RitterFolgenAI : MonoBehaviour {
    public float rotationsGeschwindigkeit = 5;
    public Transform Spieler;
    public float geschwindigkeit = 100;
    public float minimalAbstand = 3;
    private UnityEngine.AI.NavMeshAgent agent;
    private Animator anim;
    private int standBool;

	public Transform enemy;

	private bool attacking = false;

	public float damageValue = 1;
	private int attackBool;


	private bool readyToHit = true;

    private RitterAI ritterAi;

    // Use this for initialization

    void Start () {
        anim = transform.GetComponent<Animator>();
        standBool = Animator.StringToHash("Stehen");
        ritterAi = GetComponent<RitterAI>();
        ritterAi.enabled = !ritterAi.enabled;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.ResetPath();
		attackBool = Animator.StringToHash ("attackBool");

    }

    // Update is called once per frame
    void Update () {
		if (attacking == false) {

			Rotieren (Spieler.position);
		} else {
			Rotieren (enemy.position);		
		}
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


	void OnTriggerStay(Collider other)
	{
		
			if (other.CompareTag("Enemy")/* && EnemyHealth.health > 0*/)
			{
				if (readyToHit)
				{
					readyToHit = false;
					
					

					other.gameObject.SendMessage (
						"ApplyDamage",damageValue,SendMessageOptions.DontRequireReceiver);
					StartCoroutine("SetReadyToHit");
				}
			}



}

	IEnumerator SetReadyToHit()
	{
		anim.SetBool(attackBool,true);
		attacking = true;
		yield return new WaitForSeconds(0.5F);
		anim.SetBool(attackBool,false);
		attacking = false;
		yield return new WaitForSeconds(0.5F);
		readyToHit = true;

	}
}