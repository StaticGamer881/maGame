using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class manager : MonoBehaviour
{
    [SerializeField] private string player_prefab;
    [SerializeField] private Transform spawn_position;

    private void Start()
    {
        Spawn();
    }
    public void Spawn()
    {
        //Transform spawnPlayer = spawn_position[Random.Range(0, spawn_position.Length)];
        PhotonNetwork.Instantiate(player_prefab, spawn_position.position, spawn_position.rotation);
    }
}
