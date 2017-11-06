using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingController : MonoBehaviour {
    private PlayerStatusController PlayerStatusController;
    private MouseFacingController MouseFacingController;

    public GameObject BulletPrefab;
    private Transform SpawnPoint;

    public float EnergyCostPerShot = 1;
    public float Damage = 1;
    public float RefireDelay = 0;

    private float LastFire = 0;
    private bool HeldFire = false;


    void Start() {
        PlayerStatusController = GetComponent<PlayerStatusController>();
        MouseFacingController = GetComponent<MouseFacingController>();

        SpawnPoint = transform.Find("BulletSpawn");
    }

    void FixedUpdate() {
        HandleAttacking();
    }

    void HandleAttacking() {
        if (GUIUtility.hotControl == 0 && Input.GetMouseButton(0) || Input.GetAxis("Fire1") != 0) {
            Fire(SpawnPoint.position, MouseFacingController.FacingAngle, HeldFire);

            HeldFire = true;
        }
        else {
            HeldFire = false;
        }
    }

    public void Fire(Vector3 position, float angle, bool heldFire) {
        if (Time.timeSinceLevelLoad - LastFire >= RefireDelay || !heldFire) {
            if (PlayerStatusController.Energy > EnergyCostPerShot) {
                PlayerStatusController.Energy -= EnergyCostPerShot;
                LastFire = Time.timeSinceLevelLoad;

                SpawnBullet(position, angle);
            }
        }
    }

    public void SpawnBullet(Vector3 position, float angle) {
        GameObject bullet = (GameObject)GameObject.Instantiate(BulletPrefab, position, Quaternion.identity);
        bullet.transform.Rotate(Vector3.forward, angle);

        BulletController bulletController = bullet.GetComponent<BulletController>();
        bulletController.Shooter = gameObject;
        bulletController.Damage = Damage;
    }
}
