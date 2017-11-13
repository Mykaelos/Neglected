using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveController : MonoBehaviour {
    private LivingEntityController LivingEntityController;

    public float HealRate = 1;

    public float HealthRegen = 0;

    public float EnergyRegen = 0;
    public float EnergyDrain = 0;

    public float WeathRegen = 0;
    public float WeathDrain = 0;


    public float HappinessRegen = 0;
    public float FulfillmentRegen = 0;

    private Text Name;
    private CanvasGroup Group;
    private Light Light;


    void Awake() {
        Name = transform.GetComponentInChildren<Text>();
        Name.text = gameObject.name;

        Group = transform.GetComponentInChildren<CanvasGroup>();
        Group.SetVisible(false, false);

        LivingEntityController = GetComponent<LivingEntityController>();
        LivingEntityController.LocalMessenger.On(LivingEntityController.MESSAGE_DEATH, OnDeath);


        Light = GetComponentInChildren<Light>();
    }

    void Update() {
        UpdateLight();
    }

    void UpdateLight() {
        Light.spotAngle = Mathf.Lerp(15, 45, LerpHelper.CurveToOneFastSlow(LivingEntityController.HealthFraction, 2));
        Light.intensity = Mathf.Lerp(1, 5, LerpHelper.CurveToOneFastSlow(LivingEntityController.HealthFraction, 2));
    }

    void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.tag.Equals("Player")) {
            var statusController = other.gameObject.GetComponent<PlayerStatusController>();

            if (statusController.Energy >= EnergyDrain * Time.deltaTime && statusController.Wealth >= WeathDrain * Time.deltaTime) {
                statusController.Heal(HealthRegen * Time.deltaTime);

                statusController.Energy += EnergyRegen * Time.deltaTime;
                statusController.Energy -= EnergyDrain * Time.deltaTime;

                statusController.Wealth += WeathRegen * Time.deltaTime * statusController.WealthGenerationPercentBonus;
                statusController.Wealth -= WeathDrain * Time.deltaTime;

                statusController.Happiness += HappinessRegen * Time.deltaTime;
                statusController.Fulfillment += FulfillmentRegen * Time.deltaTime;

                LivingEntityController.Heal(HealRate * Time.deltaTime);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider) { //for circle area to show text
        if (collider.gameObject.tag.Equals("Player")) {
            Group.SetVisible(true, false);
        }
    }

    void OnTriggerExit2D(Collider2D collider) { //for circle area to hide text
        if (collider.gameObject.tag.Equals("Player")) {
            Group.SetVisible(false, false);
        }
    }

    void OnDeath(object[] args = null) {
        string connector = gameObject.name.EndsWith("s") ? "are" : "is";
        string message = "Your {0} {1} gone.".FormatWith(gameObject.name, connector);

        MessageController.PostMessage(message);
    }
}
