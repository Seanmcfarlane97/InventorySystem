using UnityEngine;
using System.Collections;

public class ModelScript : MonoBehaviour
{
    public float rotationSpeed = 1f;
    public float rotationDamper = 1f; 
    public float mouseSensitivity = 0.4f;

    private Vector3 mouseReference;
    private Vector3 mouseOffset;
    private Vector3 rotation;
    private bool isRotating; 

    void Update()
    {
        //Check if isRotating
        if(isRotating)
        {
            //Obtain the offset of the mouse
            mouseOffset = Input.mousePosition - mouseReference;
            //Apply the rotation 
            rotation.y = (mouseOffset.x + mouseOffset.y) * -mouseSensitivity;
            //Rotate
            transform.Rotate(rotation);
            //Store tge bew niyse reference 
            mouseReference = Input.mousePosition; 
        }
        else
        {
            float sign = rotation.y < 0 ? -1 : 1;
            rotation.y -= rotationDamper * sign * Time.deltaTime;
            rotation.y = Mathf.Abs(rotation.y) <= 1 ? sign : rotation.y;
            transform.rotation *= Quaternion.AngleAxis(rotationSpeed * rotation.y, Vector3.up);
        }
    }
    void OnMouseDown()
    {
        //Start rotating when mouse is down
        isRotating = true;
        //Store the mouse position in reference 
        mouseReference = Input.mousePosition; 
    }

    void OnMouseUp()
    {
        //Stop rotating when mouse is up
        isRotating = false; 
    }
}
