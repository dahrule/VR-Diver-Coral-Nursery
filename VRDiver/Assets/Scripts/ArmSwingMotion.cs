using UnityEngine;
using UnityEngine.InputSystem;

//[RequireComponent (typeof(Rigidbody))]
public class ArmSwingMotion : MonoBehaviour
{
    [SerializeField] InputActionReference swimInputAction=null;
    [SerializeField] GameObject player;
    //[SerializeField] float limit=5;
    [SerializeField] float forceMultiplier = 1f;

    [SerializeField] GameObject controller;
    
    Vector3 previousPos; //previous frame left hand's position.
   
    Vector3 currentPos; 
   
   
    Vector3 displacement; //difference between previous and current frames positions for the left hand.

    Vector3 Player_previousPos; //previous frame player's position.
    Vector3 Player_currentPos; //current frame player's position.

    Vector3 movementDirection;

    public bool swimModeActive=false;

    //float currentPitchRightHand;
    //float previousPitchRightHand;

    Vector3 force;
    Rigidbody rb;
    float speed;

    GameManager gamemanager;

    private void Awake()
    {

        swimInputAction.action.performed += EnableSwimMode;
        swimInputAction.action.canceled += DisableSwimMode;

        gamemanager = GameManager.instance;

    }

    void EnableSwimMode (InputAction.CallbackContext obj)
    {

        swimModeActive = true;

    }

    void DisableSwimMode(InputAction.CallbackContext obj)
    {
        swimModeActive =false;
    }
    // Start is called before the first frame update
    void Start()
    {
       
        //Initialize previous frame positions for all variables.
       previousPos = controller.transform.position;
      

        Player_previousPos = player.transform.position;

        //previousPitchRightHand= rightHand.transform.rotation.eulerAngles.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.instance.PlayModeActive && swimModeActive)
        {
            //Get current position of hands.
            currentPos = controller.transform.position;
            

            //Get current position of player.
            Player_currentPos = player.transform.position;

            //Set Movement direction
            movementDirection = controller.transform.forward; // TO CHANGE DEPNDEING ON ACTIVE SWIMMING HAND.

            //Get the displacement of hands since last frame.
            displacement = previousPos - currentPos;
            
            var displacementMagnitude = displacement.magnitude;
         

            //Get the displacement of player since last frame.
            var playerDistanceMoved = Vector3.Distance(Player_currentPos, Player_previousPos); //difference between previous and current frames positions for the player.

            speed = displacementMagnitude;


            //TO ADD restrictions BASED ON PITCH CHANGE OF FRAMES to improve movement.
            //Set restrictions. 
            if (Time.timeSinceLevelLoad > 1f)
            {

                //force = movementDirection* speed*forceMultiplier*Time.fixedDeltaTime;
                //rb.AddForce(force, ForceMode.Impulse);

                player.transform.position += movementDirection * speed * Time.fixedDeltaTime * forceMultiplier;
            }

            //Set previous positions of hands for the next frame.
            previousPos = currentPos;
           

            //Set player position previous frame
            Player_previousPos = Player_currentPos;
        }

        }

   
}
