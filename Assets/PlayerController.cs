using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
    public GameObject Light;
    public NavMeshAgent agent;
    public ThirdPersonCharacter character;
    public bool PlayerIsDead;
    public Animator animator;
    public GameObject DoorTrigger;
    public bool Crouch = false;
    public bool Win = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "DoorTrigger")
        {
            GameObject.Find("LevelChanger").GetComponent<LevelChanger>().FadeToNextLevel();
        }
        if (other.gameObject.name == "Win")
        {
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


    // Update is called once per frame
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
                Light.GetComponent<Light>().intensity = 4f;
            }

            else if (Crouch == true)
            {
                Crouch = false;
                Light.GetComponent<Light>().intensity = 7f;
            }
        }



        if (PlayerIsDead == true)
        {
            character.Move(Vector3.zero, false, false);
            animator.SetBool("DeathTrigger",true);
            Light.GetComponent<Light>().intensity = 2f;
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
}
