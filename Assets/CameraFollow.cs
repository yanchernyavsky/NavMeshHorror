using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    bool pause = true;
    public GameObject PauseTXT;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
    public void Pause()
    {
        pause = !pause;
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pause == false)
            {
                PauseTXT.SetActive(true);
                Time.timeScale = 0;
                //pause = true;
                
            }
            else
            {
                PauseTXT.SetActive(false);
                Time.timeScale = 1;
                //pause = false;
            }
        }
    }
}