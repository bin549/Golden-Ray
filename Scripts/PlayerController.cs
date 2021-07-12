using Photon.Pun;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{ 
    public float speed = 10.0f;
    public float resetSpeed;
  
    public float dashSpeed = 70.0f;
    public float dashTime = 0.1f;
    public PhotonView photonView;
    private Animator anim;
    public Health health;
    public TextMeshProUGUI nameDisplay;

    private LineRenderer rend;

    public float minX, minY, maxX, maxY;

    private void Start()
    {
    photonView = GetComponent<PhotonView>();
    anim = GetComponent<Animator>();
    health = FindObjectOfType<Health>();    
    rend = FindObjectOfType<LineRenderer>();
    resetSpeed = speed;
    if (photonView.IsMine)
    {
    nameDisplay.text = PhotonNetwork.NickName;
    }else{ 
    nameDisplay.text = photonView.Owner.NickName;
    }
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 moveAmount = moveInput.normalized * speed * Time.deltaTime;
        transform.position += (Vector3)moveAmount;
        Warp();
        if (Input.GetKeyDown(KeyCode.Space) && moveInput != Vector2.zero)
        {
        StartCoroutine(Dash());
        }

        if (moveInput == Vector2.zero)
        {
        anim.SetBool("isRunning", false);
        }else{
        anim.SetBool("isRunning", true);
        }
        rend.SetPosition(0, transform.position);

        }else{
        rend.SetPosition(1, transform.position);

        }
    }

    private void Warp()
    {
        if (transform.position.x < minX)
        {
            transform.position = new Vector2(maxX, transform.position.y);
        }
        if (transform.position.x > maxX)
        {
            transform.position = new Vector2(minX, transform.position.y);
        }
        if (transform.position.y < minY)
        {
            transform.position = new Vector2( transform.position.x , maxY);
        }
        if (transform.position.y > maxY)
        {
           transform.position = new Vector2( transform.position.x , minY);
        }
    }

    private IEnumerator Dash()
    {
        speed =dashSpeed;
        yield return new WaitForSeconds(dashTime);
        speed = resetSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (photonView.IsMine)
        {
        if (other.tag == "Enemy")
        {
        health.TakeDamage();
        }
    }
    }
}
