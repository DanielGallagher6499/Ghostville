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

public class MainMenuController : MonoBehaviour
{
    //Variables
    public GrammarRecognizer gr;
    private string _outAction;
    public GameObject StartScreen;
    public GameObject ControlsScreen;


    public void Update()
    {
        //Update switch for forward and backward movement also including stop
        /*switch (_outAction)
        {
            case "play":
                Play();
                break;
            case "quit":
                Quit();
                break;
            case "controls":
                Controls();
                break;
        }*/
    }

    // Start is called before the first frame update
    void Start()
    {
        gr = new GrammarRecognizer(Path.Combine(Application.streamingAssetsPath, "mainMenuGrammar.xml"), ConfidenceLevel.Low);
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
                case "play":
                    Play();
                    break;
                case "quit":
                    Quit();
                    break;
                case "controls":
                    Controls();
                    break;
                case "back":
                    Back();
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

   private void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void Quit()
    {
        Application.Quit();
        Debug.Log("Game Closed!");
    }

    private void Controls()
    {
        StartScreen.SetActive(false);
        ControlsScreen.SetActive(true);
    }

    private void Back()
    {
        StartScreen.SetActive(true);
        ControlsScreen.SetActive(false);
    }
}
