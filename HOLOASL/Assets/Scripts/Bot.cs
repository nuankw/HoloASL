using Newtonsoft.Json;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Windows.Speech;


public class Bot : MonoBehaviour
{

    /// <summary>
    /// Static instance of this class
    /// </summary>
    public static Bot Instance;

    /// <summary>
    /// Material of the sphere representing the Bot in the scene
    /// </summary>
    internal Material botMaterial;

    /// <summary>
    /// Speech recognizer class reference, which will convert speech to text.
    /// </summary>
    private DictationRecognizer dictationRecognizer;

    /// <summary>
    /// Use this variable to identify the Bot Id
    /// Can be any value
    /// </summary>
    private string botId = "MRBotId";

    /// <summary>
    /// Use this variable to identify the Bot Name
    /// Can be any value
    /// </summary>
    private string botName = "MRBotName";

    /// <summary>
    /// The Bot Secret key found on the Web App Bot Service on the Azure Portal
    /// </summary>
    private string botSecret = "bAqYDX3mLFs.cwA.stY.hamE2NMAZ4T8ixwYUmgFZgK5keYI3zuanRcIT4o14xI";

    /// <summary>
    /// Bot Endpoint, v4 Framework uses v3 endpoint at this point in time
    /// </summary>
    private string botEndpoint = "https://directline.botframework.com/v3/directline";

    /// <summary>
    /// The conversation object reference
    /// </summary>
    private ConversationObject conversation;

    /// <summary>
    /// Bot states to regulate the application flow
    /// </summary>
    internal enum BotState { ReadyToListen, Listening, Processing }

    /// <summary>
    /// Flag for the Bot state
    /// </summary>
    internal BotState botState;

    /// <summary>
    /// Flag for the conversation status
    /// </summary>
    internal bool conversationStarted = false;
    // Use this for initialization
    /// <summary>
    /// Called on Initialization
    /// </summary>
    void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Called immediately after Awake method
    /// </summary>
    void Start()
    {
        botState = BotState.ReadyToListen;
    }
    public void placeBotAtGaze()
    {
        GameObject bot = GameObject.Find("Bot");
        GameObject cursor = GameObject.Find("Gaze0");
        GameObject botText = GameObject.Find("botResponseText");
        GameObject camera = GameObject.Find("Main Camera");
        GameObject backButton = GameObject.Find("Back");
        Vector3 gazePos = cursor.transform.position;
        Quaternion camRotation = camera.transform.rotation;
        Quaternion temp = new Quaternion(0, 1, 0, 0);
        Quaternion botRotation = temp * camRotation;
        //Quaternion botRotation = Quaternion.Euler(camRotation.eulerAngles.x, 180f - camRotation.eulerAngles.y, camRotation.eulerAngles.z);
        bot.transform.localScale = new Vector3(250, 250, 250);
        bot.transform.position = new Vector3(gazePos.x + 25, gazePos.y - 25, gazePos.z);
        bot.transform.rotation = new Quaternion(botRotation.x, botRotation.y, botRotation.z, botRotation.w);
        Quaternion targetRotation = Quaternion.LookRotation(-transform.forward, Vector3.up);
        //bot.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1f * Time.deltaTime);
        botText.transform.localScale = new Vector3(1, 1, 1);
        botText.transform.position = new Vector3(gazePos.x, gazePos.y + 25, gazePos.z);
        botText.transform.rotation = new Quaternion(camRotation.x, camRotation.y, camRotation.z, camRotation.w);
        backButton.transform.position = new Vector3(gazePos.x + 50, gazePos.y - 50, gazePos.z);
        backButton.transform.localScale = new Vector3(20, 10, 2);
        backButton.transform.rotation = new Quaternion(camRotation.x, camRotation.y, camRotation.z, camRotation.w);
    }

    /// <summary>
    /// Start microphone capture.
    /// </summary>
    public void StartCapturingAudio()
    {
        botState = BotState.Listening;

        // Start dictation
        dictationRecognizer = new DictationRecognizer();
        dictationRecognizer.DictationResult += DictationRecognizer_DictationResult;
        dictationRecognizer.Start();
    }


    /// <summary>
    /// Stop microphone capture.
    /// </summary>
    public void StopCapturingAudio()
    {
        botState = BotState.Processing;
        dictationRecognizer.Stop();
    }

    /// <summary>
    /// This handler is called every time the Dictation detects a pause in the speech. 
    /// </summary>
    private void DictationRecognizer_DictationResult(string text, ConfidenceLevel confidence)
    {
        // Update UI with dictation captured
        Debug.Log(string.Format("User just said: {0}", text));
        SetBotResponseText(text);
        AnimateGesturesForText(text);
        // Send dictation to Bot
        //StartCoroutine(SendMessageToBot(text, botId, botName, "message"));
        //StopCapturingAudio();
    }

    /// <summary>
    /// Request a conversation with the Bot Service
    /// </summary>
    internal IEnumerator StartConversation()
    {
        string conversationEndpoint = string.Format("{0}/conversations", botEndpoint);

        WWWForm webForm = new WWWForm();

        using (UnityWebRequest unityWebRequest = UnityWebRequest.Post(conversationEndpoint, webForm))
        {
            unityWebRequest.SetRequestHeader("Authorization", "Bearer " + botSecret);
            unityWebRequest.downloadHandler = new DownloadHandlerBuffer();

            yield return unityWebRequest.SendWebRequest();
            string jsonResponse = unityWebRequest.downloadHandler.text;

            conversation = new ConversationObject();
            conversation = JsonConvert.DeserializeObject<ConversationObject>(jsonResponse);
            Debug.Log(string.Format("Start Conversation - Id: {0}", conversation.ConversationId));
            conversationStarted = true;
        }

        // The following call is necessary to create and inject an activity of type //"conversationUpdate" to request a first "introduction" from the Bot Service.
        StartCoroutine(SendMessageToBot("", botId, botName, "conversationUpdate"));
    }

    /// <summary>
    /// Send the user message to the Bot Service in form of activity
    /// and call for a response
    /// </summary>
    private IEnumerator SendMessageToBot(string message, string fromId, string fromName, string activityType)
    {
        Debug.Log(string.Format("SendMessageCoroutine: {0}, message: {1} from Id: {2} from name: {3}", conversation.ConversationId, message, fromId, fromName));

        // Create a new activity here
        Activity activity = new Activity();
        activity.from = new From();
        activity.conversation = new Conversation();
        activity.from.id = fromId;
        activity.from.name = fromName;
        activity.text = message;
        activity.type = activityType;
        activity.channelId = "DirectLineChannelId";
        activity.conversation.id = conversation.ConversationId;

        // Serialize the activity
        string json = JsonConvert.SerializeObject(activity);

        string sendActivityEndpoint = string.Format("{0}/conversations/{1}/activities", botEndpoint, conversation.ConversationId);

        // Send the activity to the Bot
        using (UnityWebRequest www = new UnityWebRequest(sendActivityEndpoint, "POST"))
        {
            www.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));

            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Authorization", "Bearer " + botSecret);
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            // extrapolate the response Id used to keep track of the conversation
            string jsonResponse = www.downloadHandler.text;
            string cleanedJsonResponse = jsonResponse.Replace("\r\n", string.Empty);
            string responseConvId = cleanedJsonResponse.Substring(10, 30);

            // Request a response from the Bot Service
            StartCoroutine(GetResponseFromBot(activity));
        }
    }

    /// <summary>
    /// Request a response from the Bot by using a previously sent activity
    /// </summary>
    private IEnumerator GetResponseFromBot(Activity activity)
    {
        string getActivityEndpoint = string.Format("{0}/conversations/{1}/activities", botEndpoint, conversation.ConversationId);

        using (UnityWebRequest unityWebRequest1 = UnityWebRequest.Get(getActivityEndpoint))
        {
            unityWebRequest1.downloadHandler = new DownloadHandlerBuffer();
            unityWebRequest1.SetRequestHeader("Authorization", "Bearer " + botSecret);

            yield return unityWebRequest1.SendWebRequest();

            string jsonResponse = unityWebRequest1.downloadHandler.text;

            ActivitiesRootObject root = new ActivitiesRootObject();
            root = JsonConvert.DeserializeObject<ActivitiesRootObject>(jsonResponse);

            foreach (var act in root.activities)
            {
                Debug.Log(string.Format("Bot Response: {0}", act.text));
                SetBotResponseText(act.text);
            }



            botState = BotState.ReadyToListen;
            botMaterial.color = Color.blue;
        }
    }

    /// <summary>
    /// Set the UI Response Text of the bot
    /// </summary>
    internal void SetBotResponseText(string responseString)
    {
        SceneOrganiser.Instance.botResponseText.text = responseString;
    }
    internal void AnimateGesturesForText(string text)
    {
        //string[] words = text.Split(' ');
        //foreach (string word in words)
        //{
        GameAnimations.Instance.PlayAnimation(text);

        //}  
    }
    // Update is called once per frame
    void Update()
    {

    }
}
