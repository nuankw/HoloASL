using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameAnimations : MonoBehaviour
{


    public static GameAnimations Instance;


    internal Animator wordAnimation;
    private int idx = 0;
    int max_length = 0;
    ArrayList word_animation = new ArrayList();

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {


    }

    private Dictionary<string, string> LoadJSONData(string fileName)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        if (File.Exists(filePath))
        {
            string dataAsJSON = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(dataAsJSON);
        }
        return null;
    }

    private string[] GetExcludedWords(string fileName)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        if (File.Exists(filePath))
        {
            string[] excludedWords = File.ReadAllText(filePath).Split(',');
            return excludedWords;
        }
        return null;
    }

    public void PlayAnimation(string text)
    {

        string[] words = text.Split(' ');

        var wordToAnimMap = LoadJSONData("animationsMapping.json");
        string[] listOfExcludedWords = GetExcludedWords("exclusions.csv");

        foreach (string word in words)
        {
            String loweredWord = word.ToLower();
            //Fetch the Animator from GameObject
            wordAnimation = GetComponent<Animator>();
            int pos = 0;
            if (wordToAnimMap.ContainsKey(loweredWord))
            {
                //string animName = wordToAnimMap[word];
                //PlayGesture(animName);
                //word_animation[anim] = wordToAnimMap[word];
                word_animation.Add(wordToAnimMap[loweredWord]);
            }
            else if ((pos = Array.IndexOf(listOfExcludedWords, word)) <= -1)
            { //if the current word is not among excluded words
                //store the characters of the word in the list of animations
               
                foreach (char c in loweredWord)
                {
                    if (wordToAnimMap.ContainsKey(c + ""))
                    {
                        word_animation.Add(wordToAnimMap[c + ""]);
                    }
                }
            }
        }
        max_length = word_animation.Count;
        PlayGesture(0);



    }

    public void Done(string name)
    {
        idx++;
        if (idx == max_length)
        {
            idx = 0;
            max_length = 0;
            word_animation.Clear();

            return;
        }
        PlayGesture(idx);
    }

    public void PlayGesture(int ind)
    {
        if (ind == max_length)
        {
            return;
        }
        /*if (word_animation[ind].Equals("AlpL"))
        {
            wordAnimation.SetTrigger("Trigger1");
        }

        if (word_animation[ind].Equals("AlpF"))
        {
            wordAnimation.SetTrigger("Trigger2");
        }*/
        wordAnimation.SetTrigger(word_animation[ind].ToString());
    }

    // Update is called once per frame
    void Update()
    {


    }
}
