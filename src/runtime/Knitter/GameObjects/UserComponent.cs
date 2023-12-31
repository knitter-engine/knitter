﻿using System;

namespace Knitter.GameObjects;

public class UserComponent
{
    public virtual void OnCreate() { }
    public virtual void AfterCreate() { }
    public virtual void OnEnable() { }
    public virtual void BeforeUpdate() { }
    public virtual void Update() { }
    public virtual void AfterUpdate() { }
    public virtual void OnDisable() { }
    public virtual void BeforeDestroy() { }
    public virtual void OnDestroy() { }
    public virtual void FixedUpdate() { }
}
