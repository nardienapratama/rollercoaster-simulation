// Source: https://answers.unity.com/questions/1077171/how-to-make-camera-follow-player-position-and-rota.html
// https://answers.unity.com/questions/762475/limit-range-camera-rotation.html
using UnityEngine;

public class FollowCar : MonoBehaviour
{
    // Camera Position variables
    public Transform car;
    public Vector3 offsetPos = new Vector3(0.0f, 1.58f, 0.0f);
    public Space offsetPosSpace = Space.Self;
    public bool lookAtCar = false;

    // Camera Rotation variables
    public float RotationSensitivity = 500.0f;
    public float minAngleY = -90.0f;
    public float maxAngleY = 90.0f;
    public float minAngleZ = -45.0f;
    public float maxAngleZ = 55.0f;

    float yRotate = 0.0f;
    float zRotate = 0.0f;
    

    private void Start()
    {
        transform.position = car.position + offsetPos;
    }

    // FixedUpdate is used because some physics is involved
    private void FixedUpdate()
    {
        RefreshPosition();
        HandleMouseRotation();
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

    private void HandleMouseRotation()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            yRotate += Input.GetAxis("Mouse X") * RotationSensitivity * Time.deltaTime;
            yRotate = Mathf.Clamp(yRotate, minAngleY, maxAngleY);

            zRotate += Input.GetAxis("Mouse Y") * RotationSensitivity * Time.deltaTime;
            zRotate = Mathf.Clamp(zRotate, minAngleZ, maxAngleZ);

            transform.Rotate(-zRotate, yRotate,0.0f , offsetPosSpace);
        }
    }

    // TODO
    // Assign camera positions - use mouse to look left and right, spacebar to change to
    // 3rd POV, etc
}