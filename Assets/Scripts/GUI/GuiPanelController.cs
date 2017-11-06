using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiPanelController : MonoBehaviour {
    private PlayerStatusController PlayerStatusController;

    Image HealthBar;
    Image EnergyBar;
    Image FulfillmentBar;
    Image HappinessBar;
    Image WealthBar;


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
    }

    void Start() {
        // Doing some dirty cheating here. I should be injecting this instead, but time is of the essense in a game jam...
        PlayerStatusController = GameObjectM.GetComponentOnObject<PlayerStatusController>("Player");

        if (PlayerStatusController == null) {
            GetComponent<CanvasGroup>().SetVisible(false);
        }
    }

    void Update() {
        UpdateHealthBar();
        UpdateEnergyBar();
        UpdateFulfillmentBar();
        UpdateHappinessBar();
        UpdateWealthBar();
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
}
