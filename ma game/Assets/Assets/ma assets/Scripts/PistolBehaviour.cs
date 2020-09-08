using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sway : MonoBehaviour
{
    [SerializeField] private float intensity = 1;
    [SerializeField] private float smoothReturn = 1;
    
    

    private Quaternion ogRotation;
    void Start()
    {
        ogRotation = transform.localRotation;
    }

    void Update()
    {
        UpdateSway();
    }

    private void UpdateSway()
    {
        float xMouse = Input.GetAxis("Mouse X");
        float yMouse = Input.GetAxis("Mouse Y");

        Quaternion XrotationAdj = Quaternion.AngleAxis(intensity * xMouse, Vector3.down);
        Quaternion YrotationAdj = Quaternion.AngleAxis(intensity * yMouse, Vector3.left);

        Quaternion targetRotation = ogRotation * XrotationAdj * YrotationAdj;

        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, smoothReturn * Time.deltaTime);
    }
}
