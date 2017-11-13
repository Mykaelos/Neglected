using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlaySceneController : MonoBehaviour {

    void Start() {
        TransitionManager.FadeIn(1f, Color.black);

        var playerStatManager = new PlayerStatManager();

        GameObjectM.GetComponentOnObject<PlayerController>("Player").Setup(playerStatManager);

        var PlayerStatusController = GameObjectM.GetComponentOnObject<PlayerStatusController>("Player");
        GameObjectM.GetComponentOnObject<GuiPanelController>("GuiPanel").Setup(playerStatManager, PlayerStatusController);
    }
}
