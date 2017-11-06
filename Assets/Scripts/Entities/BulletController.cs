using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {
    public float Duration = 2;
    public float Speed = 5000;
    public float Damage = 1;

    public GameObject Shooter;

    Rigidbody2D Rigidbody;
    AudioSource AudioSource;


    void Awake() {
        Rigidbody = GetComponent<Rigidbody2D>();
        //AudioSource = GetComponent<AudioSource>();
    }

    public void Start() {
        Rigidbody.AddForce(transform.right * Speed, ForceMode2D.Impulse);

        //AudioManager.PlaySound("Shot", AudioSource, 0.6f, 0.1f);
    }

    public void Update() {
        if ((Duration -= Time.deltaTime) < 0) {
            OnDestroy();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        LivingEntityController entity = collision.gameObject.GetComponent<LivingEntityController>();
        if (entity != null) {
            if (entity.IsAlive()) {
                entity.Injure(Damage, Shooter);
            }
        }

        OnDestroy();
    }

    public virtual void OnDestroy() {
        Destroy(this.gameObject);
    }
}
