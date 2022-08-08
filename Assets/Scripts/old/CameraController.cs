using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Variables
    [SerializeField] private float mouseSensivility;

    //References
    private Transform parent;
    void Start()
    {
        parent = transform.parent;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        
    }
    private void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensivility * Time.deltaTime;
        parent.Rotate(Vector3.up * mouseX);
    }
}
