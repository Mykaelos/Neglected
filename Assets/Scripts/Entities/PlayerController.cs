using UnityEngine;
using System.Collections;

public class PlayerController : AnimatedEntityController {
    private PlayerStatusController PlayerStatusController;
    private MouseFacingController MouseFacingController;

    [Header("Movement")]
    public float BaseSpeed = 180f;
    public float ExhaustionMinimumBonus = 0.4f;
    public float CurrentSpeed;


    void Start() {
        PlayerStatusController = GetComponent<PlayerStatusController>();
        MouseFacingController = GetComponent<MouseFacingController>();

        PlayerStatusController.LocalMessenger.On(PlayerStatusController.MESSAGE_DEATH, OnDeath);

        Camera.main.transform.position.Set(transform.position.x, transform.position.y, Camera.main.transform.position.z);
        Camera.main.transform.SetParent(transform, false);
    }

    void FixedUpdate() {
        UpdateMoveSpeed();
        UpdatePlayerMovement();

        FacingAngle = MouseFacingController.FacingAngle; // Not great, but it's a refactor for another day...
        UpdateAnimation();
    }

    void UpdateMoveSpeed() {
        float exhaustionBonus = Mathf.Lerp(ExhaustionMinimumBonus, 1f, LerpHelper.CurveToOneFastSlow(PlayerStatusController.EnergyFraction, 2));
        CurrentSpeed = BaseSpeed * exhaustionBonus;
    }

    void UpdatePlayerMovement() {
        float xMove = Mathf.Clamp(Input.GetAxis("Horizontal"), -1, 1);
        float yMove = Mathf.Clamp(Input.GetAxis("Vertical"), -1, 1);
        Vector3 movement = new Vector3(xMove, yMove, 0);
        movement.Normalize();

        Move(movement, CurrentSpeed);
    }

    void OnDeath(object[] args = null) {
        if (Camera.main != null) {
            Camera.main.transform.SetParent(null, true);
        }
    }
}
