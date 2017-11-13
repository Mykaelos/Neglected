using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusController : LivingEntityController {
    public static Color HealthColor = "a80000".HexAsColor();
    public static Color EnergyColor = "a8a800".HexAsColor();
    public static Color FulfillmentColor = "a800a8".HexAsColor();
    public static Color HappinessColor = "0000a8".HexAsColor();
    public static Color WealthColor = "00a800".HexAsColor();


    [Header("Energy")]
    public float EnergyMax = 100;
    private float _Energy = 100;
    public float Energy {
        get { return _Energy; }
        set {
            _Energy = Mathf.Clamp(value, 0, EnergyMax);
        }
    }
    public float EnergyFraction {
        get { return Energy / EnergyMax; }
    }

    [Header("Wealth")]
    public float WealthMax = 100;
    private float _Wealth = 0;
    public float Wealth {
        get { return _Wealth; }
        set {
            _Wealth = Mathf.Clamp(value, 0, WealthMax);
        }
    }
    public float WealthFraction {
        get { return Wealth / WealthMax; }
    }
    public float WealthGenerationPercentBonus = 1;


    [Header("Happiness")]
    public float HappinessMax = 100;
    private float _Happiness = 0;
    public float Happiness {
        get { return _Happiness; }
        set {
            _Happiness = Mathf.Clamp(value, 0, HappinessMax);
        }
    }
    public float HappinessFraction {
        get { return Happiness / HappinessMax; }
    }

    [Header("Fulfillment")]
    public float FulfillmentMax = 100;
    private float _Fulfillment = 0;
    public float Fulfillment {
        get { return _Fulfillment; }
        set {
            _Fulfillment = Mathf.Clamp(value, 0, FulfillmentMax);
        }
    }
    public float FulfillmentFraction {
        get { return Fulfillment / FulfillmentMax; }
    }

    public void Setup(PlayerStatManager playerStatManager) {
        HealthMax = 100 * (playerStatManager.Get("HEALTH_MAX_PERCENT") / 100f + 1f);
        Health = HealthMax;

        WealthGenerationPercentBonus = playerStatManager.Get("WEALTH_GENERATION_PERCENT") / 100f + 1f;
    }
}


/**
 * Health       - Red a80000
 * Energy       - Yellow a8a800
 * Wealth       - Green 00a800
 * 
 * //Love
 * Fulfillment  - Purple a800a8
 * Happiness    - Blue 0000a8
 * 
 * 
 * Rest     = +Energy
 * Work     = -Energy +Wealth 
 * Spouse   = -Energy -Wealth +Happiness +Fulfillment 
 * Child    = -Energy -Wealth +Happiness +Fulfillment 
 * Hobby    = -Energy +Happiness +Fulfillment 
 * Friends  = -Energy +Happiness +Fulfillment 
 * Family   = -Energy +Happiness +Fulfillment 
 */
