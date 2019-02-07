using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeCollision : MonoBehaviour, ICustomCookieCollision
{
    public Vector2 CalculateResultingVelocity(CustomCookiePhysics myObject, CustomCookiePhysics otherObject, Collision2D collision)
    {
        // Kula stöter mot kula (wikipedia)
        // Centripetal kraft -> Rotations hastighet

        //m1u1 + m2u2 = m1v1 + m2v2
        // mV = mV_1 + mV_2  = Rörelse mängd

        // V^2 = V_1^2 + V_2^2
        // -> V^2 = (V_1 + V_2)^2
        // -> V_1^2 + V_2^2 = (V_1 + V_2)^2

        //    V_1^2 + V_2^2 = V_1^2 + 2*V1*V2 + V_2^2
        //    0 = 2*V_1*V_2
        //    0 = V_1*V_2

        // -> V_1 . V_2 = 0

        // a * b = ||a|| * ||b|| * cos(v)
        // Om skalärprodukten av två nollskilda vektorer a och b är noll måste cos(v) vara noll,
        // det vill säga vektorerna a och b är vinkelräta mot varandra.



        //return newVelocity;
        return Vector2.zero;
    }
}
