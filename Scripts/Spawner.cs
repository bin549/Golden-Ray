using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Spawner : MonoBehaviour
{
  public Transform[] spawnerPoints;

  public GameObject enemy;
  public float startTimeBetweenWaves;
  public float timeBetweenWaves;

  private void Start()
{
  timeBetweenWaves = startTimeBetweenWaves;  
}

  private void Update()
{
if (!PhotonNetwork.IsMasterClient || PhotonNetwork.CurrentRoom.PlayerCount != 2)
{
  return;
}
  if (timeBetweenWaves  <= 0)
  {
    Vector2 spawnPoint = spawnerPoints[Random.Range(0, spawnerPoints.Length)].position;
    PhotonNetwork.Instantiate(enemy.name, spawnPoint, Quaternion.identity);
timeBetweenWaves = startTimeBetweenWaves;
  }else
  {
    timeBetweenWaves -= Time.deltaTime;
  }
}
}
