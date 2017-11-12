using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlaySceneController : MonoBehaviour {

    void Start() {
        TransitionManager.FadeIn(1f, Color.black);
    }
}
