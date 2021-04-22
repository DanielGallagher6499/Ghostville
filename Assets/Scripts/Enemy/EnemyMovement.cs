using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = .3f;
    public bool isDead = false;


    // Update is called once per frame
    void Update()
    {
        Forward();
    }

    public void Forward()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject);
            isDead = true;
            if (isDead == true)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}

