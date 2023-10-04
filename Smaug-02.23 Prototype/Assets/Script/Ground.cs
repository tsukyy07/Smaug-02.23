using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    MovimentoJogador Player;

    void Start()
    {
        Player = gameObject.transform.parent.gameObject.GetComponent<MovimentoJogador>();
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D collisor)
    {
        if(collisor.gameObject.layer == 6)
        {
            Player.isJumping = false;
        }
    }

    void OnCollisionExit2D(Collision2D collisor)
    {
        if (collisor.gameObject.layer == 6)
        {
            Player.isJumping = true;
        }
    }

}
