using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Enemy : MonoBehaviour
{
    private PlayerController[] players;
    private PlayerController nearestPlayer; 
    public float speed; 
    private Score score;
    public GameObject deathFX;
    public PhotonView photonView;

    private void Start() 
    { 
        photonView = GetComponent<PhotonView>();
        players = FindObjectsOfType<PlayerController>();
        score = FindObjectOfType<Score>();
    } 
                        
    private void Update()
    {
        float distanceOne = Vector2.Distance(transform.position, players[0].transform.position);
        float distanceTwo = Vector2.Distance(transform.position, players[1].transform.position);
        if (distanceOne < distanceTwo)
        {
            nearestPlayer = players[0];
        } 
        else 
        {
            nearestPlayer = players[1];
        } 

        if (nearestPlayer != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, nearestPlayer.transform.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (other.tag == "GoldenRay")
            {
                score.AddScore();
                photonView.RPC("SpawnParticle", RpcTarget.All);
                PhotonNetwork.Destroy(this.gameObject);
            }
        }
    }

    [PunRPC]
    private void SpawnParticle()
    {
        Instantiate(deathFX, transform.position, Quaternion.identity);
    }
}
