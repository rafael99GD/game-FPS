using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        other.GetComponent<ITriggerTarget>()?.HitByPlayer(player);
    }

    private void OnTriggerExit(Collider other)
    {
        other.GetComponent<ITriggerExitable>()?.ExitedByPlayer();
    }

}
