using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float cameraSensitivity = 4.0f;
    [SerializeField]
    private Transform playerTransform;
    public float cameraDistanceY = 25.0f;
    public float cameraDistanceZ = 5.0f;
    public float cameraRotationX = 75.0f;
    private float minimumY = -50.0f;
    private float maximumY = 50.0f;
    private float inputHorizontal = 0.0f;
    //private float inputVertical = 0.0f;

    void LateUpdate()
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
