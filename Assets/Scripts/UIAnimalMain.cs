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
    public string donateAddress;

    AnimalPen pen;
    AnimalContext context => pen.context;

    private void Awake()
    {
        outsideLinkBtn.onClick.AddListener(OutsideLinkBtnClicked);
        donateBtn.onClick.AddListener(DonateBtnClicked);
        visitedToggle.onValueChanged.AddListener(OnVisitedToggleToggled);
    }

    public void Open(AnimalPen pen)
    {
        if (this.pen)
        {
            this.pen.toggle.onValueChanged.RemoveListener(OnPenToggleToggled);
        }

        this.pen = pen;
        animalPicture.sprite = pen.sprite;
        endangermentImage.sprite = context.endangermentSprite;
        animalName.text = context.name;
        animalNameLatin.text = context.latinName;
        description.text = context.description;
        pen.toggle.onValueChanged.AddListener(OnPenToggleToggled);
        visitedToggle.SetIsOnWithoutNotify(pen.toggle.isOn);

        transform.position = pen.panelOpenPosition.transform.position;
        transform.parent = pen.panelOpenPosition;
        gameObject.SetActive(true);
    }

    void OutsideLinkBtnClicked()
    {
        Application.OpenURL(pen.context.outsideLink);
    }

    void DonateBtnClicked()
    {
        Application.OpenURL(donateAddress);
    }

    void OnVisitedToggleToggled(bool newValue)
    {
        pen.toggle.SetIsOnWithoutNotify(newValue);
    }

    void OnPenToggleToggled(bool newValue)
    {
        visitedToggle.SetIsOnWithoutNotify(newValue);
    }
}
