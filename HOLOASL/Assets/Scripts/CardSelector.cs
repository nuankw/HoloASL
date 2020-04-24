using UnityEngine;
using UnityEngine.XR.WSA.Input;
using UnityEngine.SceneManagement;
public class CardSelector : GazeInput
{

    private GestureRecognizer recognizer;
    //public float ForceMagnitude = 300f;
    private int number_unlocked;

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
                DestroyCursor(0);
                SceneManager.LoadScene("GameLearn");
                GameLearnController.curr_vocab_idx = 0;
            }

            else if (FocusedObject.tag == "sel_baseball")
            {
                DestroyCursor(0);
                SceneManager.LoadScene("GameLearn");
                GameLearnController.curr_vocab_idx = 1;
            }

            else if (FocusedObject.tag == "sel_cat")
            {
                DestroyCursor(0);
                SceneManager.LoadScene("GameLearn");
                GameLearnController.curr_vocab_idx = 2;
            }

            else if (FocusedObject.tag == "sel_dog")
            {
                DestroyCursor(0);
                SceneManager.LoadScene("GameLearn");
                GameLearnController.curr_vocab_idx = 3;
            }

            else if (FocusedObject.tag == "sel_elephant")
            {
                DestroyCursor(0);
                SceneManager.LoadScene("GameLearn");
                GameLearnController.curr_vocab_idx = 4;
            }

            else if (FocusedObject.tag == "sel_fire")
            {
                DestroyCursor(0);
                SceneManager.LoadScene("GameLearn");
                GameLearnController.curr_vocab_idx = 5;
            }

            else if (FocusedObject.tag == "Back")
            {
                DestroyCursor(0);
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