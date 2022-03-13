using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateFractals : MonoBehaviour
{
    //!!!! ISSUE: SHOULD B ABLE TO ASSIGN INT VALUES TO ENUMS & SKIP SWITCH STEP 
    // ASSIGN IT DIRECTION INITIATORPOINTAMOUNT = (INT) _INITIATOR; ??? 

    // Serialized: set initiator shape for fractal 
    // only classes that inherit this class can access _initiator variable 
    // enum (iterable?) object type, named constants of underlying int types  
    protected enum _initiator
    {
         Triangle, 
         Square , 
         Pentagon, 
         Hexagon , 
         Heptagon  , 
         Octagon 
    };
    [SerializeField]

    // instance of object type _initiator 
    protected _initiator initiator_shape = new _initiator();

    // updates an integer that tells us by how many sides the shape is defined 
    protected int _initiatorPointAmount;

    // stores all the initiator points 
    private Vector3[] _initiatorPoint;

     //Serialized:  rotate vector 
     private Vector3 _rotateVector;
    [SerializeField]

    // specify which axis you are rotating along 
    private Vector3 _rotateAxis; 

    [SerializeField]
    // define initiator size 
    protected float _initiatorSize;


    // place a point in a certain direction at a certain length and then rotate
    // the vector by 360 / initiator point amount 
    private void OnDrawGizmos()
    {
        GetInitiatorPoints();

        // new vector 3 with a length of the initiatorPointAmount  
        _initiatorPoint = new Vector3[_initiatorPointAmount];

        // set rotate vector to the direction in which we want to make our rotation when spawning our points 
        // Vector is revolving around the z axis 
        _rotateVector = new Vector3(0, 0, 1);

        _rotateAxis = new Vector3(0, 1, 0);  

        // fill _initiatorPointAmount array with all the points in the initiator shape, every time
        // it's filled with a point, vector rotates a certain rotation 
        for (int i = 0; i < _initiatorPointAmount; i++ )
        { 
            // we start with the rotateVector multiplied by  on the first iteration 
            _initiatorPoint[i] = _rotateVector * _initiatorSize;

            // rotate _rotateVector by a specific angle per point in selected shape  
            // angle - 360/amount of points in selected initiator shape 
            // axis - 
            _rotateVector = Quaternion.AngleAxis(360/ _initiatorPointAmount, _rotateAxis) * _rotateVector; 
            // Quaternion.AngleAxis - creates a rotation which rotatoes "angle" degrees around "axis" 
            // syntax: Quaternion.AngleAxis(angle, axis); 
        }

        for(int i = 0; i < _initiatorPointAmount; i++)
        {
            Gizmos.color = Color.white;

            // next 2 lines allows rotation in unity 
            // create rotation matrix
            Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
            // apply rotation matrix to gizmos 
            Gizmos.matrix = rotationMatrix;

            // check if it's the last line, the last line should return to line zero
            if(i< _initiatorPointAmount - 1 )
            {
                Gizmos.DrawLine(_initiatorPoint[i], _initiatorPoint[i + 1]);
            }
            else
            {
                Gizmos.DrawLine(_initiatorPoint[i], _initiatorPoint[0]);

            }
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
}
