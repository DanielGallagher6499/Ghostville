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

public class PauseMenuController : MonoBehaviour
{
    //Variables
    public GrammarRecognizer gr;
    private string _outAction;
    public GameObject pauseMenuUI;
    public static bool gameIsPaused = false;


    public void Update()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        gr = new GrammarRecognizer(Path.Combine(Application.streamingAssetsPath, "pauseMenuGrammar.xml"), ConfidenceLevel.Low);
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
                case "resume":
                    Resume();
                    break;
                case "pause":
                    Pause();
                    break;
                case "menu":
                    Menu();
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

    private void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    private void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
