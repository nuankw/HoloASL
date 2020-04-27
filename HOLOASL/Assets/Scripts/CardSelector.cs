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

        GameObject.Find("Apple").GetComponentInChildren<MeshRenderer>().enabled = false;
        GameObject.Find("Baseball").GetComponentInChildren<MeshRenderer>().enabled = false;
        GameObject.Find("Cat").GetComponentInChildren<MeshRenderer>().enabled = false;
        GameObject.Find("Dog").GetComponentInChildren<MeshRenderer>().enabled = false;
        GameObject.Find("Elephant").GetComponentInChildren<MeshRenderer>().enabled = false;
        GameObject.Find("Fire").GetComponentInChildren<MeshRenderer>().enabled = false;

        audio_apple = GameObject.Find("audio_apple").GetComponent<AudioSource>();
        audio_baseball = GameObject.Find("audio_baseball").GetComponent<AudioSource>();
        audio_cat = GameObject.Find("audio_cat").GetComponent<AudioSource>();
        audio_dog = GameObject.Find("audio_dog").GetComponent<AudioSource>();
        audio_elephant = GameObject.Find("audio_elephant").GetComponent<AudioSource>();
        audio_fire = GameObject.Find("audio_fire").GetComponent<AudioSource>();
    }

    private void hideAllCards() {
        GameObject.Find("Apple").transform.FindChild("whiteboard").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("Baseball").transform.FindChild("whiteboard").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("Cat").transform.FindChild("whiteboard").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("Dog").transform.FindChild("whiteboard").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("Elephant").transform.FindChild("whiteboard").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("Fire").transform.FindChild("whiteboard").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("Apple").transform.FindChild("text").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("Baseball").transform.FindChild("text").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("Cat").transform.FindChild("text").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("Dog").transform.FindChild("text").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("Elephant").transform.FindChild("text").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("Fire").transform.FindChild("text").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("Back").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("Back").transform.FindChild("text").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("Tap instruction").GetComponent<MeshRenderer>().enabled = false;
    }
    private async void ShootBall(TappedEventArgs obj)
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
                hideAllCards();
                GameObject.Find("Apple").GetComponentInChildren<MeshRenderer>().enabled = true;
                await Task.Delay(1000);
                DestroyCursor(0);
                SceneManager.LoadScene("GameLearn");
                GameLearnController.curr_vocab_idx = 0;
            }

            else if (FocusedObject.tag == "sel_baseball")
            {
                audio_baseball.Play();
                hideAllCards();
                GameObject.Find("Baseball").GetComponentInChildren<MeshRenderer>().enabled = true;
                await Task.Delay(1000);
                DestroyCursor(0);
                SceneManager.LoadScene("GameLearn");
                GameLearnController.curr_vocab_idx = 1;
            }

            else if (FocusedObject.tag == "sel_cat")
            {
                audio_cat.Play();
                hideAllCards();
                GameObject.Find("Cat").GetComponentInChildren<MeshRenderer>().enabled = true;
                await Task.Delay(1000);
                DestroyCursor(0);
                SceneManager.LoadScene("GameLearn");
                GameLearnController.curr_vocab_idx = 2;
            }

            else if (FocusedObject.tag == "sel_dog")
            {
                audio_dog.Play();
                hideAllCards();
                GameObject.Find("Dog").GetComponentInChildren<MeshRenderer>().enabled = true;
                await Task.Delay(1000);
                DestroyCursor(0);
                SceneManager.LoadScene("GameLearn");
                GameLearnController.curr_vocab_idx = 3;
            }

            else if (FocusedObject.tag == "sel_elephant")
            {
                audio_elephant.Play();
                hideAllCards();
                GameObject.Find("Elephant").GetComponentInChildren<MeshRenderer>().enabled = true;
                await Task.Delay(1000);
                DestroyCursor(0);
                SceneManager.LoadScene("GameLearn");
                GameLearnController.curr_vocab_idx = 4;
            }

            else if (FocusedObject.tag == "sel_fire")
            {
                audio_fire.Play();
                hideAllCards();
                GameObject.Find("Fire").GetComponentInChildren<MeshRenderer>().enabled = true;
                await Task.Delay(1000);
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
            // audio_apple.Play();
            // DestroyCursor(0);
            // SceneManager.LoadScene("GameLearn");
            // GameLearnController.curr_vocab_idx = 0;
            audio_fire.Play();
            hideAllCards();
            GameObject.Find("Fire").GetComponentInChildren<MeshRenderer>().enabled = true;
        }
        if (Input.GetKeyDown("r")) // DEBUGGING ONLY
        {
            SceneManager.LoadScene("Cards");
        }
    }
    
}