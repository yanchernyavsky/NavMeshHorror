using System.Collections;
using UnityEngine;
public class StepsController : MonoBehaviour
{
    public Rigidbody PlayerRb;
    public AudioClip[] Steps;
    public GameObject PlayerController;
    public float Speed;
    public float Volume;
    private void Update()
    {
        if (PlayerController.GetComponent<PlayerController>().Crouch == false)
        {
            Speed = 0.3f; Volume = 0.5f;
        }
        else { Speed = 0.5f; Volume = 0.2f; }
    }
    IEnumerator Start()
    {
        while (true)
        {
            if (PlayerRb.velocity.x >= 0.5 || PlayerRb.velocity.z >= 0.5 || PlayerRb.velocity.y >= 0.5)
            {
                GetComponent<AudioSource>().PlayOneShot(Steps[Random.Range(0, Steps.Length)], Volume);
                yield return new WaitForSeconds(Speed);
            }
            else yield return 0;
        }
    }
}
