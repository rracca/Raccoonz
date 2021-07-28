using UnityEngine;

public class Test_CameraController : MonoBehaviour
{
    [SerializeField]
    private float cameraSensitivity = 4.0f;
    [SerializeField]
    private Transform playerTransform;
    private float cameraDistanceY = 3.5f;
    private float cameraDistanceZ = 10.0f;
    private float cameraRotationX = 15.0f;
    private float minimumY = -50.0f;
    private float maximumY = 50.0f;
    private float inputHorizontal = 0.0f;
    //private float inputVertical = 0.0f;

    void LateUpdate()
    {
        CameraController();
    }

    void CameraController()
    {
        inputHorizontal += Input.GetAxis("Mouse X") * cameraSensitivity * Time.deltaTime;
        //inputVertical += Input.GetAxis("Mouse Y") * cameraSensitivity * Time.deltaTime;

        //inputVertical = Mathf.Clamp(inputVertical, minimumY, maximumY);

        Vector3 direction = new Vector3(0, cameraDistanceY, -cameraDistanceZ);
        //Quaternion cameraRotation = Quaternion.Euler(inputVertical, inputHorizontal, 0);
        Quaternion cameraRotation = Quaternion.Euler(cameraRotationX, inputHorizontal, 0);
        transform.position = playerTransform.position + cameraRotation * direction;

        transform.LookAt(playerTransform.position);
    }
}
