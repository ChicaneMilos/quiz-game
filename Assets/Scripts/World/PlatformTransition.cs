using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTransition : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        this.gameObject.GetComponent<BoxCollider>().isTrigger = false;
    }
}
