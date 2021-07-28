using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController playerController;
    [SerializeField]
    private Transform playerCamera;
    [SerializeField]
    private float playerSpeed;
    // Start is called before the first frame update
    void Start()
    {
        //initialize and make sure that there is a Character Controller
        playerController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        //Time.deltatime makes sure it's every frame
        float inputHorizontal = Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime;
        float inputVertical = Input.GetAxis("Vertical") * playerSpeed * Time.deltaTime;

        //Vector3 (x, 0, 0) * x-axis input; sideways
        //Vector3 (0, 0, z) * z-axis input; forward
        //Result Vector (x1, 0, z1)
        Vector3 playerMovement = playerCamera.right * inputHorizontal + playerCamera.forward * inputVertical;
        playerMovement.y = 0f; //Makes sure that y-axis stays the same
        playerMovement += Physics.gravity * Time.deltaTime;

        playerController.Move(playerMovement);

        if (playerMovement.magnitude != 0f)
        {
            //Vector3 (0, y, 0) * x-axis input; mouse sideways
            //playerCamera.GetComponent<CameraScript>().sensitivity
            transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Time.deltaTime);
            
            Quaternion cameraRotation = playerCamera.rotation;
            cameraRotation.x = 0f;
            cameraRotation.z = 0f;

            transform.rotation = Quaternion.Lerp(transform.rotation, cameraRotation, 0.1f);
        }
    }
}
