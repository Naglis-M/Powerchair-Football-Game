using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball" || other.gameObject.tag == "GoalPost")
        {
            print("ENTER");
        }

    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Ball" || other.gameObject.tag == "GoalPost")
        {
            print("STAY");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ball" || other.gameObject.tag == "GoalPost")
        {
            print("EXIT");
        }
    }

}
