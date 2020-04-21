using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool enableMouse;

    [Header("Player configurarion")]
    public string playerName;
    public int life;
    public float speed;
    public float runSpeed;
    public float sensivity;

    [Header("Imports")]
    public Camera cam;

    //Private
    private Rigidbody rb;
    private float realSpeed;
    private Vector3 velocity;
    private Vector3 rotation;
    private Vector3 camRotation;
    private float rotCam;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        #region Movement
        float movX = Input.GetAxisRaw("Horizontal");
        float movY = Input.GetAxisRaw("Vertical");

        if(Input.GetButton("Run") == true && movX == 0 && movY == 1)
        {
            realSpeed = runSpeed;
        }
        else
        {
            realSpeed = speed;
        }
        Vector3 moveHorizontal = transform.right * movX;
        Vector3 moveVertical = transform.forward * movY;

        velocity = (moveHorizontal + moveVertical).normalized * realSpeed;

        #endregion

        #region Rotation
        float mouseY = Input.GetAxisRaw("Mouse X");
        rotation = new Vector3(0, mouseY, 0) * sensivity;

        float mouseX = Input.GetAxisRaw("Mouse Y");
        camRotation = new Vector3(mouseX, 0, 0) * sensivity;
        #endregion

        #region enableMouse
        if (enableMouse)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        #endregion
    }

    private void FixedUpdate()
    {
        if (enableMouse)
        {
            Movement();
            Rotation();
        }
    }

    void Movement()
    {
        if(velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.deltaTime);
        }
    }

    void Rotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));

        if(cam != null)
        {
            rotCam += camRotation.x;
            rotCam = Mathf.Clamp(rotCam, -80, 80);

            cam.transform.localEulerAngles = new Vector3(-rotCam, 0, 0);
        }
    }
}
