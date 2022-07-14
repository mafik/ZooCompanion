using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AnimalPen : MonoBehaviour
{
    public Transform panelOpenPosition;
    public TextAsset rawAnimalData;
    public TextAsset rawQuestionData;
    public Sprite sprite;
    public Sprite map;
    public LineRenderer lr;
    public GameObject animalMesh;
    public AudioClip voiceClip;

    [HideInInspector]
    public AnimalContext context;

    [HideInInspector]
    public Question question;

    private void Start()
    {
        context = AnimalCsv.ParseAnimal(rawAnimalData);
        question = AnimalCsv.ParseQuestion(rawQuestionData);
    }

    public void ColorPen()
    {
        lr.startColor = lr.endColor = new Color(0.3545746f, 0.9056604f, 0.372496f);
    }
}
