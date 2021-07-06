using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Upgrade : ScriptableObject
{
    public string Title;
    public Sprite SpriteImage;

    public virtual void Activate(object data) { }
}