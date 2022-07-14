using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalContext
{
    public string latinName;
    public string name;
    public string description;
    public string outsideLink;
    public Sprite endangermentSprite;
}

public class Question
{
    public string question;
    public string[] answers;
    public int answerId;
}

[Serializable]
class EndangermentClasses
{
    public string key;
    public Sprite classPicture;
}

public class AnimalCsv : MonoBehaviour
{
    [SerializeField]
    EndangermentClasses[] endangermentImages;

    static Dictionary<string, Sprite> namesToSprites = new Dictionary<string, Sprite>();

    void Awake()
    {
        namesToSprites.Clear();
        foreach (EndangermentClasses img in endangermentImages)
            namesToSprites.Add(img.key, img.classPicture);
    }

    static public AnimalContext ParseAnimal(TextAsset textAsset)
    {
        string text = textAsset.text;
        string[] lines = text.Split(';');
        for (int i = 0; i < lines.Length; ++i)
            lines[i] = lines[i].Trim();

        return new AnimalContext()
        {
            latinName = lines[0],
            name = lines[1],
            description = lines[2],
            endangermentSprite = namesToSprites[lines[3]],
            outsideLink = lines[4]
        };
    }

    static public Question ParseQuestion(TextAsset textAsset)
    {
        if (textAsset == null)
            return null;

        string text = textAsset.text;
        string[] lines = text.Split(';');
        for (int i = 0; i < lines.Length; ++i)
            lines[i] = lines[i].Trim();

        return new Question()
        {
            question = lines[0],
            answers = new string[]
            {
                lines[1],
                lines[2],
                lines[3],
            },
            answerId = int.Parse(lines[4])
        };
    }
}