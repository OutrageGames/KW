using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using UnityEngine.InputSystem;

public abstract class Gun : NetworkBehaviour
{
    public bool IsShooting { get => _isShooting; set => _isShooting = value; }
    public bool IsReloading { get => _isReloading; }
    // public bool IsOwner { get => _isOwner; }
    // public ulong OwnerClientId { get => _ownerId; }

    private bool _isShooting, _isReloading;
    // private ulong _ownerId;
    public float fireRate, spreadRate, spread;
    public GameObject _bulletPrefab, particleEffect;
    public int oneMagBullets, currentBullets;
    public Transform spawnPoint, efePoint;
    // public Vector2 spawnPointPos;
    // private Animator anim;
    private string currentState;
    // public string idleAnimation, shootAnimation, reloadAnimation;
    // public AudioClip shootSound;
    // public AudioSource audioSource;
    // private float _rotZ;
    // private PlayerVariables playerVars;
    // [SerializeField] private float shakeAmount;
    // private PlayerCameraController _cameraController;
    public InputMaster controls;


    private float _shootTimer;
    protected bool _immediateDestroyBullet;
    public float ReloadTime, ReloadStartTime = 2f;

    public void Initialize(bool isOwner, ulong ownerId)
    {
        // _ownerId = ownerId;
        // _isOwner = isOwner;
        // Debug.Log($"IsOwner: {_isOwner}, OwnerID: {_ownerId}");
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        if(!base.IsOwner)
        {
            GetComponent<Gun>().enabled = false;
        }
    }

    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }

    private void Awake()
    {
        controls = new InputMaster(); 
        
        // playerVars = GetComponentInParent<PlayerVariables>();
        // _cameraController = transform.parent.GetComponent<PlayerCameraController>();

        //userController con = GameObject.FindGameObjectWithTag("controller").GetComponent<userController>();

        // if (con.playerID == playerVars.playerID)
        // {
        //     GetComponentInChildren<SpriteRenderer>().color = con.gunColor;
        //     oneMagBullets = con.gun.magBullets;
        // }

        currentBullets = oneMagBullets;

        //audioSource = GetComponent<AudioSource>();

        // anim = GetComponentInChildren<Animator>();
        //audioSource = GetComponent<AudioSource>();

        // spawnPoint.localPosition = spawnPointPos;
        // efePoint.localPosition = spawnPointPos;
    }

    void Start()
    {
        controls.Player.Shoot.performed += ShootCallback;
        controls.Player.Shoot.canceled += ShootCallback;
    }

    public virtual void Reload()
    {
        // if (!playerVars.HasDebuff(TimedEffectType.Stun))
        // {
        _isReloading = true;
        spread = 0;
        // }
    }

    // public virtual void StartShoot()
    // {
    //     _isShooting = start;

    //     if (!start)
    //     {
    //         spread = 0.0f;
    //     }
    // }

    public void ShootCallback(InputAction.CallbackContext context)
    {
        if(!base.IsOwner)
            return;

        if (context.performed)
        {
            // Shoot();
            // ServerShoot();
            // Shooting();
            StartCoroutine(Shooting());
        }
        if (context.canceled)
        {
            spread = 0;
            _isShooting = false;
            StopAllCoroutines();
        }
    }

    public virtual IEnumerator Shooting()
    {
        yield return new WaitForSeconds(.1f);
    }

    public void Update()
    {
        if(_isReloading)
        {
            ReloadTime -= Time.deltaTime;
        }

        if(ReloadTime <= 0)
        {
            _isReloading = false;
            ReloadTime = ReloadStartTime;
            currentBullets = oneMagBullets;
        }
        // if (anim.GetCurrentAnimatorStateInfo(0).IsName(reloadAnimation) && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        // {
        //     _isReloading = false;
        //     currentBullets = oneMagBullets;
        // }

        // if (_isReloading)
        // {
        //     PlayAnim(reloadAnimation);
        // }
        // else if (!_isShooting)
        // {
        //     PlayAnim(idleAnimation);
        // }

        if (currentBullets <= 0 && !_isReloading)
        {
            _isReloading = true;
            spread = 0;
        }

        if (_isShooting && _shootTimer <= 0.0f && !_isReloading)
        {
            Shooting();
            _shootTimer = fireRate;

            // if (_isOwner)
            // {
            //     _cameraController.CameraShake(shakeAmount, 0.1f);
            // }

            // PlayAnim(shootAnimation);
        }
        else if (_shootTimer > 0.0f)
        {
            _shootTimer -= Time.deltaTime;
        }
    }

    // void PlayAnim(string newState)
    // {
    //     if (currentState == newState) return;
    //     anim.Play(newState);
    //     currentState = newState;
    // }


    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.gameObject.layer == 16 || collision.gameObject.layer == 17)
    //     {
    //         _immediateDestroyBullet = true;
    //         spawnPoint.localPosition = efePoint.transform.localPosition;
    //     }
    // }

    // private void OnTriggerExit2D(Collider2D collision)
    // {
    //     if (collision.gameObject.layer == 16 || collision.gameObject.layer == 17)
    //     {
    //         _immediateDestroyBullet = false;
    //         spawnPoint.localPosition = spawnPointPos;
    //     }
    // }
}
