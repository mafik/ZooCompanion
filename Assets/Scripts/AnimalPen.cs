using UnityEngine;
using UnityEngine.UI;

public class AnimalPen : MonoBehaviour
{
    public Transform panelOpenPosition;
    public TextAsset rawAnimalData;
    public Sprite sprite;
    public LineRenderer lr;
    public GameObject animalMesh;
    public AudioClip voiceClip;

    [HideInInspector]
    public AnimalContext context;

    private void Start()
    {
        context = AnimalCsv.Parse(rawAnimalData);
    }
}
