using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;
public class PlayerController : MonoBehaviour
{
    public Renderer rend;
    public Material invis;
    public Material vis;
    public Camera cam;
    public GameObject LightYellow;
    public GameObject LightBlue;
    public NavMeshAgent agent;
    public ThirdPersonCharacter character;
    public bool PlayerIsDead;
    public Animator animator;
    public GameObject DoorTrigger;
    public bool Crouch = false;
    public bool Win = false;
    public bool undead = false;
    public bool invisible = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "DoorTrigger")
        {
            undead = true;
            GameObject.Find("LevelChanger").GetComponent<LevelChanger>().FadeToNextLevel();

        }
        if (other.gameObject.name == "Win")
        {
            undead = true;
            character.Move(Vector3.zero, false, false);
            animator.SetBool("Win", true);
            Win = true;
        }
    }
    private void Start()
    {
        agent.updateRotation = false;
        animator.GetComponent<Animator>();
        PlayerIsDead = false;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)&& PlayerIsDead == false)
        {
           Ray ray = cam.ScreenPointToRay(Input.mousePosition);
           RaycastHit hit;

           if (Physics.Raycast(ray, out hit))
           {
                agent.SetDestination(hit.point);
           }
        }
        if (Input.GetKeyDown(KeyCode.Space) && PlayerIsDead == false)
        {
            if (Crouch == false)
            {
                Crouch = true;
                LightYellow.GetComponent<Light>().intensity = 4f;
            }
            else if (Crouch == true)
            {
                Crouch = false;
                LightYellow.GetComponent<Light>().intensity = 7f;
            }
        }
        
        
        {
            if (agent.remainingDistance > agent.stoppingDistance)
            {
                character.Move(agent.desiredVelocity, Crouch, false);
            }
            else
            {
                character.Move(Vector3.zero, Crouch, false);
            }
        }//movment animation
    }
    //public void InvisiblePlayer()
    //{


    //}

     public IEnumerator InvisiblePlayer()
    {
        invisible = true;
        rend.material = invis;
        LightYellow.SetActive(false);
        LightBlue.SetActive(true);
        yield return new WaitForSeconds(10f);
        PlayerIsVisible();
    }

    public void PlayerIsVisible() {
        invisible = false;
        rend.material = vis;
        LightYellow.SetActive(true);
        LightBlue.SetActive(false);

    }

    public void playerDeath()
    {

        if (!undead)
        {
            PlayerIsDead = true;
            //agent.SetDestination(transform.position);
            agent.isStopped = true;
            character.Move(Vector3.zero, false, false);
            animator.SetBool("DeathTrigger", true);
            LightYellow.GetComponent<Light>().intensity = 2f;
        }
            
        
    }
}
