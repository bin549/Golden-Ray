using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class GameOver : MonoBehaviour
{
    public Text scoreDisplay;
    public GameObject restartButton;
    public GameObject waitingText;                                          
    public PhotonView photonView; 
 
    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        scoreDisplay.text = FindObjectOfType<Score>().score.ToString();

        if (!PhotonNetwork.IsMasterClient)
        {
            restartButton.SetActive(false);
            waitingText.SetActive(true); 
        } 
    }

    public void OnClickRestart()
    {
        photonView.RPC("Restart", RpcTarget.All);
    }

    [PunRPC]   
    public void Restart()
    {
        PhotonNetwork.LoadLevel("Game");
    }
}
 