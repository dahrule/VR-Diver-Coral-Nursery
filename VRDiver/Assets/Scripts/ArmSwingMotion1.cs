/*using UnityEngine;
using UnityEngine.InputSystem;

//[RequireComponent (typeof(Rigidbody))]
public class ArmSwingMotion1 : MonoBehaviour
{
¨   [SerializeField] InputActionReference swimInputActionRight;
    [SerializeField] InputActionReference swimInputActionLeft;

    //[SerializeField] float limit=5;
    [SerializeField] float forceMultiplier = 1f;
    [SerializeField] GameObject leftHand;
    [SerializeField] GameObject rightHand;
    Vector3 LeftHand_previousPos; //previous frame left hand's position.
    Vector3 RightHand_previousPos; //previous frame right hand's position.
    Vector3 LeftHand_currentPos; //current frame left hand's position.
    Vector3 RightHand_currentPos;//current frame right hand's position.
   
    Vector3 LeftHand_displacement; //difference between previous and current frames positions for the left hand.
    Vector3 RightHand_displacement; //difference between previous and current frames positions for the right hand.

    Vector3 Player_previousPos; //previous frame player's position.
    Vector3 Player_currentPos; //current frame player's position.

    Vector3 movementDirection;

    //float currentPitchRightHand;
    //float previousPitchRightHand;

    Vector3 force;
    Rigidbody rb;
    float speed;

    private void Awake()
    {
        swimInputActionRight.action.performed += Test;
        rb = GetComponent<Rigidbody>();
    }

    void Test (InputAction.CallbackContext obj)
        {
        Debug.Log(obj.ReadValue<bool>());
        }
    // Start is called before the first frame update
    void Start()
    {
        //Initialize previous frame positions for all variables.
        LeftHand_previousPos = leftHand.transform.position;
        RightHand_previousPos = rightHand.transform.position;

        Player_previousPos = this.transform.position;

        //previousPitchRightHand= rightHand.transform.rotation.eulerAngles.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.instance.PlayModeActive)
        {
            //Get current position of hands.
            LeftHand_currentPos = leftHand.transform.position;
            RightHand_currentPos = rightHand.transform.position;

            //Get current position of player.
            Player_currentPos = this.transform.position;

            //Set Movement direction
            movementDirection = rightHand.transform.forward; // TO CHANGE DEPNDEING ON ACTIVE SWIMMING HAND.

            //Get the displacement of hands since last frame.
            LeftHand_displacement = LeftHand_previousPos - LeftHand_currentPos;
            RightHand_displacement = RightHand_previousPos - RightHand_currentPos;
            var LeftHand_displacementMagnitude = LeftHand_displacement.magnitude;
            var RightHand_displacementMagnitude = RightHand_displacement.magnitude;

            //Get the displacement of player since last frame.
            var playerDistanceMoved = Vector3.Distance(Player_currentPos, Player_previousPos); //difference between previous and current frames positions for the player.

            speed = RightHand_displacementMagnitude;


            //TO ADD restrictions BASED ON PITCH CHANGE OF FRAMES to improve movement.
            //Set restrictions. 
            if (Time.timeSinceLevelLoad > 1f)
            {

                //force = movementDirection* speed*forceMultiplier*Time.fixedDeltaTime;
                //rb.AddForce(force, ForceMode.Impulse);

                transform.position += movementDirection * speed * Time.fixedDeltaTime * forceMultiplier;
            }

            //Set previous positions of hands for the next frame.
            LeftHand_previousPos = LeftHand_currentPos;
            RightHand_previousPos = RightHand_currentPos;

            //Set player position previous frame
            Player_previousPos = Player_currentPos;
        }

        }

   
}
*/