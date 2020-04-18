using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameLearnController : MonoBehaviour {
    public GameObject glObject;
    public TextMesh m_Score;
    public TextMesh m_CurrentObjectName;

    private GameObject parentObj;

    internal Animator m_Animator;
    public static int currentAnimation = -1;
    String[] animations_list = new String[] {"Apple","Baseball","Cat",  "Dog", "Elephant","Fire"};
    float[] objects_scale = new float[] {     0.1f,   0.008f,    0.035f, 0.03f,  0.048f,    1.0f };
    private int score = -1;
    private int attempted = -1;
    private int starting = 1;
    private float animator_speed = 1.0f;
    private bool animator_is_playing = false;
    public static GameLearnController Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start()
    {
        // Add the GazeInput class to this object
        gameObject.AddComponent<GazeInput>();

        // Add the Interactions class to this object
        gameObject.AddComponent<GameLearnInteractions>();

        // Add the Animator object to this object
        m_Animator = GetComponent<Animator>();

        // Get prent object for loading gamelean objects
        parentObj = GameObject.FindGameObjectWithTag("glObject");

        BeginGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("n"))
        {
            LoadNext();
        }
        if (Input.GetKeyDown("space"))
        {
            PlayAnimation();
        }
        if (Input.GetKeyDown("r"))
        {
            ResetScore();
        }
        if (Input.GetKeyDown("p"))
        {
            UpdateScore(1);
            LoadNext();
        }
        if (Input.GetKeyDown("f"))
        {
            UpdateScore(0);
            LoadNext();
        }
        if (Input.GetKeyDown("d"))
        {
            SlowDownAnimation();
        }
        if (Input.GetKeyDown("u"))
        {
            SpeedUpAnimation();
        }
        if (Input.GetKeyDown("1"))
        {
            FullSpeed();
        }
        if (Input.GetKeyDown("2"))
        {
            HalfSpeed();
        }
        GameObject currentObject = GameObject.FindGameObjectWithTag("actualObject");
        currentObject.transform.Rotate(0, 50 * Time.deltaTime, 0);
        PlayAnimation();
    }

    public void BeginGame()
    {
        UpdateScore(0 + starting);
        starting = 0;
        LoadNext();
    }

    public void LoadNext()
    {
        currentAnimation = (currentAnimation+1) % animations_list.Length; // TODO(@kourt: should we be looping?)
        UpdateObject();
        PlayAnimation();
    }

    public void UpdateScore(int point)
    {
        score += point;
        attempted += 1;
        UpdateScoreUI();
    }

    public void ResetScore()
    {
        score = 0;
        attempted = 0;
        UpdateScoreUI();
    }

    public void PlayAnimation()
    {
        m_Animator.speed = animator_speed;
        m_Animator.Play(animations_list[currentAnimation]);
    }

    public void SlowDownAnimation()
    {
        animator_speed = 0.8f * animator_speed;
        if (animator_speed < 0.1f) {
            animator_speed = 0.1f; 
        }
        PlayAnimation();
    }

    public void HalfSpeed()
    {
        animator_speed = 0.5f;
    }

    public void FullSpeed()
    {

        animator_speed = 1.0f;
    }

    public void SpeedUpAnimation()
    {
        animator_speed = 1.2f * animator_speed;
        if (animator_speed > 2.0f)
        {
            animator_speed = 2.0f;
        }
        PlayAnimation();
    }

    public void Done(string name)
    {
        return;
    }


    private void UpdateScoreUI()
    {
        m_Score.text = "Score: " + score + " / " + attempted;
        m_Score.transform.position = new Vector3(1f, 0.5f, -0.5f);
        m_Score.transform.eulerAngles = new Vector3(0, 10, 0);

    }

    private void UpdateObjectLabelUI()
    {
        char[] anim_chars = animations_list[currentAnimation].ToCharArray();
        m_CurrentObjectName.text = String.Join(" ", anim_chars);
        m_CurrentObjectName.transform.position = new Vector3(1f, 1f, -0.5f);
        m_CurrentObjectName.transform.eulerAngles = new Vector3(0, 10, 0);
        m_CurrentObjectName.fontSize = 500;
        m_CurrentObjectName.fontStyle = FontStyle.Bold;
        m_CurrentObjectName.transform.localScale = new Vector3(0.008f, 0.008f, 0.008f);
    }

    private void UpdateObjectUI()
    {
        if (starting == 0) {
            GameObject oldObject = GameObject.Find(animations_list[(currentAnimation - 1 + animations_list.Length) % animations_list.Length]);
            if (oldObject != null)
            {
                oldObject.SetActive(false);
                Destroy(oldObject);
            }
        }
        GameObject newObject = Instantiate(Resources.Load(animations_list[currentAnimation])) as GameObject;
        newObject.transform.position = new Vector3(2.0f, -2.0f, -1f);
        float scale = objects_scale[currentAnimation];
        newObject.transform.localScale = new Vector3(scale, scale, scale);
        newObject.name = animations_list[currentAnimation];
        newObject.tag = "actualObject";
        // newObject.transform.parent = parentObj.transform;
    }

    public void UpdateObject()
    {
        UpdateObjectLabelUI();
        UpdateObjectUI();
    }
}