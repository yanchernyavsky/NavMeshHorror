﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 5f;
    public bool Stealth;
    public Renderer rend;
    public Material active;
    public Material notactive;
    public GameObject Light;
    public GameObject Explosion;
    public ThirdPersonCharacter character;
    Transform target;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }


    IEnumerator EnemyEnable()
    {
        yield return new WaitForSeconds(2f);
        if (Stealth == false)
        {
        agent.SetDestination(target.position);
        rend.material = active;
        Light.SetActive(true);
        }
    } 

    // Update is called once per frame
    void Update()
    {
        float Distance = Vector3.Distance(target.position, transform.position);
        Stealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Crouch;

        if (Distance <= lookRadius)
        {
            StartCoroutine("EnemyEnable");
        }
        else
        {
            rend.material = notactive;
            Light.SetActive(false);
        }

        if (Distance <=3)
        {
            agent.SetDestination(target.position);
            rend.material = active;
            Light.SetActive(true);
            Light.GetComponent<Light>().intensity = 2;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().PlayerIsDead = true;
            Instantiate(Explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        if (Distance <= 1.5f && Stealth == false)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().PlayerIsDead = true;
            Instantiate(Explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        {
            if (agent.remainingDistance > agent.stoppingDistance)
            {
                character.Move(agent.desiredVelocity, false, false);
            }
            else
            {
                character.Move(Vector3.zero, false, false);
            }
        }//animation
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }


}
