using UnityEngine;
using UnityEngine.XR.WSA.Input;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class CardSelector : GazeInput
{

    private GestureRecognizer recognizer;
    //public float ForceMagnitude = 300f;
    private int number_unlocked;
    private AudioSource audio_apple, audio_baseball, audio_cat, audio_dog, audio_elephant, audio_fire;
    private GameObject apple, baseball, cat, dog, elephant, fire;
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
        
        apple = GameObject.Find("Apple");
        baseball = GameObject.Find("Baseball");
        cat = GameObject.Find("Cat");
        dog = GameObject.Find("Dog");
        elephant = GameObject.Find("Elephant");
        fire = GameObject.Find("Fire");

        apple.GetComponentInChildren<MeshRenderer>().enabled = false;
        baseball.GetComponentInChildren<MeshRenderer>().enabled = false;
        cat.GetComponentInChildren<MeshRenderer>().enabled = false;
        dog.GetComponentInChildren<MeshRenderer>().enabled = false;
        elephant.GetComponentInChildren<MeshRenderer>().enabled = false;
        fire.GetComponentInChildren<MeshRenderer>().enabled = false;
        GameObject.Find("LoadingText").GetComponent<MeshRenderer>().enabled = false;

        audio_apple = GameObject.Find("audio_apple").GetComponent<AudioSource>();
        audio_baseball = GameObject.Find("audio_baseball").GetComponent<AudioSource>();
        audio_cat = GameObject.Find("audio_cat").GetComponent<AudioSource>();
        audio_dog = GameObject.Find("audio_dog").GetComponent<AudioSource>();
        audio_elephant = GameObject.Find("audio_elephant").GetComponent<AudioSource>();
        audio_fire = GameObject.Find("audio_fire").GetComponent<AudioSource>();
    }

    private void hideAllCards() {
        apple.transform.FindChild("whiteboard").GetComponent<MeshRenderer>().enabled = false;
        baseball.transform.FindChild("whiteboard").GetComponent<MeshRenderer>().enabled = false;
        cat.transform.FindChild("whiteboard").GetComponent<MeshRenderer>().enabled = false;
        dog.transform.FindChild("whiteboard").GetComponent<MeshRenderer>().enabled = false;
        elephant.transform.FindChild("whiteboard").GetComponent<MeshRenderer>().enabled = false;
        fire.transform.FindChild("whiteboard").GetComponent<MeshRenderer>().enabled = false;
        apple.transform.FindChild("text").GetComponent<MeshRenderer>().enabled = false;
        baseball.transform.FindChild("text").GetComponent<MeshRenderer>().enabled = false;
        cat.transform.FindChild("text").GetComponent<MeshRenderer>().enabled = false;
        dog.transform.FindChild("text").GetComponent<MeshRenderer>().enabled = false;
        elephant.transform.FindChild("text").GetComponent<MeshRenderer>().enabled = false;
        fire.transform.FindChild("text").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("Back").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("Back").transform.FindChild("text").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("Tap instruction").GetComponent<MeshRenderer>().enabled = false;
        
        GameObject.Find("LoadingText").GetComponent<MeshRenderer>().enabled = true;
    
    }
    private void ShootBall(TappedEventArgs obj)
    {

        Debug.Log("Focussed object is:" + FocusedObject.tag);
        // Initialize Raycasting.
        if (FocusedObject != null)
        {

            // apologize for the ugliness
            if (FocusedObject.tag == "sel_apple")
            {
                audio_apple.Play();
                hideAllCards();
                apple.GetComponentInChildren<MeshRenderer>().enabled = true;
                GameLearnController.curr_vocab_idx = 0;
                SceneManager.LoadSceneAsync("GameLearn");
            }

            else if (FocusedObject.tag == "sel_baseball")
            {
                audio_baseball.Play();
                hideAllCards();
                baseball.GetComponentInChildren<MeshRenderer>().enabled = true;
                GameLearnController.curr_vocab_idx = 1;
                SceneManager.LoadSceneAsync("GameLearn");
            }

            else if (FocusedObject.tag == "sel_cat")
            {
                audio_cat.Play();
                hideAllCards();
                cat.GetComponentInChildren<MeshRenderer>().enabled = true;
                GameLearnController.curr_vocab_idx = 2;
                SceneManager.LoadSceneAsync("GameLearn");
            }

            else if (FocusedObject.tag == "sel_dog")
            {
                audio_dog.Play();
                hideAllCards();
                dog.GetComponentInChildren<MeshRenderer>().enabled = true;
                GameLearnController.curr_vocab_idx = 3;
                SceneManager.LoadSceneAsync("GameLearn");
            }

            else if (FocusedObject.tag == "sel_elephant")
            {
                audio_elephant.Play();
                hideAllCards();
                elephant.GetComponentInChildren<MeshRenderer>().enabled = true;
                GameLearnController.curr_vocab_idx = 4;
                SceneManager.LoadSceneAsync("GameLearn");
            }

            else if (FocusedObject.tag == "sel_fire")
            {
                audio_fire.Play();
                hideAllCards();
                fire.GetComponentInChildren<MeshRenderer>().enabled = true;
                GameLearnController.curr_vocab_idx = 5;
                SceneManager.LoadSceneAsync("GameLearn");
            }

            else if (FocusedObject.tag == "Back")
            {
                SceneManager.LoadSceneAsync("MainScene");
            }
        }
    }

    // Update is called once per frame
    internal override void Update()
    {
        base.Update();

        if (Input.GetKeyDown("space")) // DEBUGGING ONLY
        {
            // audio_apple.Play();
            // DestroyCursor(0);
            // SceneManager.LoadSceneAsync("GameLearn");
            // GameLearnController.curr_vocab_idx = 0;
            audio_fire.Play();
            hideAllCards();
            // apple.GetComponentInChildren<MeshRenderer>().enabled = true;
            // baseball.GetComponentInChildren<MeshRenderer>().enabled = true;
            // cat.GetComponentInChildren<MeshRenderer>().enabled = true;
            // dog.GetComponentInChildren<MeshRenderer>().enabled = true;
            // elephant.GetComponentInChildren<MeshRenderer>().enabled = true;
            fire.GetComponentInChildren<MeshRenderer>().enabled = true;
            GameLearnController.curr_vocab_idx = 5;
            SceneManager.LoadSceneAsync("GameLearn");
        }
        if (Input.GetKeyDown("r")) // DEBUGGING ONLY
        {
            SceneManager.LoadSceneAsync("Cards");
        }
    }
    
}