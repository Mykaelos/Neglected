using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageController : MonoBehaviour {
    private static MessageController Self;
    private Text MessageText;
    private CanvasGroup Group;
    private IEnumerator FadingCoroutine;


    void Awake() {
        Self = this;
        MessageText = GetComponent<Text>();
        Group = GetComponent<CanvasGroup>();

        MessageText.text = "";
        Group.SetVisible(false, false);
    }

    public static void PostMessage(string message) {
        Self.MessageText.text += (!Self.MessageText.text.IsNullOrEmpty() ? "\n" : "") + message;

        if (Self.FadingCoroutine != null) {
            Self.Group.StopFadeOut(Self.FadingCoroutine);
        }

        Self.Group.SetVisible(true, false);
        Self.FadingCoroutine = Self.Group.FadeOut(null, 3f, 1f, delegate {
            Self.MessageText.text = "";
        });
    }
}
