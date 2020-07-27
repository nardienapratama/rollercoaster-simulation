// Source: https://answers.unity.com/questions/1077171/how-to-make-camera-follow-player-position-and-rota.html
using UnityEngine;

public class FollowCar3rdPOV : MonoBehaviour
{
    // Camera Position variables
    public Transform car;
    public Vector3 offsetPos = new Vector3(-3.86f, 2.54f, 2.95f);
    public Space offsetPosSpace = Space.Self;
    public bool lookAtCar = true;

    private void Start()
    {
        transform.position = car.position + offsetPos;

    }

    // FixedUpdate is used because some physics is involved
    private void FixedUpdate()
    {
        RefreshPosition();
    }

    private void RefreshPosition()
    {
        if (car == null)
        {
            Debug.LogWarning("Missing car ref!", this);
            return;
        }

        // compute the position of the camera
        if (offsetPosSpace == Space.Self)
        {
            // if the position of the car involves rotation in space
            transform.position = car.TransformPoint(offsetPos);
        }
        else
        {
            // if the position of the car doesn't involve rotation
            transform.position = car.position + offsetPos;
        }

        // compute rotation of the camera
        if (lookAtCar)
        {
            // this rotates the car so that the forward vector points at the car's current position
            // this causes it to look at the car and not at the tracks - SET TO LOOKAT TO FALSE!
            transform.LookAt(car);
        }
        else
        {
            // get the rotation of the car in world space from the quaternion and set it to the rotation of the camera
            transform.rotation = car.rotation;
        }
    }
}