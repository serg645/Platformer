using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StickPlatform : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            other.transform.SetParent(this.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            other.transform.SetParent(null);
        }
    }
}
