using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace MaGame
{
    public class MouseY : MonoBehaviourPunCallbacks
    {
        private bool escPressed = false;
        [SerializeField] private float sensitivityY = 1f;
        [SerializeField] private Transform _guns;

        void Update()
        {
            if (!photonView.IsMine) return;
            if (Input.GetKeyDown(KeyCode.Escape) && escPressed) { escPressed = false; }
            else if (Input.GetKeyDown(KeyCode.Escape) && !escPressed) { escPressed = true; }
            if (!escPressed)
            {
                float mouse = Input.GetAxis("Mouse Y");
                Vector3 newRotation = transform.localEulerAngles;
                newRotation.x -= mouse * sensitivityY;
                transform.localEulerAngles = newRotation;
                _guns.transform.rotation = transform.rotation;
            }
            
        }
    }
}