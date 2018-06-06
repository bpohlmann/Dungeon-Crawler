using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RitterAI : MonoBehaviour {
    private UnityEngine.AI.NavMeshAgent agent;
    private bool dialog;
    public Transform[] waypoints;
    public float waypointPauseTime = 2;
    private int currentWaypointIndex;
    private GameObject player;
    public bool spielerGefunden = false;
    private bool quest = false;
    private Text messageText;

    private RitterFolgenAI ritterFolgenAi;

    private Animator anim;
    private int standBool;
    // Use this for initialization
    void Start () {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = transform.GetComponent<Animator>();
        standBool = Animator.StringToHash("Stehen");
        player = GameObject.FindGameObjectWithTag("Player");
        messageText = GameObject.FindGameObjectWithTag("Message").
            GetComponent<Text>();	//uGUI

        ritterFolgenAi = GetComponent<RitterFolgenAI>(); 
    }
	
	// Update is called once per frame
	void Update () {
        if (spielerGefunden == false)
        {
            WaypointWalk();
        }
        else  {

            agent.SetDestination(gameObject.transform.position);

            anim.SetBool(standBool, true );

            
        }



    }

    void WaypointWalk()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            if (currentWaypointIndex == waypoints.Length - 1)
                currentWaypointIndex = 0;
            else
                currentWaypointIndex++;
           
            StartCoroutine("WaypointPause", waypointPauseTime);
        }
        agent.SetDestination(waypoints[currentWaypointIndex].position);
    }

    IEnumerator WaypointPause(float seconds)
    {
        if (spielerGefunden == false)
        {
            float oldSpeed = agent.speed;
            agent.speed = 0.1F;
            yield return new WaitForSeconds(seconds);
            agent.speed = oldSpeed;
           
        }
        }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            spielerGefunden = true;
            dialog = true;
        }
    }
    private void OnGUI()
    {
        if (dialog)
        {
            if (quest == false) {
                Time.timeScale = 0; 
                GUILayout.BeginArea(new Rect(10, 10, 200, 200));
                GUILayout.Label("ja/Nein");
                if (GUILayout.Button("Ja"))
                {
                    quest = true;
                    Time.timeScale = 1;
                    dialog = false;
                    ritterFolgenAi.enabled = !ritterFolgenAi.enabled;

                }
                if (GUILayout.Button("Nein"))
                {
                    quest = false;
                    Time.timeScale = 1;
                    dialog = false;
                    
                }
                GUILayout.EndArea();
            }
            
        }
    }
    
}
