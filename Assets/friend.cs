using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.AI;

public class friend : MonoBehaviour
{
    public float lookRadius = 3f;
    public GameObject Player;
    public ThirdPersonCharacter character;
    Transform target;
    NavMeshAgent agent;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Door").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float Distance = Vector3.Distance(Player.transform.position, transform.position);

        if (Distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            Destroy(gameObject, 3);
        }

        
            if (agent.remainingDistance > agent.stoppingDistance)
            {
                character.Move(agent.desiredVelocity, false, false);
            }
            else
            {
                character.Move(Vector3.zero, false, false);
            }
        
    }

}


