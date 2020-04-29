using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;
using System.Threading.Tasks;

public class GameLearnController : MonoBehaviour {
    public TextMesh unlock_num_text_obj;
    public TextMesh curr_label_obj;
    public Animator curr_animator_obj;
    public static int curr_vocab_idx;
    public static int n_unlocked; // number of unlocked objects
    private float animator_speed = 1.0f;// Future: can change all following Array to ArrayList for easier future dynamic addition (back-end ish)
    String[] animations_list = new String[] {
        "Apple",
        "Baseball",
        "Cat",
        "Dog",
        "Elephant",
        "Fire"
    };
    float[] objects_scale = new float[] {
        0.1f,
        0.008f,
        0.03f,
        0.027f,
        0.3f,
        0.7f 
    };
    Vector3[] objects_pos = new Vector3[] {
        new Vector3(-1.3f, -2.1f, -1f),
        new Vector3(-1.3f, -2.1f, -1f),
        new Vector3(-1.6f, -1.5f, -1f),
        new Vector3(-1.5f, -1.7f, -1f),
        new Vector3(-1.7f, -1.8f, -1f),
        new Vector3(-1.4f, -1.9f, -1f)
    };
    public static bool[] object_unlocked = new bool[] {false, false, false, false, false, false};
    public static GameLearnController Instance;

    private void Awake() {
        Instance = this;
    }

    // Use this for initialization
    void Start() {
        // Add the GazeInput class to this object
        gameObject.AddComponent<GazeInput>();

        // Add the Interactions class to this object
        gameObject.AddComponent<GameLearnInteractions>();

        // Add the Animator object to this object
        curr_animator_obj = GetComponent<Animator>();

        // load objects
        LoadAll();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown("space")){ LoadAll(); }
        if (Input.GetKeyDown("1")){ Play_at_Full_Speed(); }
        if (Input.GetKeyDown("2")){ Play_at_Half_Speed(); }
        if (Input.GetKeyDown("4")){ Play_at_Quarter_Speed(); }
        if (Input.GetKeyDown("t")){
            Unlock_Current_Vocab();
            curr_vocab_idx += 1;
            LoadAll();
        }
        if (Input.GetKeyDown("r")) { SceneManager.LoadSceneAsync("Cards"); }
        if (Input.GetKeyDown("u")) { Unlock_Current_Vocab(); }
        
        GameObject currentObject = GameObject.FindGameObjectWithTag("actualObject");
        // if (animations_list[curr_vocab_idx] == "Elephant") { 
        //    GameObject.Find("default").transform.Rotate(0, 50 * Time.deltaTime, 0);
        // } else {
        currentObject.transform.Rotate(0, 50 * Time.deltaTime, 0);
        // }
    }

    // ===============================================================
    public async void LoadAll() {
        curr_vocab_idx = Math.Abs(curr_vocab_idx % animations_list.Length); // just to play safe
        Load_Object();
        // MissingReferenceException: The object of type 'Animator' has been destroyed but you are still trying to access it.
        // Your script should either check if it is null or you should not destroy the object.
        // but the following still won't work..
        // no auto play
        // if (GameObject.Find("Alphabet_animation").GetComponent<Animator>().GetType() == typeof(Animator)) { // handle async scene loading
        //     Debug.Log("found");
        //     Play_Animation(800);
        // }
    }

    public void Load_Object() {
        Load_Display_Obj_Name();
        Update_Score_UI();
        Load_Low_Poly_Object();
    }

    public async void Play_Animation(int wait_miliseconds=1000) {
        curr_animator_obj.speed = animator_speed;
        await Task.Delay(wait_miliseconds);
        curr_animator_obj.Play(animations_list[curr_vocab_idx]);
    }

    private void Load_Display_Obj_Name() {
        char[] anim_chars = animations_list[curr_vocab_idx].ToCharArray();
        curr_label_obj.text = String.Join(" ", anim_chars);
        curr_label_obj.color = new Color(230f / 255f, 230f / 255f, 230f / 255f);
        curr_label_obj.transform.position = new Vector3(-2.0f, 1f, -0.5f);
        curr_label_obj.transform.eulerAngles = new Vector3(0, 0, 0);
        curr_label_obj.fontSize = 500;
        curr_label_obj.fontStyle = FontStyle.Bold;
        curr_label_obj.transform.localScale = new Vector3(0.008f, 0.008f, 0.008f);
    }

    private void Update_Score_UI() {
        unlock_num_text_obj.text = "L e a r n e d: " + n_unlocked + " / " + animations_list.Length;
        unlock_num_text_obj.color = new Color(230f / 255f, 230f / 255f, 230f / 255f);
        unlock_num_text_obj.transform.position = new Vector3(-2.0f, 0.5f, -0.5f);
        unlock_num_text_obj.transform.eulerAngles = new Vector3(0, 0, 0);
    }

    private void Load_Low_Poly_Object() {
        if (GameObject.FindGameObjectWithTag("actualObject")) {
            GameObject old_obj = GameObject.FindGameObjectWithTag("actualObject");
            Destroy(old_obj);
        }
        GameObject newObject = Instantiate(Resources.Load(animations_list[curr_vocab_idx])) as GameObject;
        newObject.transform.position = objects_pos[curr_vocab_idx];
        float scale = objects_scale[curr_vocab_idx];
        newObject.transform.localScale = new Vector3(scale, scale, scale);
        newObject.name = animations_list[curr_vocab_idx];
        newObject.tag = "actualObject";
    }

    public bool[] Unlock_Current_Vocab() {
        Debug.Log(curr_vocab_idx);
        Debug.Log(object_unlocked[curr_vocab_idx]);

        if (object_unlocked[curr_vocab_idx] == false) {
            Debug.Log("updating n_unlocked");
            n_unlocked += 1;
            object_unlocked[curr_vocab_idx] = true;
            Debug.Log(object_unlocked[curr_vocab_idx]);
            Update_Score_UI();
        }
        
        return object_unlocked;
    }

    public void Play_at_Full_Speed() {
        animator_speed = 1.0f;
        Play_Animation(1000);
    }

    public void Play_at_Half_Speed() {
        animator_speed = 0.5f;
        Play_Animation(600);
    }

    public void Play_at_Quarter_Speed() {
        animator_speed = 0.25f;
        Play_Animation(400);
    }

    public void Done(string name)
    {
        // GameObject[] allGobj = GameObject.FindObjectsOfType<GameObject>();
        // List<String> nameList = new List<String>();
        // for (int i = 0; i < allGobj.Length; i++) {
        //     GameObject gameObj = allGobj[i];
        //     if (nameList.Contains(gameObj.name) && gameObj.name == "Gaze0"){
        //         Destroy(gameObj);
        //     } else {
        //         nameList.Add(gameObj.name);
        //     }
        // }
    }
}