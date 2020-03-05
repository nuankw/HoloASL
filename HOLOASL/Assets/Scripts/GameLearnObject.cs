using Newtonsoft.Json;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Windows.Speech;


public class GameLearnObject : MonoBehaviour
{

    public static GameLearnObject Instance;

    internal Material glObjectMaterial;

    private string glObjectId = "MRBotId";

    private string glObjectName = "MRBotName";

    private string glObjectSecret = "bAqYDX3mLFs.cwA.stY.hamE2NMAZ4T8ixwYUmgFZgK5keYI3zuanRcIT4o14xI";

    void Awake()
    {
        Instance = this;
    }

    private TextMesh CreateScoreText()
    {
        // Create a sphere as new cursor
        GameObject textObject = new GameObject();
        textObject.name = "Score";
        textObject.transform.parent = Instance.transform;
        textObject.transform.localPosition = new Vector3(0, 1, 0);

        // Resize the new cursor
        textObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        // Creating the text of the Label
        TextMesh textMesh = textObject.AddComponent<TextMesh>();
        textMesh.anchor = TextAnchor.MiddleCenter;
        textMesh.alignment = TextAlignment.Center;
        textMesh.fontSize = 35;
        textMesh.text = "0 / 0";
        return textMesh;
    }

}

