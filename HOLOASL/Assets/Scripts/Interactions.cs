using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.WSA.Input;

public class Interactions : GazeInput
{
    // Reference to scene org script 
    //SceneOrganiser sceneOrgScript;
    private static bool isListening = false;
    /// <summary>
    /// Allows input recognition with the HoloLens
    /// </summary>
    private GestureRecognizer _gestureRecognizer;

    /// <summary>
    /// Called on initialization, after Awake
    /// </summary>
    internal override void Start()
    {
        base.Start();

        //Register the application to recognize HoloLens user inputs
        _gestureRecognizer = new GestureRecognizer();
        _gestureRecognizer.SetRecognizableGestures(GestureSettings.Tap);
        _gestureRecognizer.Tapped += GestureRecognizer_Tapped;
        _gestureRecognizer.StartCapturingGestures();

        // assign to scene org script
        //sceneOrgScript = FindObjectOfType<SceneOrganiser>();
    }


    /// <summary>
    /// Detects the User Tap Input
    /// </summary>
    private void GestureRecognizer_Tapped(TappedEventArgs obj)
    {
        /*        // Ensure the bot is being gazed upon.
                if (FocusedObject != null)
                {
                    // If the user is tapping on Bot and the Bot is ready to listen
                    if (FocusedObject.name == "Bot" && Bot.Instance.botState == Bot.BotState.ReadyToListen && !isListening)
                    {
                        isListening = true;
                        Bot.Instance.SetBotResponseText("Listening...");
                        StartCoroutine(Bot.Instance.StartConversation());
                        Bot.Instance.StartCapturingAudio();
                    }
                    // If the user is tapping on Bot and the Bot is ready to listen
                    else if (FocusedObject.name == "Bot" && Bot.Instance.botState == Bot.BotState.ReadyToListen && isListening)
                    {
                        isListening = false;
                        Bot.Instance.SetBotResponseText("Gaze on the speaker and tap to start listening");
                        Bot.Instance.StopCapturingAudio();
                    }

                }
                else
                {
          */

        if (FocusedObject != null)
        {
            if (FocusedObject.tag == "Back")
            {
                if (isListening)
                {
                    isListening = false;
                    Bot.Instance.StopCapturingAudio();
                }
                SceneManager.LoadScene(0);
            }

        }

        else
        {
            if (!isListening)
            {
                Bot.Instance.placeBotAtGaze();
                isListening = true;
                Bot.Instance.SetBotResponseText("Listening...");
                StartCoroutine(Bot.Instance.StartConversation());
                Bot.Instance.StartCapturingAudio();
            }
            else
            {
                isListening = false;
                Bot.Instance.SetBotResponseText("Gaze on the speaker and tap to start listening");
                Bot.Instance.StopCapturingAudio();
            }
        }
    }
}
