using UnityEngine;
using System.Collections;

public class CreepController : AnimatedEntityController {
    public float WanderSpeed = 45;
    public float PursueSpeed = 90;
    public GameObject Target = null;
    public float Damage = 1;

    public CircleCollider2D TriggerCollider;
    AudioSource AudioSource;
    LivingEntityController LivingEntityController;


    protected override void Awake() {
        base.Awake();

        TriggerCollider = GetComponent<CircleCollider2D>();
        AudioSource = GetComponent<AudioSource>();
        LivingEntityController = GetComponent<LivingEntityController>();
    }

    void Start() {
        LivingEntityController.LocalMessenger.On(LivingEntityController.MESSAGE_INJURED, OnInjure);
    }

    public void Update() {
        if (Target != null) {
            PursueTarget();
        }
        else {
            MakeDecision();
            TakeAction();
        }

        UpdateAnimation();
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag.Equals("Player") || other.gameObject.tag.Equals("Objective")) {
            SetTarget(other.gameObject);
        }
    }

    public void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.tag.Equals("Player") || other.gameObject.tag.Equals("Objective")) {
            var entity = other.gameObject.GetComponent<LivingEntityController>();
            entity.Injure(Damage, gameObject);
        }
    }

    private void OnInjure(object[] args) {
        GameObject attacker = (GameObject)args[0];
        SetTarget(attacker);
    }

    public void SetTarget(GameObject newTarget) {
        if (Target == null) {
            //AudioManager.PlaySound("EnemySound", AudioSource, 0.7f, 0.25f);
        }

        Target = newTarget;
        LastSeenTime = Time.time;
    }

    public Decision[] Decisions = new Decision[] {
        new Decision(60, Action.Wait, 2),
        new Decision(10, Action.WanderLeft, 2),
        new Decision(10, Action.WanderRight, 2),
        new Decision(10, Action.WanderUp, 2),
        new Decision(10, Action.WanderDown, 2)
    };

    public float NextDecision = 2;
    public float LastDecision = 0;
    public Action CurrentAction = Action.Wait;

    public void MakeDecision() {
        if (LastDecision + NextDecision > Time.time) {
            return;
        }
        LastDecision = Time.time;

        float choice = Random.Range(0f, 100f);
        float runningPercent = 0;
        for (int i = 0; i < Decisions.Length; i++) {
            if (choice <= runningPercent + Decisions[i].Chance) {
                CurrentAction = Decisions[i].Action;
                NextDecision = Decisions[i].Duration;
                break;
            }
            runningPercent += Decisions[i].Chance;
        }

        //Debug.Log(this.name + " New Decision: " + CurrentAction);
    }

    public void TakeAction() {
        Vector3 movement = Vector3.zero;
        float speed = 0;

        if (CurrentAction == Action.WanderLeft) {
            movement.x -= 1;
            speed = WanderSpeed;
            FacingAngle = 180;
        }
        if (CurrentAction == Action.WanderRight) {
            movement.x += 1;
            speed = WanderSpeed;
            FacingAngle = 0;
        }
        if (CurrentAction == Action.WanderUp) {
            movement.y += 1;
            speed = WanderSpeed;
            FacingAngle = 90;
        }
        if (CurrentAction == Action.WanderDown) {
            movement.y -= 1;
            speed = WanderSpeed;
            FacingAngle = -90;
        }

        Move(movement, speed);
    }

    public float LastSeenTime = 0;
    public float LostTargetCutoff = 5;

    public void PursueTarget() {
        Vector3 direction = Target.transform.position - transform.position;
        if (Mathf.Abs(direction.magnitude) < TriggerCollider.radius) {
            LastSeenTime = Time.time;
        }

        Move(direction, PursueSpeed);
        FacingAngle = (Mathf.Atan2(Target.transform.position.y - transform.position.y, Target.transform.position.x - transform.position.x) * 180 / Mathf.PI);

        if (LastSeenTime + LostTargetCutoff < Time.time) {
            //Debug.Log(this.name + " lost " + Target.name);
            Target = null;
            LastSeenTime = 0;
        }
    }

    public enum Action {
        Wait,
        WanderLeft,
        WanderRight,
        WanderUp,
        WanderDown
    }

    public class Decision {
        public float Chance;
        public Action Action;
        public float Duration;

        public Decision(float chance, Action action, float duration) {
            Chance = chance;
            Action = action;
            Duration = duration;
        }
    }
}
