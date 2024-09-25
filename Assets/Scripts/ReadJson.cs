using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

using static GameController;

public class ReadJson 
{
    public static string fileName = "Assets/Json/data.json";
  
    public static Question[] createArrayOfQuestions()
    {
        string jsonstr = File.ReadAllText(fileName);
        return JsonUtility.FromJson<Questions>(jsonstr).questions;
    }

    [System.Serializable]
    public class Question
    {
        public string text;
        public string A, B, C, D, rightanswer;

    }

    [System.Serializable]
    public class Questions
    {
        public Question[] questions;

    }
}
