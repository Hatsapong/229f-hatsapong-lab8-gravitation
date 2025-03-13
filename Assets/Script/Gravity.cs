using NUnit.Framework;
using System.Collections.Generic; //for List
using UnityEngine;

public class Gravity : MonoBehaviour
{
    Rigidbody rb;

    const float G = 0.00667f;

    public static List<Gravity> gravityObjectList;

    [SerializeField] bool planet = false;
    [SerializeField] int orbitSpeed = 1000;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (gravityObjectList == null)
            gravityObjectList = new List<Gravity>();


        gravityObjectList.Add( this );
    }

    private void FixedUpdate()
    {
        foreach (var obj in gravityObjectList)
        {
            //call Attract
            if (obj != this)
            Attract(obj);
        }
        
        //orbiting
        if (!planet)
        {
            rb.AddForce(Vector3.left * orbitSpeed);
        }

    }

    void Attract(Gravity other)
    {
        Rigidbody otherRb = other.rb;
        Vector3 direction = rb.position - otherRb.position;
        float distance = direction.magnitude;

        float forceMagnitude = G * (rb.mass * otherRb.mass/ Mathf.Pow( distance, 2));
        Vector3 gravityForce = forceMagnitude * direction.normalized;

        otherRb.AddForce(gravityForce);
    }

}
