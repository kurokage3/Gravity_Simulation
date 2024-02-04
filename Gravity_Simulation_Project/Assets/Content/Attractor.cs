using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class simulates gravitational attraction between objects in Unity.
public class Attractor : MonoBehaviour
{
    #region Variables
    const float G = 667.4f; // Gravitational constant to scale gravitational force. G = 6.674×10-11 (m^3 / kg * s^2).

    // Holds all Attractor instances for global attraction calculations.
    public static List<Attractor> Attractors;

    // Rigidbody component for physics calculations.
    public Rigidbody rb;
    #endregion

    #region Methods
    // Apply gravitational attraction to all other Attractors every physics update.
    void FixedUpdate()
    {
        foreach (Attractor attractor in Attractors)
        {
            if (attractor != this) // Skip self-attraction.
            {
                Attract(attractor); 
            }
        }
    }

    // Calculate and apply gravitational force between this and another Attractor.
    void Attract(Attractor objToAttract)
    {
        // Access the Rigidbody of the object to attract.
        Rigidbody rbToAttract = objToAttract.rb;

        // Calculate the direction.
        Vector3 direction = rbToAttract.position - rb.position;

        // Calculate the distance between the two objects.
        float distance = direction.magnitude;

        if (distance == 0f)
        {
            return; // Prevent division by zero if objects are at the same position.
        }

        // Calculate the Force, F = G * (M1 * M2) / R^2
        float forceMagnitude = G * (rb.mass * rbToAttract.mass) / Mathf.Pow(distance, 2);

        // A force is just a magnitude and direction.
        Vector3 force = direction.normalized * forceMagnitude;

        rbToAttract.AddForce(-force); // Apply force to the other object.
    }

    // Add this Attractor to the global list when enabled.
    void OnEnable()
    {
        if (Attractors == null)
        {
            Attractors = new List<Attractor>();
        }
        Attractors.Add(this);
    }

    // Remove this Attractor from the global list when disabled.
    void OnDisable()
    {
        Attractors.Remove(this);
    }
    #endregion
}
