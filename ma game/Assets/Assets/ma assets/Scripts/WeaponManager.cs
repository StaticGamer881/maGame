using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace MaGame
{
    public class WeaponManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject[] loadout;
        [SerializeField] private Transform wepParent;
        [SerializeField] private GameObject bulletHolePrefab;
        [SerializeField] private Transform spawn;
        [SerializeField] private LayerMask canBeShot;
        


        private GameObject currentWeapon;

        void Start()
        {
        }

        void Update()
        {
            if (!photonView.IsMine) return;
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                photonView.RPC("Equip", RpcTarget.All, 0);
            }
            if(currentWeapon != null)
            {
                Aim(Input.GetMouseButton(1));

                if (Input.GetMouseButtonDown(0))
                {
                    photonView.RPC("shoot", RpcTarget.All);
                }
            }
            

        }
        [PunRPC]
        void Equip(int wep_no)
        {
            if (currentWeapon != null) { Destroy(currentWeapon); }

            GameObject new_GO = Instantiate(loadout[wep_no], wepParent.position, wepParent.rotation, wepParent) as GameObject;
            new_GO.transform.localPosition = Vector3.zero;
            new_GO.transform.localEulerAngles = Vector3.zero;
           // new_GO.GetComponent<Sway>().enabled = photonView.IsMine;

            currentWeapon = new_GO;
        }
        void Aim(bool isAiming)
        {
            Transform anchor = currentWeapon.transform.Find("Anchor");
            Transform Hip = currentWeapon.transform.Find("States/Hip");
            Transform ADS = currentWeapon.transform.Find("States/ADS");
            

            if (Input.GetMouseButtonDown(1))
            {
                anchor.position = Hip.position;
            }
            if (isAiming)
            {
                anchor.position = Vector3.Lerp(anchor.position, ADS.position, Time.deltaTime * 10);
            }
            else
            {
                anchor.position = Vector3.Lerp(anchor.position, Hip.position, Time.deltaTime * 10);
            }
        }
        [PunRPC]
        void shoot()
        {
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(spawn.position, spawn.forward, out hit, 1000f, canBeShot))
            {
                GameObject newBulletHole = Instantiate(bulletHolePrefab, hit.point + hit.normal * 0.001f, Quaternion.identity) as GameObject;
                newBulletHole.transform.LookAt(hit.point + hit.normal);
                Vector3 newRotation = newBulletHole.transform.localEulerAngles;
                newRotation.x += 90;
                newBulletHole.transform.localEulerAngles = newRotation;
                Destroy(newBulletHole, 20f);
                if (photonView.IsMine)
                {
                    if(hit.collider.gameObject.layer == 10)
                    {
                        hit.collider.gameObject.GetPhotonView().RPC("takeDamage", RpcTarget.All, 5);
                    }
                }
            }
        }
        [PunRPC]
        private void takeDamage(int damage)
        {
            GetComponent<playerMovement>().takeDamage(damage);
        }

    }
}
