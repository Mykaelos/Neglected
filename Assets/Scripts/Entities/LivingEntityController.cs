using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntityController : MonoBehaviour {
    public static string MESSAGE_DEATH = "LivingEntityController.MESSAGE_DEATH";
    public static string MESSAGE_INJURED = "LivingEntityController.MESSAGE_INJURED";
    public LocalMessenger LocalMessenger = new LocalMessenger();

    [Header("Health")]
    public bool IsInvincible = false;
    public float HealthMax = 100;
    public float Health = 100;
    //public float Health {
    //    get { return _Health; }
    //    set {
    //        _Health = Mathf.Clamp(value, 0, HealthMax);

    //        if (!IsAlive()) {
    //            OnDeath();
    //        }
    //    }
    //}
    public float HealthFraction {
        get { return Health / HealthMax; }
    }


    public float Heal(float amount) {
        float healedHealth = Health + amount;
        float healRemaining = healedHealth > HealthMax ? healedHealth - HealthMax : 0;
        float healthHealed = amount - healRemaining;
        //Health += healthHealed;
        Health = MathM.MaximumClamped(Health + healthHealed, HealthMax);

        return healRemaining;
    }

    public virtual float Injure(float amount, GameObject Attacker) {
        if (IsInvincible) {
            return 0;
        }

        float overkillDamage = 0;
        if (amount > Health) {
            overkillDamage = amount - Health;
        }

        //Health -= amount;
        Health = MathM.MinimumClamped(Health - amount, 0);

        LocalMessenger.Fire(MESSAGE_INJURED, new object[] { Attacker });

        if (!IsAlive()) {
            OnDeath();
        }

        return overkillDamage;
    }

    public bool IsAlive() {
        return IsInvincible || Health > 0;
    }

    public virtual void OnDeath() {
        LocalMessenger.Fire(MESSAGE_DEATH);
        Destroy(this.gameObject);
    }
}
