using UnityEngine;
using UnityEngine.XR.WSA.Input;
using UnityEngine.SceneManagement;
public class CardSelector : GazeInput
{

    private GestureRecognizer recognizer;
    //public float ForceMagnitude = 300f;
    private int number_unlocked;
    private AudioSource audio_apple, audio_baseball, audio_cat, audio_dog, audio_elephant, audio_fire;

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

        audio_apple = GameObject.Find("audio_apple").GetComponent<AudioSource>();
        audio_baseball = GameObject.Find("audio_baseball").GetComponent<AudioSource>();
        audio_cat = GameObject.Find("audio_cat").GetComponent<AudioSource>();
        audio_dog = GameObject.Find("audio_dog").GetComponent<AudioSource>();
        audio_elephant = GameObject.Find("audio_elephant").GetComponent<AudioSource>();
        audio_fire = GameObject.Find("audio_fire").GetComponent<AudioSource>();
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
                audio_apple.Play();
                DestroyCursor(0);
                SceneManager.LoadScene("GameLearn");
                GameLearnController.curr_vocab_idx = 0;
            }

            else if (FocusedObject.tag == "sel_baseball")
            {
                audio_baseball.Play();
                DestroyCursor(0);
                SceneManager.LoadScene("GameLearn");
                GameLearnController.curr_vocab_idx = 1;
            }

            else if (FocusedObject.tag == "sel_cat")
            {
                audio_cat.Play();
                DestroyCursor(0);
                SceneManager.LoadScene("GameLearn");
                GameLearnController.curr_vocab_idx = 2;
            }

            else if (FocusedObject.tag == "sel_dog")
            {
                audio_dog.Play();
                DestroyCursor(0);
                SceneManager.LoadScene("GameLearn");
                GameLearnController.curr_vocab_idx = 3;
            }

            else if (FocusedObject.tag == "sel_elephant")
            {
                audio_elephant.Play();
                DestroyCursor(0);
                SceneManager.LoadScene("GameLearn");
                GameLearnController.curr_vocab_idx = 4;
            }

            else if (FocusedObject.tag == "sel_fire")
            {
                audio_fire.Play();
                DestroyCursor(0);
                SceneManager.LoadScene("GameLearn");
                GameLearnController.curr_vocab_idx = 5;
            }

            else if (FocusedObject.tag == "Back")
            {
                DestroyCursor(0);
                SceneManager.LoadScene("MainScene");
            }

            else if (FocusedObject.tag == "hide_cartoons")
            {
                GameObject.Find("Apple").GetComponentInChildren<MeshRenderer>().enabled = false;
                GameObject.Find("Baseball").GetComponentInChildren<MeshRenderer>().enabled = false;
                GameObject.Find("Cat").GetComponentInChildren<MeshRenderer>().enabled = false;
                GameObject.Find("Dog").GetComponentInChildren<MeshRenderer>().enabled = false;
                GameObject.Find("Elephant").GetComponentInChildren<MeshRenderer>().enabled = false;
                GameObject.Find("Fire").GetComponentInChildren<MeshRenderer>().enabled = false;
            }

            else if (FocusedObject.tag == "show_cartoons")
            {
                GameObject.Find("Apple").GetComponentInChildren<MeshRenderer>().enabled = true;
                GameObject.Find("Baseball").GetComponentInChildren<MeshRenderer>().enabled = true;
                GameObject.Find("Cat").GetComponentInChildren<MeshRenderer>().enabled = true;
                GameObject.Find("Dog").GetComponentInChildren<MeshRenderer>().enabled = true;
                GameObject.Find("Elephant").GetComponentInChildren<MeshRenderer>().enabled = true;
                GameObject.Find("Fire").GetComponentInChildren<MeshRenderer>().enabled = true;
            }
        }
    }

    // Update is called once per frame
    internal override void Update()
    {
        base.Update();

        if (Input.GetKeyDown("space")) // DEBUGGING ONLY
        {
            audio_apple.Play();
            DestroyCursor(0);
            SceneManager.LoadScene("GameLearn");
            GameLearnController.curr_vocab_idx = 0;
        }
    }
    
}