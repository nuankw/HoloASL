using UnityEngine;
using UnityEngine.XR.WSA.Input;
using UnityEngine.SceneManagement;
public class Shoot : GazeInput
{

    private GestureRecognizer recognizer;
    //public float ForceMagnitude = 300f;

    // Use this for initialization
    internal override void Start()
    {

        //FocusedObject = null;
        base.Start();
        //Cursor.GetComponent<Renderer>().enabled = false;
        // Set up a GestureRecognizer to detect tap gestures.
        recognizer = new GestureRecognizer();
        recognizer.SetRecognizableGestures(GestureSettings.Tap);
        recognizer.Tapped += ShootBall;
        
        recognizer.StartCapturingGestures();
    }
    

    private void ShootBall(TappedEventArgs obj)
    {

        //Debug.Log("obj"+ obj);
        //Debug.Log("Renderer"+ GetComponent<Renderer>()+"Object"+ GetComponent<Renderer>());
        //Debug.Log("Focussed object is:" + FocusedObject.tag);
        // Initialize Raycasting.
        if (FocusedObject != null)
        {

            if (FocusedObject.tag == "Learn")
            {
                SceneManager.LoadScene("Learning");
            }

            else if (FocusedObject.tag == "Converse")
            {
                SceneManager.LoadScene("Converse");
            }

            else if (FocusedObject.tag == "Quit")
            {
                Application.Quit();
            }

            else if (FocusedObject.tag == "GameLearn")
            {
                base.DestroyCursor(0);
                SceneManager.LoadScene("Cards");
            }
        }
    }

    // Update is called once per frame
    internal override void Update()
    {
        base.Update();
    }
    
}