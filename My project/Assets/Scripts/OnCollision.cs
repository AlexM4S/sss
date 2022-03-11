using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollision : MonoBehaviour
{
    // Este script devuelve el estado collision al detectar que el Player colisiona

    public Character m_char;

    private void OnControllerColliderHit(ControllerColliderHit collision)
    {
        if (collision.transform.tag == "Player")
            return;
        m_char.OnCharacterColliderHit(collision.collider);
    }

}
