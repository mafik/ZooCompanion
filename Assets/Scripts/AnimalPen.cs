using UnityEngine;
using UnityEngine.UI;

public class AnimalPen : MonoBehaviour
{
    public Transform panelOpenPosition;
    public TextAsset rawAnimalData;
    public Sprite sprite;
    public Toggle toggle;

    [HideInInspector]
    public AnimalContext context;

    private void Start()
    {
        context = AnimalCsv.Parse(rawAnimalData);
    }
}
