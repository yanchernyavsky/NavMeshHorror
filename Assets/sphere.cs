using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sphere : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        FindObjectOfType<PlayerController>().StartCoroutine("InvisiblePlayer");
        gameObject.SetActive(false);
    }
}
