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

    public void Update()
    {
        //Update switch for forward and backward movement also including stop
        switch (_outAction)
        {
            case "forward":
                Forward();
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
        animator.SetFloat("Speed", horizontalMove * moveSpeed);
        Debug.Log("Run animator" + animator.name);
        float step = moveSpeed * Time.deltaTime;

        // move sprite towards the target location
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
    }
}
