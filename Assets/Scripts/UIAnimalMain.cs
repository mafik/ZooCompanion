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
    public string donateAddress;
    public PanelAnims anims;
    public Followers followers;

    AnimalPen pen;
    AnimalContext context => pen.context;

    private void Awake()
    {
        outsideLinkBtn.onClick.AddListener(OutsideLinkBtnClicked);
        donateBtn.onClick.AddListener(DonateBtnClicked);
        lectorBtn.onClick.AddListener(LectorBtnClicked);
        visitedToggle.onValueChanged.AddListener(OnVisitedToggleToggled);
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
        Application.OpenURL(donateAddress);
    }

    void LectorBtnClicked()
    {
        lectorSource.clip = pen.voiceClip;
        lectorSource.Play();
    }

    void OnVisitedToggleToggled(bool newValue)
    {
        followers.AddFollower(pen.animalMesh);
        Close();
    }
}
