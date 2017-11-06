using UnityEngine;
using System.Collections.Generic;
using System;

public class DirectionalSpriteAnimator : SpriteAnimator {
    public DirectionalAnimationDirection CurrentDirection = DirectionalAnimationDirection.Up;
    public string CurrentAnimation;

    public List<DirectionalAnimation> Animations = new List<DirectionalAnimation>(8);


    protected override void Start() {
        SetAnimation(Animations[0]);

        base.Start();
    }

    public void ChangeAnimation(string animationName, DirectionalAnimationDirection direction) {
        if (animationName.Equals(CurrentAnimation) && direction == CurrentDirection) {
            return; // No need to change when everything is the same.
        }

        var nextAnimation = Find(animationName, direction);
        if (nextAnimation == null) {
            Debug.Log("{0} Could not find animation {1} {2}".FormatWith(this.gameObject.name, animationName, direction).Color("red"));
            return;
        }

        SetAnimation(nextAnimation);
    }

    public void SetAnimation(DirectionalAnimation animation) {
        Sprites = animation.Sprites;
        CurrentDirection = animation.Direction;
        CurrentAnimation = animation.Name;
        Restart();
    }

    public DirectionalAnimation Find(string name, DirectionalAnimationDirection direction) {
        for (int i = 0; i < Animations.Count; i++) {
            if (Animations[i].Name.Equals(name) && Animations[i].Direction == direction) {
                return Animations[i];
            }
        }

        return null;
    }
}

[Serializable]
public class DirectionalAnimation {
    public string Name;
    public DirectionalAnimationDirection Direction;
    public List<Sprite> Sprites = new List<Sprite>(2);
}

public enum DirectionalAnimationDirection {
    Up,
    Right,
    Down,
    Left,
    Any
}
