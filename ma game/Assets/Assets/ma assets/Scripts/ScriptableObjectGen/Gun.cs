using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MaGame
{
    [CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
    public class Gun : ScriptableObject
    {
        //[SerializeField] private int ammo;
        //[SerializeField] private int clipSize;
        [SerializeField] private string name;
        [SerializeField] private float fireRate;
        [SerializeField] private GameObject profab;

        //private int clip;
        //private int stash;

       /* private bool FireBullet()
        {
            if (clip > 0)
            {
                clip -= 1;
                return true;
            }
            else return false;
        }

        private void Relode()
        {
            stash += clip;
            clip = Mathf.Min(clipSize, stash);
            stash -= clip;
        }*/
    }
}
