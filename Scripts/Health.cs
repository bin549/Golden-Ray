using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Health : MonoBehaviour
{
    public float health = 10f;
    public Text healthText;
    public PhotonView photonView;
    public GameObject gameOver;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        healthText.text = health.ToString();
    }

    public void TakeDamage() 
    {
       photonView.RPC("TakeDamageRPC", RpcTarget.All);
    }

    [PunRPC]
    public void TakeDamageRPC()
    {
        health--;
        if (health <= 0)
        {
            gameOver.SetActive(true);
        }
        healthText.text = health.ToString();
    }
}
