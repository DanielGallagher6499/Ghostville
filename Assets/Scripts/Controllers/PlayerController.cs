using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioClip coinSound;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("GoldCoin"))
        {
            //Plays a sound for the coin
            //AudioSource.PlayClipAtPoint(coinSound, transform.position);
            //Destorys the game object after a collision (ie the coin)
            Destroy(other.gameObject);
        }
    }
}
