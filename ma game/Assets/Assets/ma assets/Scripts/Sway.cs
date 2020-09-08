using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PistolBehaviour : MonoBehaviourPunCallbacks
{
    [SerializeField] private float intensity = 1;
    [SerializeField] private float smoothReturn = 1;
    private bool _canCal = true;

    private Quaternion ogRotation;
    void Start()
    {
        ogRotation = transform.localRotation;
    }

    void Update()
    {
        if (_canCal)
        {
            UpdateSway();
        }
            if (!photonView.IsMine) return;
        
        if(Input.GetKeyDown(KeyCode.Escape) && _canCal) { _canCal = false; }
        else if (Input.GetKeyDown(KeyCode.Escape) && !_canCal) { _canCal = true; }
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
