using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFacingController : MonoBehaviour {
    public float FacingAngle;

    void Update() {
        Vector3 mouseLoc = Input.mousePosition;
        Vector3 target = Camera.main.ScreenToWorldPoint(mouseLoc);
        FacingAngle = (Mathf.Atan2(target.y - transform.position.y, target.x - transform.position.x) * 180 / Mathf.PI);
    }
}
