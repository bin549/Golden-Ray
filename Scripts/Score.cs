using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Score : MonoBehaviour
{
  public int score = 0;

  public Text scoreText;
  public PhotonView photonView;

  private void Start()
  {
  photonView = GetComponent<PhotonView>();
  scoreText.text = score.ToString();
  }

  public void AddScore()
{
  photonView.RPC("AddScoreRPC", RpcTarget.All);
}

    [PunRPC]
    public void AddScoreRPC()
    {
      score++;
      scoreText.text = score.ToString();
    }
}
   