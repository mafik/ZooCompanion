using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimalMain : MonoBehaviour
{
    public Image animalPicture;
    public Image endangermentImage;
    public Toggle visitedToggle;
    public Text animalName;
    public Text animalNameLatin;
    public Text description;
    public Button outsideLinkBtn;
    public Button donateBtn;
    public Button lectorBtn;
    public AudioSource lectorSource;
    public AudioClip winAudioClip;

    public PanelAnims anims;
    public Followers followers;
    public bool proximityCollectOnly;

    public GameObject mainSection;
    public UiAnimalQuiz quizSection;
    public UiAnimalDonate donateSection;

    AnimalPen pen;
    AnimalContext context => pen.context;

    private void Awake()
    {
        outsideLinkBtn.onClick.AddListener(OutsideLinkBtnClicked);
        donateBtn.onClick.AddListener(DonateBtnClicked);
        lectorBtn.onClick.AddListener(LectorBtnClicked);
        visitedToggle.onValueChanged.AddListener(OnQuizToggleToggled);
        quizSection.onClose += OnQuizClosed;
        donateSection.onClose += OnDonateClosed;
    }

    private void Update()
    {
        bool ok;

        if (followers.followers.Contains(pen.animalMesh))
            ok = false;
        else if (!proximityCollectOnly)
            ok = true;
        else
        {
            Vector3 target = followers.center.position;
            Vector3 follower = pen.animalMesh.transform.position;
            ok = Vector3.Distance(target, follower) < 1.0f;
        }

        visitedToggle.interactable = ok;
    }

    public void Open(AnimalPen pen)
    {
        this.pen = pen;
        animalPicture.sprite = pen.sprite;
        endangermentImage.sprite = context.endangermentSprite;
        animalName.text = context.name;
        animalNameLatin.text = context.latinName;
        description.text = context.description;

        transform.position = pen.panelOpenPosition.transform.position;
        transform.SetParent(pen.panelOpenPosition);
        anims.Open();
    }

    public void Close()
    {
        anims.Close();
        lectorSource.Stop();
    }

    void OutsideLinkBtnClicked()
    {
        Application.OpenURL(pen.context.outsideLink);
    }

    void DonateBtnClicked()
    {
        mainSection.SetActive(false);
        donateSection.Open();
    }

    void LectorBtnClicked()
    {
        lectorSource.clip = pen.voiceClip;
        lectorSource.Play();
    }

    void OnQuizToggleToggled(bool newValue)
    {
        mainSection.SetActive(false);
        quizSection.Open(pen.question);
    }

    void OnQuizClosed(bool success)
    {
        mainSection.SetActive(true);

        if (success)
        {
            pen.ColorPen();
            followers.AddFollower(pen.animalMesh);
            Close();
        }
    }

    void OnDonateClosed()
    {
        mainSection.SetActive(true);
    }
}
