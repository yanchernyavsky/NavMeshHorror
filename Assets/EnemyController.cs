using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class EnemyController : MonoBehaviour
{
    public int explosionRadius;
    public float stealthRadius;
    public float lookRadius;
    public bool Stealth;
    public Renderer rend;
    public Material active;
    public Material notactive;
    public GameObject Light;
    public GameObject Explosion;
    public ThirdPersonCharacter character;
    Transform target;
    public NavMeshAgent agent;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    IEnumerator EnemyEnable()
    {
        yield return new WaitForSeconds(0.2f);
        if (Stealth == false)
        {
            agent.SetDestination(target.position);
            rend.material = active;
            Light.SetActive(true);
        }
    }

    void Update()
    {
        float Distance = Vector3.Distance(target.position, transform.position);
        Stealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Crouch;

        if (FindObjectOfType<PlayerController>().invisible==true)
        {
            return;
        }
        if (Distance <= lookRadius)
        {
            StartCoroutine("EnemyEnable");
        }
        else
        {
            rend.material = notactive;
            Light.SetActive(false);
        }

        if (Distance <= explosionRadius)
        {
            agent.SetDestination(target.position);
            rend.material = active;
            Light.SetActive(true);
            Light.GetComponent<Light>().intensity = 2;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().playerDeath();
            Instantiate(Explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        if (Distance <= stealthRadius && Stealth == false)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().playerDeath();
            Instantiate(Explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        if (FindObjectOfType<PlayerController>().undead == true)
        {
            agent.isStopped = true;
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
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, stealthRadius);
    }
}


