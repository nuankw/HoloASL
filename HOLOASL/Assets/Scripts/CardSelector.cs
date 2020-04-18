using UnityEngine;
using UnityEngine.XR.WSA.Input;
using UnityEngine.SceneManagement;
public class CardSelector : GazeInput
{

    public GestureRecognizer recognizer;
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

            if (FocusedObject.tag == "sel_apple")
            {
                SceneManager.LoadScene("GameLearn");
                GameLearnController.currentAnimation = -1;
            }

            else if (FocusedObject.tag == "sel_baseball")
            {
                SceneManager.LoadScene("GameLearn");
                GameLearnController.currentAnimation = 0;
            }

            else if (FocusedObject.tag == "sel_cat")
            {
                SceneManager.LoadScene("GameLearn");
                GameLearnController.currentAnimation = 1;
            }

            else if (FocusedObject.tag == "sel_dog")
            {
                SceneManager.LoadScene("GameLearn");
                GameLearnController.currentAnimation = 2;
            }

            else if (FocusedObject.tag == "sel_elephant")
            {
                SceneManager.LoadScene("GameLearn");
                GameLearnController.currentAnimation = 3;
            }

            else if (FocusedObject.tag == "sel_fire")
            {
                SceneManager.LoadScene("GameLearn");
                GameLearnController.currentAnimation = 4;
            }

            else if (FocusedObject.tag == "Back")
            {
                SceneManager.LoadScene("MainScene");
            }
        }
    }

    // Update is called once per frame
    internal override void Update()
    {
        base.Update();

        if (Input.GetKeyDown("space")) // DEBUGGING ONLY
        {
            SceneManager.LoadScene("MainScene");
            // SceneManager.LoadScene("GameLearn");
            // GameLearnController.currentAnimation = -1; // offset by -1
            // Debug.Log("Scene Changed");
        }
    }
    
}