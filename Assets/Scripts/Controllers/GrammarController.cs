using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

//Daniel Gallagher - G00360986
//Ghostville

public class GrammarController : MonoBehaviour
{
    //Variables
    public Animator animator;
    public GrammarRecognizer gr;
    private string _outAction;
    public Rigidbody2D rb;
    public float moveSpeed = 1.3f;
    float horizontalMove = 1f;
    public float jumpPower = 1f;
    public static bool isGrounded = false;

    public void Update()
    {
        //Update switch for forward and backward movement also including stop
        switch (_outAction)
        {
            case "forward":
                Forward();
                break;
            case "backwards":
                Backwards();
                break;
            case "stop":
                Stop();
                break;
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        gr = new GrammarRecognizer(Path.Combine(Application.streamingAssetsPath, "controlsGrammar.xml"), ConfidenceLevel.Low);
        gr.OnPhraseRecognized += GR_OnPhraseRecognized;
        gr.Start();
        Debug.Log("Grammar loaded and recogniser started!");
    }


    private void GR_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        //Phase recognizer
        StringBuilder message = new StringBuilder();
        Debug.Log("Recognised a phrase");

        // read the semantic meanings from the args passed in.
        SemanticMeaning[] meanings = args.semanticMeanings;

        // semantic meanings are returned as key/value pairs
        // use foreach to get all the meanings.
        foreach (SemanticMeaning meaning in meanings)
        {
            string keyString = meaning.key.Trim();
            string valueString = meaning.values[0].Trim();
            message.Append("Key: " + keyString + ", Value: " + valueString + " ");
            _outAction = valueString;

            switch (_outAction)
            {
                case "jump":
                    if (isGrounded == true)
                    {
                        Jump();
                    }
                    break;
            }
        }
        // use a string builder to create the string and out put to the user
        Debug.Log(message);
    }

    private void OnApplicationQuit()
    {
        if (gr != null && gr.IsRunning)
        {
            gr.OnPhraseRecognized -= GR_OnPhraseRecognized;
            gr.Stop();
        }
    }

    private void Forward()
    {
        animator.SetBool("Jump", false);
        animator.SetFloat("Speed", horizontalMove * moveSpeed);
        isGrounded = true;
        Debug.Log("Run animator" + animator.name);
        float step = moveSpeed * Time.deltaTime;

        // move sprite towards the target location
        //transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, GameObject.FindGameObjectWithTag("Frontpoint").transform.position, step * 1);
    }

    private void Stop()
    {
        animator.SetFloat("Speed", 0);
        animator.SetBool("Jump", false);
        isGrounded = true;
        //Stops the player
        rb.velocity = Vector3.zero;
    }

    private void Jump()
    {
        animator.SetFloat("Speed", 0);
        animator.SetBool("Jump", true);
        rb.AddForce(Vector2.up * jumpPower);
        isGrounded = false;
        //Out action is nullified
        _outAction = null;
    }

    private void Backwards()
    {
        animator.SetBool("Jump", false);
        animator.SetFloat("Speed", horizontalMove * moveSpeed);
        isGrounded = true;
        Debug.Log("Run animator" + animator.name);

        //Moves player backwards
        float step = moveSpeed * Time.deltaTime;
        // move sprite towards the target location
        transform.position = Vector2.MoveTowards(transform.position, GameObject.FindGameObjectWithTag("Endpoint").transform.position, step * 1);
    }
}
