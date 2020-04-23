using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.WSA.Input;


public class GameLearnInteractions : GazeInput
{
    private GestureRecognizer _gestureRecognizer;

    internal override void Start()
    {
        base.Start();

        //Register the application to recognize HoloLens user inputs
        _gestureRecognizer = new GestureRecognizer();
        _gestureRecognizer.SetRecognizableGestures(GestureSettings.Tap);
        _gestureRecognizer.Tapped += GestureRecognizer_Tapped;
        _gestureRecognizer.StartCapturingGestures();
    }


    private void GestureRecognizer_Tapped(TappedEventArgs obj)
    {
        Debug.Log(FocusedObject.tag);
        if (FocusedObject.tag == "Back") {
            base.DestroyCursor(0);
            SceneManager.LoadScene("Cards");
        }
        else if (FocusedObject.tag == "Replay") {
            GameLearnController.Instance.PlayAnimation();
        }
        else if (FocusedObject.tag == "Pass") {
            GameLearnController.Instance.UpdateScore(1);
            base.DestroyCursor(0);
            SceneManager.LoadScene("Cards");
        }
        else if (FocusedObject.tag == "Fail") {
            GameLearnController.Instance.UpdateScore(0);
            GameLearnController.Instance.PlayAnimation();
        }
        else if (FocusedObject.tag == "FullSpeed")
        {
            GameLearnController.Instance.FullSpeed();
            GameLearnController.Instance.PlayAnimation();
        }
        else if (FocusedObject.tag == "HalfSpeed")
        {
            GameLearnController.Instance.HalfSpeed();
            GameLearnController.Instance.PlayAnimation();
        }
        else if (FocusedObject.tag == "QuarterSpeed")
        {
            GameLearnController.Instance.QuarterSpeed();
            GameLearnController.Instance.PlayAnimation();
        }
    }

    // Update is called once per frame
    internal override void Update()
    {
        base.Update();
    }
}

