using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

public class GameLearnController : MonoBehaviour {
    public TextMesh unlock_num_text_obj;
    public TextMesh curr_label_obj;
    internal Animator curr_animator_obj;
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
        0.035f,
        0.03f,
        0.048f,
        1.0f 
    };
    public bool[] object_unlocked = new bool[] {false, false, false, false, false, false};
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
        GameObject currentObject = GameObject.FindGameObjectWithTag("actualObject");
        currentObject.transform.Rotate(0, 50 * Time.deltaTime, 0);
    }

    // ===============================================================
    public void LoadAll() {
        curr_vocab_idx = Math.Abs(curr_vocab_idx % animations_list.Length); // just to play safe
        Load_Object();
        Play_Animation(800);
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
        unlock_num_text_obj.text = "U n l o c k e d: " + n_unlocked + " / " + animations_list.Length;
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
        newObject.transform.position = new Vector3(-1.5f, -1.7f, -1f);
        float scale = objects_scale[curr_vocab_idx];
        newObject.transform.localScale = new Vector3(scale, scale, scale);
        newObject.name = animations_list[curr_vocab_idx];
        newObject.tag = "actualObject";
    }

    public int Unlock_Current_Vocab() {
        Debug.Log(curr_vocab_idx);
        Debug.Log(object_unlocked[curr_vocab_idx]);

        if (object_unlocked[curr_vocab_idx] == false) {
            Debug.Log("updating n_unlocked");
            n_unlocked += 1;
            object_unlocked[curr_vocab_idx] = true;
            Debug.Log(object_unlocked[curr_vocab_idx]);
            Update_Score_UI();
        }
        
        return n_unlocked;
    }

    public void Play_at_Full_Speed() {
        animator_speed = 1.0f;
        Play_Animation(1000);
    }

    public void Play_at_Half_Speed() {
        animator_speed = 0.5f;
        Play_Animation(800);
    }

    public void Play_at_Quarter_Speed() {
        animator_speed = 0.25f;
        Play_Animation(400);
    }

    public void Done(string name)
    {
        return;
    }
}