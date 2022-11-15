using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Skills : MonoBehaviour
{
    // private InputMaster controls;
    public float cooldown1, cooldown2, duration1, duration2;
    public bool active1, active2;
    //public GameObject[] skillObjects;
    //public float[] actualCooldown1, actualCooldown2, skillDamages1, skillDamages2, startDuration1, startDuration2;
    // public playerVariables playerVars;
    // public PlayerStatsController _playerStatsController;
    // Animator skillAnimator1, skillAnimator2;

    // private PlayerCameraController _cameraController;

    // public PlayerCameraController PlayerCameraController { get => _cameraController; }

    private void Awake()
    {
        // playerVars = GetComponent<playerVariables>();
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

    public virtual void DoAbility1(InputAction.CallbackContext context)
    {
        // if (playerVars.skillLevel1 >= 1 && cooldown1 <= 0)
        // {
        //     StartCoroutine(Stop1());
        //     Ability1();
        //     duration1 = playerVars.WarriorObject.startDuration1[playerVars.skillLevel1 - 1];
        //     cooldown1 = playerVars.WarriorObject.startCooldown1[playerVars.skillLevel1 - 1];
        //     skillAnimator1.SetBool("lock", true);
        // }
    }

    public virtual void DoAbility2(InputAction.CallbackContext context)
    {
        // if (playerVars.skillLevel2 >= 1 && cooldown2 <= 0)
        // {
        //     StartCoroutine(Stop2());
        //     Ability2();
        //     duration2 = playerVars.WarriorObject.startDuration2[playerVars.skillLevel2 - 1];
        //     cooldown2 = playerVars.WarriorObject.startCooldown2[playerVars.skillLevel2 - 1];
        //     skillAnimator2.SetBool("lock", true);
        // }
    }

    // public IEnumerator Stop1()
    // {
    //     active1 = true;
    //     yield return new WaitForSeconds(playerVars.WarriorObject.startDuration1[playerVars.skillLevel1 - 1]);
    //     active1 = false;
    // }
    // public IEnumerator Stop2()
    // {
    //     active2 = true;
    //     yield return new WaitForSeconds(playerVars.WarriorObject.startDuration2[playerVars.skillLevel2 - 1]);
    //     active2 = false;
    // }

    public virtual void Ability1()
    {
        Debug.Log("gab1");
    }
    public virtual void Ability2()
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

    private void OnEnable()
    {
        // controls.Enable();
    }
    private void OnDisable()
    {
        // controls.Disable();
    }
}