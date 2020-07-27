using UnityEngine;

namespace PathCreation.Examples
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    
    // ----------------[ATTENTION!]-----------------
    // This is a modified version of the PathFollower Component - some changes were made specific to the physics motion of a rollercoaster.
    // SET SPEED HERE!
    public class CarMovement : MonoBehaviour
    {
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public float speed;
        float distanceTravelled;
        private float tempSpeed;

        void Start()
        {
            if (pathCreator != null)
            {
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;
            }
        }

        void Update()
        {
            if (pathCreator != null)
            { 
                
                if (Input.GetKey(KeyCode.Space))
                {
                    tempSpeed = 0;
                    distanceTravelled += tempSpeed * Time.deltaTime;
                    transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                    transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                }
                else
                {
                    
                    distanceTravelled += speed * Time.deltaTime;
                    transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                    transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                }
                
            }

           

        }

        // If the path changes during the game, update the distance travelled so that the follower's position on the new path
        // is as close as possible to its position on the old path
        void OnPathChanged()
        {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }
    }
}