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
}
