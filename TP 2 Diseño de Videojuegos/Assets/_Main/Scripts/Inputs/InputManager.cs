using UnityEngine;

public class InputManager : MonoBehaviour
{
/*    [SerializeField] private GameObject player;
    [SerializeField] public float MouseSensitivity = 50;
    private float xRotation = 0.0f;
    private float yRotation = 0.0f;
    private Camera cam;
    private const string xAxis = "Mouse X";
    private const string yAxis = "Mouse Y";


    private void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.CurrentState == GameManager.GameStates.Playing)
        {
            float MouseX = Input.GetAxis(xAxis) * MouseSensitivity + Time.deltaTime;
            float MouseY = Input.GetAxis(yAxis) * MouseSensitivity + Time.deltaTime;

            xRotation -= MouseY;
            xRotation = Mathf.Clamp(xRotation, -90, 90);
        

      //      cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            player.transform.Rotate(Vector3.up * MouseX);

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKey(KeyCode.W))
            {
                player.GetComponent<PlayerMovementScript>().MoveForward();
            }
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKey(KeyCode.A))
            {
                player.GetComponent<PlayerMovementScript>().MoveLeft();
            }
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKey(KeyCode.D))
            {
                player.GetComponent<PlayerMovementScript>().MoveRight();
            }
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKey(KeyCode.S))
            {
                player.GetComponent<PlayerMovementScript>().MoveBack();
            }

            if (Input.GetMouseButtonDown(0))
            {
                player.GetComponent<PlayerMovementScript>().Shoot();
            }
        } 
    }*/
}
