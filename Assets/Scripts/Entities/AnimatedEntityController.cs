using UnityEngine;
using System.Collections;

public class AnimatedEntityController : MonoBehaviour {
    protected Rigidbody2D Rigidbody;
    protected DirectionalSpriteAnimator DirectionalSpriteAnimator;

    public float FacingAngle = 0;


    protected virtual void Awake() {
        Rigidbody = GetComponent<Rigidbody2D>();
        DirectionalSpriteAnimator = transform.Find("Sprite").GetComponent<DirectionalSpriteAnimator>();
    }

    protected void UpdateAnimation() {
        string animationName = Rigidbody.velocity.magnitude > 0 ? "moving" : "idle";
        DirectionalAnimationDirection direction = DetermineDirection(FacingAngle);

        DirectionalSpriteAnimator.ChangeAnimation(animationName, direction);
    }

    protected DirectionalAnimationDirection DetermineDirection(float angle) {
        if (-135 < angle && angle <= -45) {
            return DirectionalAnimationDirection.Down;
        }
        if (-45 < angle && angle <= 45) {
            return DirectionalAnimationDirection.Right;
        }
        if (45 < angle && angle <= 135) {
            return DirectionalAnimationDirection.Up;
        }
        return DirectionalAnimationDirection.Left;
    }

    public void Move(Vector3 direction, float speed) {
        direction.Normalize();

        Rigidbody.velocity = new Vector3(direction.x * Time.deltaTime * speed, direction.y * Time.deltaTime * speed, 0);
    }
}
