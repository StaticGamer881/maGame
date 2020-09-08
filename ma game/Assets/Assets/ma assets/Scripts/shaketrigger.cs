using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MaGame
{
    public class shaketrigger : MonoBehaviour
    {
        [SerializeField] private GameObject _player;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "wall")
            {
                _player.GetComponent<camerashake>().vibrate();
            }
        }
    }
}
