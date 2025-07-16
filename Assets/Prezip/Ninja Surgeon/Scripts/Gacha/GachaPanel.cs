using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaPanel : MonoBehaviour
{
    [SerializeField] private GameObject _gachaPopUp;
    [SerializeField] private Text _descriptionText;
    [SerializeField] private Image _iconImage;
    [SerializeField] private Text _disclaimerText;
    [SerializeField] private Button _reclaimButton;
    [SerializeField] private string _defaultRewardDisclaimer = "You've already got this. Instead, you'll get 30 of Stamina.";

    void Start()
    {
        GachaSystem.Instance.OnActivation += DisplayGachaPanel;
    }

    public void DisplayGachaPanel(Item reward, Item defaultReward)
    {
        _gachaPopUp.SetActive(true);

        _descriptionText.text = reward.Name;

        _iconImage.sprite = reward.Icon;

        _disclaimerText.text = reward.CanRetrieve() ? "" : _defaultRewardDisclaimer;

        _reclaimButton.onClick.RemoveAllListeners();

        _reclaimButton.onClick.AddListener(ClosePopUp);

        _reclaimButton.onClick.AddListener(reward.GetRetrieved);
    }

    public void ClosePopUp()
    {
        _gachaPopUp.SetActive(false);
    }
}
