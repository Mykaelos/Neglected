using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiPanelController : MonoBehaviour {
    private PlayerStatusController PlayerStatusController;
    private PlayerStatManager PlayerStatManager;

    Image HealthBar;
    Image EnergyBar;
    Image FulfillmentBar;
    Image HappinessBar;
    Image WealthBar;

    Text AgeText;
    Text StatusText;


    public void Setup(PlayerStatManager playerStatManager, PlayerStatusController playerStatusController) {
        PlayerStatusController = playerStatusController;
        PlayerStatManager = playerStatManager;

        HealthBar.rectTransform.sizeDelta = new Vector2(PlayerStatusController.HealthMax, HealthBar.rectTransform.sizeDelta.y);
        EnergyBar.rectTransform.sizeDelta = new Vector2(PlayerStatusController.EnergyMax, EnergyBar.rectTransform.sizeDelta.y);

        UpdateStatusText();
    }

    void Awake() {
        HealthBar = transform.Find("Health/Image").GetComponent<Image>();
        HealthBar.color = PlayerStatusController.HealthColor;
        transform.Find("Health/Text").GetComponent<Text>().color = PlayerStatusController.HealthColor;

        EnergyBar = transform.Find("Energy/Image").GetComponent<Image>();
        EnergyBar.color = PlayerStatusController.EnergyColor;
        transform.Find("Energy/Text").GetComponent<Text>().color = PlayerStatusController.EnergyColor;

        FulfillmentBar = transform.Find("Fulfillment/Image").GetComponent<Image>();
        FulfillmentBar.color = PlayerStatusController.FulfillmentColor;
        transform.Find("Fulfillment/Text").GetComponent<Text>().color = PlayerStatusController.FulfillmentColor;

        HappinessBar = transform.Find("Happiness/Image").GetComponent<Image>();
        HappinessBar.color = PlayerStatusController.HappinessColor;
        transform.Find("Happiness/Text").GetComponent<Text>().color = PlayerStatusController.HappinessColor;

        WealthBar = transform.Find("Wealth/Image").GetComponent<Image>();
        WealthBar.color = PlayerStatusController.WealthColor;
        transform.Find("Wealth/Text").GetComponent<Text>().color = PlayerStatusController.WealthColor;

        AgeText = transform.Find("AgeText").GetComponent<Text>();
        StatusText = transform.Find("StatusText").GetComponent<Text>();
    }

    void Update() {
        if (PlayerStatusController != null) {
            UpdateHealthBar();
            UpdateEnergyBar();
            UpdateFulfillmentBar();
            UpdateHappinessBar();
            UpdateWealthBar();
            UpdateAgeText();
        }
    }

    void UpdateHealthBar() {
        HealthBar.fillAmount = PlayerStatusController.HealthFraction;
    }

    void UpdateEnergyBar() {
        EnergyBar.fillAmount = PlayerStatusController.EnergyFraction;
    }

    void UpdateFulfillmentBar() {
        FulfillmentBar.fillAmount = PlayerStatusController.FulfillmentFraction;
    }

    void UpdateHappinessBar() {
        HappinessBar.fillAmount = PlayerStatusController.HappinessFraction;
    }

    void UpdateWealthBar() {
        WealthBar.fillAmount = PlayerStatusController.WealthFraction;
    }

    void UpdateAgeText() {
        AgeText.text = "{0} years old".FormatWith(PlayerStatusController.Age.ToString("N1"));
    }

    void UpdateStatusText() {
        string text = "";

        foreach (var card in PlayerStatManager.CurrentCards) {
            text += card.Name + "\n";
        }

        StatusText.text = text;
    }
}
