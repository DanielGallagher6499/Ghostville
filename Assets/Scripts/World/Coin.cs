using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1;
    public AudioClip coinSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            //Plays a sound for the coin
            AudioSource.PlayClipAtPoint(coinSound, transform.position);
            //Put the score up
            ScoreManager.instance.ChangeScore(coinValue);
        }
    }
}
