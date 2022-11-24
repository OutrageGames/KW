using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;
using FishNet.Object;
using UnityEngine.InputSystem;


public class PlayerUI : NetworkBehaviour
{
    [Header("Head UI")]
    [SerializeField] public Transform HealthBar;
    [SerializeField] public Transform ExperienceBar;
    [SerializeField] private TMP_Text _nameText, _bullets;
    [SerializeField] public TMP_Text LevelText;
    public TMP_Text Username { get => _nameText; set => _nameText = value; }
    [SerializeField] private SpriteRenderer _bulletsImage;
    // [SerializeField] private GameObject _skillUpParticle;
    [Space(10)]

    private Image _skillBar1, _skillBar2, _skillbarBG1, _skillbarBG2, _skillCooldownBar1, _skillCooldownBar2, _skillLock1, _skillLock2, _skillImage1, _skillImage2;
    private Image _durationBar1, _durationBar2;
    private TMP_Text _skillCooldownText1, _skillCooldownText2;
    [SerializeField] private Animator _skillAnimator1, _skillAnimator2;
    private PlayerVariables _playerVars;
    [SerializeField] private Skills _playerSkills;
    private PlayerExperience _playerExperience;
    

 

    public override void OnStartClient()
    {
        base.OnStartClient();
        if(!base.IsOwner)
        {
            _bulletsImage.enabled = false;
            _bullets.enabled = false;
        }

        _playerVars = GetComponent<PlayerVariables>();
        _playerExperience = GetComponent<PlayerExperience>();

        


        _skillBar1 = GameObject.Find("skillbar1").GetComponent<Image>();
        _skillBar2 = GameObject.Find("skillbar2").GetComponent<Image>();
        _skillbarBG1 = GameObject.Find("skillbarBG1").GetComponent<Image>();
        _skillbarBG2 = GameObject.Find("skillbarBG2").GetComponent<Image>();
        _skillCooldownBar1 = GameObject.Find("skillCDImage1").GetComponent<Image>();
        _skillCooldownBar2 = GameObject.Find("skillCDImage2").GetComponent<Image>();
        _skillLock1 = GameObject.Find("skillLock1").GetComponent<Image>();
        _skillLock2 = GameObject.Find("skillLock2").GetComponent<Image>();
        _durationBar1 = GameObject.Find("durationBar1").GetComponent<Image>();
        _durationBar2 = GameObject.Find("durationBar2").GetComponent<Image>();
        _skillImage1 = GameObject.Find("skillImage1").GetComponent<Image>();
        _skillImage2 = GameObject.Find("skillImage2").GetComponent<Image>();
        _skillCooldownText1 = GameObject.Find("skillCDtext1").GetComponent<TMP_Text>();
        _skillCooldownText2 = GameObject.Find("skillCDtext2").GetComponent<TMP_Text>();
        _skillAnimator1 = GameObject.Find("skillAnimator1").GetComponent<Animator>();
        _skillAnimator2 = GameObject.Find("skillAnimator2").GetComponent<Animator>();

        if (IsOwner)
        {
            _skillImage1.sprite = _playerVars.Warrior.skillImages[0];
            _skillImage2.sprite = _playerVars.Warrior.skillImages[1];

            _skillLock1.enabled = false;
            _skillLock2.enabled = false;
            _skillCooldownText1.enabled = false;
            _skillCooldownText2.enabled = false;


            //userController con = GameObject.FindGameObjectWithTag("controller").GetComponent<userController>();

            _durationBar2.GetComponent<Image>().color = _playerVars.Warrior.warriorColor[0];
            _durationBar2.GetComponent<Image>().color = new Color(_playerVars.WarriorColor.r, _playerVars.WarriorColor.g,
                _playerVars.WarriorColor.b, 0.2f);
            _durationBar1.GetComponent<Image>().color = _playerVars.WarriorColor;
            _durationBar1.GetComponent<Image>().color = new Color(_playerVars.WarriorColor.r, _playerVars.WarriorColor.g,
                _playerVars.WarriorColor.b, 0.2f);

            _skillImage1.GetComponent<Image>().color = new Color(_playerVars.WarriorColor.r, _playerVars.WarriorColor.g,
                _playerVars.WarriorColor.b, 0.5f);
            _skillImage2.GetComponent<Image>().color = new Color(_playerVars.WarriorColor.r, _playerVars.WarriorColor.g,
                _playerVars.WarriorColor.b, 0.5f);

            _skillbarBG1.GetComponent<Image>().color = new Color(_playerVars.WarriorColor.r, _playerVars.WarriorColor.g,
                _playerVars.WarriorColor.b, 0.5f);
            _skillbarBG2.GetComponent<Image>().color = new Color(_playerVars.WarriorColor.r, _playerVars.WarriorColor.g,
                _playerVars.WarriorColor.b, 0.5f);
        }

        
    }

    void Start()
    {
        if (GetComponent<Skills>())
        {
            _playerSkills = GetComponent<Skills>();
        }
    }

    private void Update()
    {
        
        // levelBar.fillAmount = Mathf.Lerp(0.22f, 0.488f, (_playerVars.currentXp / 100f) / (_playerVars.nextXp / 100f));
        // levelBar.fillAmount = (_playerVars.currentXp / 100f) / (_playerVars.nextXp / 100f);
        

        if (IsOwner)
        {
            UpdateSkillset();
            _bullets.text = GetComponentInChildren<Gun>().currentBullets.ToString();
        }
        
        //hpBar.fillAmount = Mathf.Lerp(0.22f, 1f, playerVars.hp / 100f) / (playerVars.fullHp / 100f);

        
        //if (playerVars.ccDuration <= 0)
        //{
        //    ccBar.localScale = new Vector2(0f, ccBar.localScale.y);
        //    ccText.enabled = false;
        //}
        //else
        //{
        //    ccBar.localScale = new Vector2(playerVars.ccDuration / playerVars.ccStart, ccBar.localScale.y);
        //    ccText.enabled = true;
        //}


        //ccText.text = playerVars.currentCC;

        if (IsOwner)
        {
            if (_playerVars.skillLevel1 > 0)
            {
                _skillAnimator1.SetTrigger("start");
            }
            if (_playerVars.skillLevel2 > 0)
            {
                _skillAnimator2.SetTrigger("start");
            }

            if (_playerSkills.cooldown1 > 0f)
            {
                _skillLock1.enabled = true;
                _skillCooldownText1.enabled = true;
                _skillAnimator1.SetBool("lock", true);
            }

            if (_playerSkills.cooldown1 < 0.1f)
            {
                _skillLock1.enabled = false;
                _skillCooldownText1.enabled = false;
                _skillAnimator1.SetBool("lock", false);
            }

            if (_playerSkills.cooldown2 > 0f)
            {
                _skillLock2.enabled = true;
                _skillCooldownText2.enabled = true;
                _skillAnimator2.SetBool("lock", true);
            }

            if (_playerSkills.cooldown2 < 0.1f)
            {
                _skillLock2.enabled = false;
                _skillCooldownText2.enabled = false;
                _skillAnimator2.SetBool("lock", false);
            }

            _skillCooldownText1.text = _playerSkills.cooldown1.ToString("f1");
            _skillCooldownText2.text = _playerSkills.cooldown2.ToString("f1");

            if (_playerVars.skillLevel1 > 0)
            {
                _skillCooldownBar1.fillAmount = _playerSkills.cooldown1 / _playerVars.Warrior.startCooldown1[_playerVars.skillLevel1 - 1];
                _durationBar1.fillAmount = _playerSkills.duration1 / _playerVars.Warrior.startDuration1[_playerVars.skillLevel1 - 1];
            }
            if (_playerVars.skillLevel2 > 0)
            {
                _skillCooldownBar2.fillAmount = _playerSkills.cooldown2 / _playerVars.Warrior.startCooldown2[_playerVars.skillLevel2 - 1];
                _durationBar2.fillAmount = _playerSkills.duration2 / _playerVars.Warrior.startDuration2[_playerVars.skillLevel2 - 1];
            }
        }
    }
    public void UpdateSkillset()
    {
        switch (_playerExperience.Level)
        {
            case 1:
                _skillBar1.fillAmount = 0f;
                _skillBar2.fillAmount = 0f;
                break;
            case 2:
                _playerVars.skillLevel1 = 1;
                _skillBar1.fillAmount = 0.35f;
                // Instantiate(_skillUpParticle, Camera.main.ScreenToWorldPoint(_skillAnimator1.transform.position), Quaternion.identity, Camera.main.transform);
                break;
            case 3:
                _playerVars.skillLevel1 = 2;
                _skillBar1.fillAmount = 0.64f;
                // Instantiate(_skillUpParticle, Camera.main.ScreenToWorldPoint(_skillAnimator1.transform.position), Quaternion.identity, Camera.main.transform);
                break;
            case 4:
                _playerVars.skillLevel2 = 1;
                _skillBar2.fillAmount = 0.35f;
                // Instantiate(_skillUpParticle, Camera.main.ScreenToWorldPoint(_skillAnimator2.transform.position), Quaternion.identity, Camera.main.transform);
                break;
            case 5:
                _playerVars.skillLevel1 = 3;
                _skillBar1.fillAmount = 1f;
                // Instantiate(_skillUpParticle, Camera.main.ScreenToWorldPoint(_skillAnimator1.transform.position), Quaternion.identity, Camera.main.transform);
                break;
            case 6:
                _playerVars.skillLevel2 = 2;
                _skillBar2.fillAmount = 0.64f;
                // Instantiate(_skillUpParticle, Camera.main.ScreenToWorldPoint(_skillAnimator2.transform.position), Quaternion.identity, Camera.main.transform);
                break;
            case 7:
                _playerVars.skillLevel2 = 3;
                _skillBar2.fillAmount = 1f;
                // Instantiate(_skillUpParticle, Camera.main.ScreenToWorldPoint(_skillAnimator2.transform.position), Quaternion.identity, Camera.main.transform);
                break;
        }
    }
}
