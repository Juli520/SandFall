using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerState : MonoBehaviourPun
{
    [Header("Movement")]
    public float speed = 5;
    public float jumpForce = 7;
    public float rotationSpeed = 5;
    [Header("Attack")] 
    public float attackRate = 3f;
    public float pushForce = 5f;
    public Collider hitBox;
    [Header("Death")]
    public float deathHeight = -2f;
    public Player owner;

    [SerializeField, HideInInspector]private float _baseJumpForce;
    [SerializeField, HideInInspector]private Rigidbody _rb;
    
    private LevelManager _lvlManager;

    private void Awake()
    {
        if(!photonView.IsMine) return;

        _lvlManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        _rb = GetComponent<Rigidbody>();
        owner = photonView.Owner;
        
        _baseJumpForce = jumpForce;
    }

    private void Update()
    {
        if(!photonView.IsMine) return;
        
        if (transform.position.y <= deathHeight)
            KillPlayer();
    }

    public void ApplyBuff(float multiplier, float duration)
    {
        if(!photonView.IsMine) return;
        
        StartCoroutine(Apply(multiplier, duration));
    }
    
    /// <summary>
    /// Applies the buff using the specific multiplier and duration
    /// </summary>
    /// <param name="multiplier"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    private IEnumerator Apply(float multiplier, float duration)
    {
        jumpForce *= multiplier;
        yield return new WaitForSeconds(5);
        jumpForce = _baseJumpForce;
    }

    private void KillPlayer()
    {
        _lvlManager.SumPlayersDead();
        _lvlManager.LoadLoseScene();
        PhotonNetwork.Destroy(gameObject);
    }
    
    [PunRPC]
    public void PushedByOtherPlayer(Vector3 direction)
    {
        _rb.AddForce(direction, ForceMode.Impulse);
    }
    
    internal void PushPlayer(Player target, Vector3 direction)
    {
        photonView.RPC("PushedByOtherPlayer", target, direction);
    }
}
