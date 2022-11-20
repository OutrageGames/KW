using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using UnityEngine.InputSystem;
using System;

public class Skills : NetworkBehaviour
{
    public InputMaster controls;
    public float cooldown1, cooldown2, duration1, duration2;
    public bool active1, active2;
    public PlayerVariables PlayerVariables;
    //public GameObject[] skillObjects;
    //public float[] actualCooldown1, actualCooldown2, skillDamages1, skillDamages2, startDuration1, startDuration2;
    
    // public PlayerStatsController _playerStatsController;
    // Animator skillAnimator1, skillAnimator2;

    // private PlayerCameraController _cameraController;

    // public PlayerCameraController PlayerCameraController { get => _cameraController; }

    public override void OnStartClient()
    {
        base.OnStartClient();

        
        // if(!base.IsOwner)
        // {
        //     GetComponent<Skills>().enabled = false;
        // }

        //gameObject.AddComponent(Type.GetType(PlayerVariables.Warrior.skillSet)); 
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
    }

    private void Start()
    {
        controls.Player.Skill1.performed += Skill1Callback;
        controls.Player.Skill2.performed += Skill2Callback;
        PlayerVariables = GetComponent<PlayerVariables>();

        //controls.actions.get["Player"].Ability1.performed += Ability1Callback;
        //controls.Player.Ability2.performed += Ability2Callback;
        // InputAction asd = controls.currentActionMap.actions[3];
        // asd += Ability1Callback;
        // _cameraController = GetComponent<PlayerCameraController>();

        // if (playerVars.IsOwner)
        // {
        //     skillAnimator1 = GameObject.Find("skillAnimator1").GetComponent<Animator>();
        //     skillAnimator2 = GameObject.Find("skillAnimator2").GetComponent<Animator>();
        // }

        // controls = new InputMaster();

        // controls.Player.Ability1.performed += DoAbility1;
        // controls.Player.Ability2.performed += DoAbility2;
    }

    public virtual void Skill1Callback(InputAction.CallbackContext context)
    {
        // if (PlayerVariables.skillLevel1 >= 1 && cooldown1 <= 0)
        // {
            if(IsOwner)
            {
                StartCoroutine(Stop1());
                Skill1();
                //duration1 = PlayerVariables.Warrior.startDuration1[PlayerVariables.skillLevel1 - 1];
                //cooldown1 = PlayerVariables.Warrior.startCooldown1[PlayerVariables.skillLevel1 - 1];
                //skillAnimator1.SetBool("lock", true);
            }
        // }
        
    }

    public virtual void Skill2Callback(InputAction.CallbackContext context)
    {
        // if (PlayerVariables.skillLevel2 >= 1 && cooldown2 <= 0)
        // {
            if(IsOwner)
            {
                StartCoroutine(Stop2());
                Skill2();
            // duration2 = PlayerVariables.Warrior.startDuration2[PlayerVariables.skillLevel2 - 1];
            // cooldown2 = PlayerVariables.Warrior.startCooldown2[PlayerVariables.skillLevel2 - 1];
            //skillAnimator2.SetBool("lock", true);
            }
        // }
    }

    public IEnumerator Stop1()
    {
        active1 = true;
        yield return new WaitForSeconds(PlayerVariables.Warrior.startDuration1[PlayerVariables.skillLevel1 - 1]);
        active1 = false;
    }
    public IEnumerator Stop2()
    {
        active2 = true;
        yield return new WaitForSeconds(PlayerVariables.Warrior.startDuration2[PlayerVariables.skillLevel2 - 1]);
        active2 = false;
    }

    
    public virtual void Skill1()
    {
        Debug.Log("gab1");
    }




    public virtual void Skill2()
    {
        Debug.Log("gab2");
    }

    protected virtual void Update()
    {
        if (cooldown1 > 0)
        {
            cooldown1 -= Time.deltaTime;
        }

        if (cooldown2 > 0)
        {
            cooldown2 -= Time.deltaTime;
        }

        if (active1)
        {
            duration1 -= Time.deltaTime;
        }

        if (active2)
        {
            duration2 -= Time.deltaTime;
        }
    }
}