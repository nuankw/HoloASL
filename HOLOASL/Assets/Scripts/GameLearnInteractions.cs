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
        if (FocusedObject.tag == "Back") {
            SceneManager.LoadScene("Cards");
            //TODO(@kourt: test on headset)
        }
        else if (FocusedObject.tag == "Replay") {
            GameLearnController.Instance.PlayAnimation();
            //TODO(@kourt: test on headset)
        }
        else if (FocusedObject.tag == "Pass") {
            GameLearnController.Instance.UpdateScore(1);
            GameLearnController.Instance.LoadNext();
            //TODO(@kourt: test on headset)
        }
        else if (FocusedObject.tag == "Fail") {
            GameLearnController.Instance.UpdateScore(0);
            GameLearnController.Instance.LoadNext();
            //TODO(@kourt: test on headset)
        }
        else if (FocusedObject.tag == "Faster") {
            GameLearnController.Instance.SpeedUpAnimation();
            //TODO(@nuan: test on headset)
        }
        else if (FocusedObject.tag == "Slower") {
            GameLearnController.Instance.SlowDownAnimation();
            //TODO(@nuan: test on headset)
        }
        else if (FocusedObject.tag == "FullSpeed")
        {
            GameLearnController.Instance.FullSpeed();
            //TODO(@nuan: test on headset)
        }
        else if (FocusedObject.tag == "HalfSpeed")
        {
            GameLearnController.Instance.HalfSpeed();
            //TODO(@nuan: test on headset)
        }
        else if (FocusedObject.tag == "QuarterSpeed")
        {
            GameLearnController.Instance.QuarterSpeed();
            //TODO(@nuan: test on headset)
        }
    }
}

