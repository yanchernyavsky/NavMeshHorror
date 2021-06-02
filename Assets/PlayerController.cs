using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;
public class PlayerController : MonoBehaviour
{
    public GameObject crouchbutton;
    public FixedJoystick joystick;
    public GameObject pause;
    public Renderer rend;
    public Material invis;
    public Material vis;
    public Camera cam;
    public GameObject LightYellow;
    public GameObject LightBlue;
    public NavMeshAgent agent;
    public Rigidbody rigidbody;
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
    
    public void SetCrouch()
    {
        Crouch =! Crouch;
    }
   
    void Update()
    {
        //if (Input.GetMouseButtonDown(0) && PlayerIsDead == false)
        //{
        //    Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;

        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        agent.SetDestination(hit.point);
        //    }
        //}

        //var velocity = new Vector3(joystick.Vertical, 0f, joystick.Horizontal) * 20f;
        transform.eulerAngles = new Vector3(0, Mathf.Atan2(joystick.Horizontal, joystick.Vertical) * 180 / Mathf.PI+45f, 0);
        var velocity = transform.forward;


        if (/*Input.GetKeyDown(KeyCode.Space) &&*/ PlayerIsDead == false)
        {
            if (Crouch == true)
            {
                //Crouch = true;
                LightYellow.GetComponent<Light>().intensity = 4f;
            }
            else if (Crouch == false)
            {
                //Crouch = false;
                LightYellow.GetComponent<Light>().intensity = 7f;
            }
        }
        
        
        {
            if ( joystick.Vertical == 0 && joystick.Horizontal == 0) //agent.remainingDistance > agent.stoppingDistance)
            {
                character.Move(Vector3.zero, Crouch, false);
            }
            else
            {
                character.Move(velocity, Crouch, false);
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
            joystick.gameObject.SetActive(false);
            crouchbutton.gameObject.SetActive(false);
            pause.SetActive(false);
            //agent.SetDestination(transform.position);
            agent.isStopped = true;
            character.Move(Vector3.zero, false, false);
            animator.SetBool("DeathTrigger", true);
            LightYellow.GetComponent<Light>().intensity = 2f;
        }
            
        
    }
}
