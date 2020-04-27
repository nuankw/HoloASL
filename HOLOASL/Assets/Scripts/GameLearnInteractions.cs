using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.WSA.Input;


public class GameLearnInteractions : GazeInput
{
    private GestureRecognizer _gestureRecognizer;
    private AudioSource audio_ding;

    internal override void Start()
    {
        base.Start();

        //Register the application to recognize HoloLens user inputs
        _gestureRecognizer = new GestureRecognizer();
        _gestureRecognizer.SetRecognizableGestures(GestureSettings.Tap);
        _gestureRecognizer.Tapped += GestureRecognizer_Tapped;
        _gestureRecognizer.StartCapturingGestures();

        audio_ding = GetComponent<AudioSource>();
    }

    private void GestureRecognizer_Tapped(TappedEventArgs obj)
    {
        Debug.Log(FocusedObject.tag);
        if (FocusedObject.tag == "Back") {
            audio_ding.Play();
            DestroyCursor(0);
            SceneManager.LoadScene("Cards");
        }
        else if (FocusedObject.tag == "Pass") {
            audio_ding.Play();
            GameLearnController.Instance.Unlock_Current_Vocab();
            DestroyCursor(0);
            // @Jix: add some special sound effect here!
        }
        else if (FocusedObject.tag == "FullSpeed") {
            audio_ding.Play();
            GameLearnController.Instance.Play_at_Full_Speed();
        }
        else if (FocusedObject.tag == "HalfSpeed") {
            audio_ding.Play();
            GameLearnController.Instance.Play_at_Half_Speed();
        }
        else if (FocusedObject.tag == "QuarterSpeed") {
            audio_ding.Play();
            GameLearnController.Instance.Play_at_Quarter_Speed();
        }
    }

    // Update is called once per frame
    internal override void Update()
    {
        base.Update();
    }
}

