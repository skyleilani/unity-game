using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateFractals : MonoBehaviour
{
    // set initiator shape for fractal 
    // only classes that inherit this class can access _initiator variable 
    // enum (iterable?) object type, named constants of underlying int types 
     
    protected enum _initiator
    {
         Triangle, 
         Square, 
         Pentagon, 
         Hexagon, 
         Heptagon, 
         Octagon
    };
    // protected _initiator won't show up in the inspector unless we serialize the field 
    [SerializeField]

    // instance of object type _initiator 
    protected _initiator initiator_shape = new _initiator();

    // updates an integer that tells us by how many sides the shape is defined 
    protected int _initiatorPointAmount;

    // stores all the initiator points 
    private Vector3[] _initiatorPoint;

     // rotate vector 
     private Vector3 _rotateVector;

    // define initiator size 
    protected float _initiatorSize;
    [SerializeField]

    private void OnDrawGizmos()
    {
        GetInitiatorPoints();

        // place a point in a certain direction at a certain length and then rotate
        // the vector by 360 / initiator point amount  

        // new vector 3 with a length of the initiatorPointAmount  
        _initiatorPoint = new Vector3[_initiatorPointAmount];

        // set rotate vector to the direction in which we want to make our rotation when spawning our points 
        // Vector is revolving around the z axis 
        _rotateVector = new Vector3(0, 0, 1);

        // fill _initiatorPointAmount array with all the points we need, every time
        // it's filled with a point, vector rotates a certain rotation 
        for (int i = 0; i < _initiatorPointAmount; i++ )
        {

        }

    }

    // returns initiator point amount depending on selected initiator_shape / named constant 
    private void GetInitiatorPoints()
    {
        // value of _initiatorPointAmount depending on each named constant in _initiator enum 
        switch (initiator_shape)
        {
            case _initiator.Triangle:
                _initiatorPointAmount = 3; 
                break;

            case _initiator.Square:
                _initiatorPointAmount = 4;

                break;

            case _initiator.Pentagon:
                _initiatorPointAmount = 5;

                break;

            case _initiator.Hexagon:
                _initiatorPointAmount = 6;

                break;

            case _initiator.Heptagon:
                _initiatorPointAmount = 7;

                break;

            case _initiator.Octagon:
                _initiatorPointAmount = 8;

                break;
            default:
                _initiatorPointAmount = 3;

                break; 

        }
    }


   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
