using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearnOrganizer : MonoBehaviour {

	//public TextMesh debugLog;

    /// <summary>
    /// Static instance of this class
    /// </summary>
    public static LearnOrganizer Instance;

    /// <summary>
    /// The 3D text representing the Bot response
    /// </summary>
    internal TextMesh botResponseText;
    // Use this for initialization
    /// <summary>
    /// Called on Initialization
    /// </summary>
    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Called immediately after Awake method
    /// </summary>
    void Start()
    {
		//debugLog.text = "Starting...";

        // Add the GazeInput class to this object
        //gameObject.AddComponent<GazeInput>();

        // Add the Interactions class to this object
        //gameObject.AddComponent<Interactions>();

        // Create the Bot in the scene
        CreateBotInScene();
        
    }

    /// <summary>
    /// Create the Sign In button object in the scene
    /// and sets its properties
    /// </summary>
    private void CreateBotInScene()
    {
        GameObject botObjInScene = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        botObjInScene.name = "Bot";

        // Add the Bot class to the Bot GameObject
        botObjInScene.AddComponent<LearnBot>();
        botObjInScene.AddComponent<HoloToolkit.Unity.Billboard>();

        // Create the Bot UI
        botResponseText = CreateBotResponseText();

        // Set properties of Bot GameObject
        LearnBot.Instance.botMaterial = new Material(Shader.Find("Diffuse"));
        botObjInScene.GetComponent<Renderer>().material = LearnBot.Instance.botMaterial;
        LearnBot.Instance.botMaterial.color = Color.blue;
        botObjInScene.transform.position = new Vector3(0f, 2f, 10f);
        botObjInScene.tag = "TextBotTag";
        botObjInScene.GetComponent<Renderer>().enabled = false;
        botObjInScene.GetComponent<Collider>().enabled = false;


    }

    /// <summary>
    /// Spawns cursor for the Main Camera
    /// </summary>
    private TextMesh CreateBotResponseText()
    {
        // Create a sphere as new cursor
        GameObject textObject = new GameObject();
        textObject.name = "botResponseText";
        textObject.transform.parent = LearnBot.Instance.transform;
        textObject.transform.localPosition = new Vector3(0, 1, 0);

        // Resize the new cursor
        textObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        // Creating the text of the Label
        TextMesh textMesh = textObject.AddComponent<TextMesh>();
        textMesh.anchor = TextAnchor.MiddleCenter;
        textMesh.alignment = TextAlignment.Center;
        textMesh.fontSize = 35;
        textMesh.text = "Welcome to ASL training mode. \nAfter the scanning is complete, \n try tapping on some horizontal surface\n and your trainer will appear there!";
        textMesh.text.Replace("\\n", "\n");
        return textMesh;
    }

    // Update is called once per frame
    void Update () {

		

	}
}
