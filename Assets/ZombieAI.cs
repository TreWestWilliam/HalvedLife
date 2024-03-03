using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public GameObject Player;
    public RaycastHit canSeePlayer;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, (Player.transform.position-transform.position ), out canSeePlayer)) 
        {
            if (canSeePlayer.transform.gameObject == Player) 
            {
                agent.destination = new(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z);
                Debug.Log("Can See player");
            }
            Debug.Log(canSeePlayer.collider.gameObject.name);
        }
        
    }
}
