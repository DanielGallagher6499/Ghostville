using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public Animator animator;
    public Rigidbody2D rb;
    public float moveSpeed = 1.3f;
    float horizontalMove = 1f;
    public float jumpPower = 1f;
    public static bool isGrounded = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("GoldCoin"))
        {
            //Destorys the game object after a collision (ie the coin)
            Destroy(other.gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            animator.SetFloat("Speed", 0);
            animator.SetBool("Jump", true);
            rb.AddForce(Vector2.up * jumpPower);
            isGrounded = false;
        }

        if (Input.GetKey("d"))
        {
            animator.SetBool("Jump", false);
            animator.SetFloat("Speed", horizontalMove * moveSpeed);
            isGrounded = true;
            float step = moveSpeed * Time.deltaTime;

            // move sprite towards the target location
            //transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, GameObject.FindGameObjectWithTag("Frontpoint").transform.position, step * 1);
        }

        if (Input.GetKey("a"))
        {
             animator.SetBool("Jump", false);
             animator.SetFloat("Speed", horizontalMove * moveSpeed);
             isGrounded = true;

             //Moves player backwards
             float step = moveSpeed * Time.deltaTime;
             // move sprite towards the target location
             transform.position = Vector2.MoveTowards(transform.position, GameObject.FindGameObjectWithTag("Endpoint").transform.position, step * 1);
        }
    }
}
