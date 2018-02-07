using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorMaths
{
    public static void RigidbodyXVelocity(Rigidbody rb, Vector3 xDir, float xMagnitude)
    {
        xDir.y = 0.0f;
        xDir.z = 0.0f;
        xDir = xDir.normalized * xMagnitude; //Only interested in x value

        Vector3 currentVelocity = rb.velocity;

        currentVelocity.x = xDir.x;

        rb.velocity = currentVelocity;
    }
}
