using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Collector : MonoBehaviour
{
    [SerializeField] Text countText;
    private int countApple = 0;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "fruit")
        {
            countApple++;
            countText.text = "APPLES: " + countApple;
            Destroy(other.gameObject);
        }
    }
}
