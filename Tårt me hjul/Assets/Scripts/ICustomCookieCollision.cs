using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICustomCookieCollision
{
    Vector2 CalculateResultingVelocity(CustomCookiePhysics myObject, CustomCookiePhysics otherObject, Collision2D collision);
}
