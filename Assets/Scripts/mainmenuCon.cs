using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;
using System.Linq;
using UnityEngine.SceneManagement;

public class mainmenuCon : MonoBehaviour
{
    [SerializeField] private userController _userController;

    [Header("Main Menu")]
    [SerializeField] private TMP_Text _userKwText;
    [SerializeField] private TMP_Text _userOrText;
    [Space(10)]

    [Header("Warrior Menu")]
    [SerializeField] private GameObject _warriorMenu;
    [SerializeField] private GameObject _dripContent;
    [SerializeField] private GameObject _warriorMenuCloseButton;
    [SerializeField] private TMP_Text _warriorName, _warriorSkill1Text, _warriorSkill2Text;
    [SerializeField] private Image _warriorHeadImage, _warriorTorsoImage, _warriorLeg1Image, _warriorLeg2Image, _warriorSkill1Image, _warriorSkill2Image, _warriorBackground;
    [Space(10)]

    [Header("Gun Menu")]
    public Image _gunImage;
    [SerializeField] private TMP_Text _gunName;
    [SerializeField] private GameObject _gunMenuCloseButton, _gunColorsContent, _gunMenu;
    [Space(10)]

    [Header("Options Menu")]
    [SerializeField] private GameObject _optionsMenu;
    [Space(10)]

    [Header("Exit Menu")]
    [SerializeField] private GameObject _exitMenu;
    [Space(10)]

    [Header("Buy Menu")]
    [SerializeField] private GameObject _buyMenu;
    [SerializeField] private TMP_Text _productName, _availableKwText, _availableOrText, _kwPriceText, _orPriceText, _skill1Description, _skill2Description;
    [SerializeField] private Toggle _kwPointsToggle, _orPointsToggle;
    [SerializeField] private Image _productImage, _buyMenuBackground, _buyMenuSkillImage1, _buyMenuSkillImage2;
    [Space(10)]

    [Header("All Warriors Guns")]
    [SerializeField] private GameObject _allWarriorsBtn;
    [SerializeField] private GameObject _allGunsBtn;
    [Space(10)]

    [Header("Play Menu")]
    [SerializeField] private GameObject _playMenu;
    [Space(10)]

    [Header("Notification Menu")]
    [SerializeField] private GameObject _notificationMenu;
    [Space(10)]

    [Header("Prefabs")]
    [SerializeField] private GameObject _dripPrefab;
    [SerializeField] private GameObject _gunColorPrefab;
    private int _justBoughtItemID;


    void Start()
    {
        _warriorMenu.SetActive(true);
        _gunMenu.SetActive(true);
        _notificationMenu.SetActive(true);
        UpdateSelectedDrip();
        UpdateSelectedGunImage();
        UpdatePoints();
    }


    #region PLAY-OPTION-EXIT
    public void OpenOptions()
    {
        _optionsMenu.GetComponent<RectTransform>().SetAsLastSibling();

        _optionsMenu.GetComponent<Animator>().SetTrigger("show");
    }
    public void CloseOptions()
    {
        _optionsMenu.GetComponent<Animator>().SetTrigger("hide");
    }

    public void ExitGame()
    {
        _exitMenu.GetComponent<RectTransform>().SetAsLastSibling();
        _exitMenu.GetComponent<Animator>().SetTrigger("show");

        Invoke(nameof(ExitNow), 0.5f);
    }
    private void ExitNow()
    {
        Application.Quit();
    }

    public void Play()
    {
        _playMenu.GetComponent<RectTransform>().SetAsLastSibling();
        _playMenu.GetComponent<Animator>().SetTrigger("show");
        Invoke(nameof(PlayNow), 2.2f);
    }
    private void PlayNow()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    #endregion


    #region WARRIOR_MENU

    public void SelectDrip(int adder)
    {
        var _myIndex = _userController.DripIndex;
        do
        {
            _myIndex += adder;
            if (_myIndex > _userController.WarriorObject.dripIndexes.Length - 1)
                _myIndex = 0;
            else if (_myIndex < 0)
                _myIndex = _userController.WarriorObject.dripIndexes.Length - 1;
        }
        while (userController.UnlockedItems[_userController.WarriorObject.dripIndexes[_myIndex]] == false);
        _userController.DripIndex = _myIndex;

        UpdateSelectedDrip();
    }

    private void UpdateSelectedDrip()
    {
        var warriorObject = _userController.WarriorObject;
        _warriorHeadImage.sprite = Resources.LoadAll<Sprite>("Images/allHeads")[Shop.items[warriorObject.dripIndexes[_userController.DripIndex]].dripID];
        _warriorTorsoImage.sprite = Resources.LoadAll<Sprite>("Images/allTorso")[Shop.items[warriorObject.dripIndexes[_userController.DripIndex]].dripID];
        _warriorLeg1Image.sprite = Resources.LoadAll<Sprite>("Images/allLegs")[Shop.items[warriorObject.dripIndexes[_userController.DripIndex]].dripID];
        _warriorLeg2Image.sprite = Resources.LoadAll<Sprite>("Images/allLegs")[Shop.items[warriorObject.dripIndexes[_userController.DripIndex]].dripID];
        _warriorSkill1Image.color = new Color(warriorObject.warriorColor[0].r, warriorObject.warriorColor[0].g, warriorObject.warriorColor[0].b, 1f);
        _warriorSkill2Image.color = new Color(warriorObject.warriorColor[0].r, warriorObject.warriorColor[0].g, warriorObject.warriorColor[0].b, 1f);
    }

    private void UpdateDripList()
    {
        foreach (Transform child in _dripContent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        for (int i = 0; i < _userController.WarriorObject.dripIndexes.Length; i++)
        {
            var asd = _userController.WarriorObject.dripIndexes[i];

            GameObject dripp = Instantiate(_dripPrefab, transform.position, Quaternion.identity, _dripContent.transform);
            dripp.GetComponentInChildren<TMP_Text>().text = Shop.items[asd].ItemName;
            dripp.GetComponent<Button>().onClick.AddListener(() => OpenBuyMenu(asd));
            dripp.GetComponent<checkIfLocked>().itemIndex = asd;

            foreach (Transform child in dripp.transform)
            {
                if (child.gameObject.name == "dripImage")
                {
                    child.gameObject.GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("Images/allWarriors")[Shop.items[asd].dripID];
                }
            }
        }

    }

    public void SelectWarrior(WarriorObject playerObject)
    {

        _userController.WarriorObject = playerObject;

        var warriorObject = _userController.WarriorObject;

        _userController.DripIndex = 0;
        _warriorSkill1Image.sprite = playerObject.skillImages[0];
        _warriorSkill2Image.sprite = playerObject.skillImages[1];
        _warriorSkill1Text.text = playerObject.skill1;
        _warriorSkill2Text.text = playerObject.skill2;

        UpdateSelectedDrip();

        _allWarriorsBtn.GetComponentInParent<Animator>().SetTrigger("hide");
        _warriorSkill1Image.color = new Color(warriorObject.warriorColor[0].r, warriorObject.warriorColor[0].g, warriorObject.warriorColor[0].b, 1f);
        _warriorSkill2Image.color = new Color(warriorObject.warriorColor[0].r, warriorObject.warriorColor[0].g, warriorObject.warriorColor[0].b, 1f);
        UpdateDripList();
        _warriorBackground.sprite = warriorObject.splashArt;
    }

    private void OpenWarriorMenu()
    {
        UpdateDripList();
        _warriorBackground.sprite = _userController.WarriorObject.splashArt;
        _warriorMenu.GetComponent<RectTransform>().SetAsLastSibling();
        _allWarriorsBtn.GetComponent<RectTransform>().SetAsLastSibling();
        _warriorMenuCloseButton.GetComponent<RectTransform>().SetAsLastSibling();
        _warriorMenu.GetComponent<Animator>().SetTrigger("show");
        _allWarriorsBtn.GetComponent<Animator>().SetTrigger("show");
        _warriorMenuCloseButton.GetComponent<Animator>().SetTrigger("show");
    }
    private void CloseWarriorMenu()
    {
        _warriorMenu.GetComponent<Animator>().SetTrigger("hide");
        _allWarriorsBtn.GetComponent<Animator>().SetTrigger("hide");
        _warriorMenuCloseButton.GetComponent<Animator>().SetTrigger("hide");
    }

    #endregion


    #region GUN_MENU

    private void UpdateSelectedGunImage()
    {
        _gunImage.sprite = _userController.GunObject.UIImage;
        _gunImage.SetNativeSize();
        SetImageColor(Shop.items[_userController.GunObject.colorIndexes[_userController.GunColorIndex]].gunColorHex, _gunImage);
    }

    private void SetImageColor(string hexCode, Image image)
    {
        if (ColorUtility.TryParseHtmlString("#" + hexCode, out Color newColor))
        {
            image.color = newColor;
        }
    }

    public void UpdateGunColorList()
    {
        foreach (Transform child in _gunColorsContent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        for (int i = 0; i < _userController.GunObject.colorIndexes.Length; i++)
        {
            var asd = _userController.GunObject.colorIndexes[i];

            GameObject dripp = Instantiate(_gunColorPrefab, transform.position, Quaternion.identity, _gunColorsContent.transform);
            dripp.GetComponentInChildren<TMP_Text>().text = Shop.items[asd].ItemName;
            dripp.GetComponent<Button>().onClick.AddListener(() => OpenBuyMenu(asd));
            dripp.GetComponent<checkIfLocked>().itemIndex = asd;

            foreach (Transform child in dripp.transform)
            {
                if (child.gameObject.name == "dripImage")
                {
                    child.gameObject.GetComponent<Image>().sprite = Shop.items[asd].gunObj.UIImage;
                    Color asdd;
                    if (ColorUtility.TryParseHtmlString("#" + Shop.items[asd].gunColorHex, out asdd))
                    {
                        child.gameObject.GetComponent<Image>().color = asdd;
                    }
                }
            }
        }
    }

    public void SelectGun(GunObject gunObject)
    {
        _userController.GunObject = gunObject;
        _userController.GunColorIndex = 0;
        _gunName.text = _userController.GunObject.name;

        UpdateSelectedGunImage();

        UpdateGunColorList();
        _allGunsBtn.GetComponentInParent<Animator>().SetTrigger("hide");
    }

    public void SelectGunColor(int adder)
    {
        var _myIndex = _userController.GunColorIndex;
        do
        {
            _myIndex += adder;
            if (_myIndex > _userController.GunObject.colorIndexes.Length - 1)
                _myIndex = 0;
            else if (_myIndex < 0)
                _myIndex = _userController.GunObject.colorIndexes.Length - 1;
        }
        while (userController.UnlockedItems[_userController.GunObject.colorIndexes[_myIndex]] == false);
        _userController.GunColorIndex = _myIndex;

        UpdateSelectedGunImage();

    }

    public void OpenGunMenu()
    {
        UpdateGunColorList();
        _gunMenu.GetComponent<RectTransform>().SetAsLastSibling();
        _gunMenuCloseButton.GetComponent<RectTransform>().SetAsLastSibling();
        _allGunsBtn.GetComponent<RectTransform>().SetAsLastSibling();
        _gunMenu.GetComponent<Animator>().SetTrigger("show");
        _gunMenuCloseButton.GetComponent<Animator>().SetTrigger("show");
        _allGunsBtn.GetComponent<Animator>().SetTrigger("show");
    }

    public void CloseGunMenu()
    {
        _gunMenu.GetComponent<Animator>().SetTrigger("hide");
        _gunMenuCloseButton.GetComponent<Animator>().SetTrigger("hide");
        _allGunsBtn.GetComponent<Animator>().SetTrigger("hide");
    }



    #endregion


    #region ALL-WARRIORS/ALL-GUNS

    public void OpenAllWarriors()
    {
        _allWarriorsBtn.GetComponent<RectTransform>().SetAsLastSibling();
        _allWarriorsBtn.GetComponentInParent<Animator>().SetTrigger("show");
    }
    public void CloseAllWarriors()
    {
        _allWarriorsBtn.GetComponentInParent<Animator>().SetTrigger("hide");
        UpdateDripList();
    }

    public void OpenAllGuns()
    {
        _allGunsBtn.GetComponent<RectTransform>().SetAsLastSibling();
        _allGunsBtn.GetComponentInParent<Animator>().SetTrigger("show");
    }
    public void CloseAllGuns()
    {
        UpdateGunColorList();
        _allGunsBtn.GetComponentInParent<Animator>().SetTrigger("hide");
    }

    #endregion


    #region BUY_MENU

    public void OpenBuyMenu(int itemID)
    {
        var currentItem = Shop.items[itemID];

        if (currentItem.ItemPriceKW != 0)
        {
            _kwPointsToggle.GetComponent<Toggle>().isOn = true;
        }

        _buyMenu.SetActive(true);
        _buyMenu.GetComponent<RectTransform>().SetAsLastSibling();

        if (Shop.items[itemID].playerObj != null)
        {
            //warrior
            SetImageColor("FFFFFF", _productImage);
            _productName.text = currentItem.playerObj.name;
            _kwPriceText.text = currentItem.playerObj.priceKW.ToString();
            _orPriceText.text = currentItem.playerObj.priceO.ToString();
            _buyMenuBackground.color = new Color32(51, 51, 51, 255);
            _buyMenuBackground.sprite = currentItem.playerObj.splashArt;
            _productImage.sprite = Resources.LoadAll<Sprite>("Images/allWarriors")[currentItem.dripID];
            _productImage.transform.localScale = new Vector2(2f, 2f);
            _skill1Description.text = currentItem.playerObj.skill1;
            _skill2Description.text = currentItem.playerObj.skill2;
            _buyMenuSkillImage1.sprite = currentItem.playerObj.skillImages[0];
            _buyMenuSkillImage2.sprite = currentItem.playerObj.skillImages[1];
            _buyMenuSkillImage1.color = new Color(currentItem.playerObj.warriorColor[0].r, currentItem.playerObj.warriorColor[0].g, currentItem.playerObj.warriorColor[0].b, 1f);
            _buyMenuSkillImage2.color = new Color(currentItem.playerObj.warriorColor[0].r, currentItem.playerObj.warriorColor[0].g, currentItem.playerObj.warriorColor[0].b, 1f);
            _buyMenu.GetComponent<Animator>().SetTrigger("show");
        }
        else if (Shop.items[itemID].gunObj != null)
        {
            //gun-guncolor
            SetImageColor(currentItem.gunColorHex, _productImage);
            _productName.text = currentItem.ItemName;
            _kwPriceText.text = currentItem.ItemPriceKW.ToString();
            _orPriceText.text = currentItem.ItemPriceO.ToString();
            _buyMenuBackground.sprite = null;
            _buyMenuBackground.color = new Color32(12, 30, 127, 255);
            _productImage.sprite = currentItem.gunObj.UIImage;
            _productImage.transform.localScale = new Vector2(1.5f, 1.5f);
            _buyMenu.GetComponent<Animator>().SetTrigger("show2");

        }
        else
        {
            //drip
            SetImageColor("FFFFFF", _productImage);
            _productName.text = currentItem.ItemName;
            _kwPriceText.text = currentItem.ItemPriceKW.ToString();
            _orPriceText.text = currentItem.ItemPriceO.ToString();
            _buyMenuBackground.color = new Color32(51, 51, 51, 255);
            _buyMenuBackground.sprite = _userController.WarriorObject.splashArt;
            _productImage.transform.localScale = new Vector2(2f, 2f);
            _productImage.sprite = Resources.LoadAll<Sprite>("Images/allWarriors")[currentItem.dripID];
            _buyMenu.GetComponent<Animator>().SetTrigger("show2");
        }
        _justBoughtItemID = currentItem.ItemID;
        _productImage.SetNativeSize();
        UpdatePaymentMethod();
    }

    private void UpdatePaymentMethod()
    {
        if (_kwPriceText.text == "0")
        {
            _orPointsToggle.isOn = true;
            _kwPointsToggle.interactable = false;
            _kwPointsToggle.gameObject.SetActive(false);
        }
        else
        {
            _kwPointsToggle.gameObject.SetActive(true);
            _kwPointsToggle.interactable = true;
        }
    }

    public void PressBuyButton()
    {
        GameObject[] allItems = GameObject.FindGameObjectsWithTag("item");

        if (_kwPointsToggle.isOn && UserStatsController._kwPoints >= int.Parse(_kwPriceText.text))
        {
            UserStatsController._kwPoints -= int.Parse(_kwPriceText.text);
            userController.UnlockedItems[_justBoughtItemID] = true;
            for (int i = 0; i < allItems.Length; i++)
            {
                if (allItems[i].GetComponent<checkIfLocked>().itemIndex == _justBoughtItemID)
                {
                    if (allItems[i].GetComponent<chooseWarrior>())
                    {
                        allItems[i].GetComponent<chooseWarrior>().choose.SetActive(true);
                    }
                }
            }
            _buyMenu.GetComponent<Animator>().SetTrigger("hide");
            _notificationMenu.GetComponentInChildren<Animator>().SetTrigger("show");
            _notificationMenu.GetComponentInChildren<TMP_Text>().text = "you just bought " + Shop.items[_justBoughtItemID].ItemName + " for " + _kwPriceText.text + " KW.";
        }
        else if (_orPointsToggle.isOn && UserStatsController._outragePoints >= int.Parse(_orPriceText.text))
        {
            UserStatsController._outragePoints -= int.Parse(_orPriceText.text);
            userController.UnlockedItems[_justBoughtItemID] = true;
            for (int i = 0; i < allItems.Length; i++)
            {
                if (allItems[i].GetComponent<checkIfLocked>().itemIndex == _justBoughtItemID)
                {
                    if (allItems[i].GetComponent<chooseWarrior>())
                    {
                        allItems[i].GetComponent<chooseWarrior>().choose.SetActive(true);
                    }
                }
            }
            _buyMenu.GetComponent<Animator>().SetTrigger("hide");
            _notificationMenu.GetComponentInChildren<Animator>().SetTrigger("show");
            _notificationMenu.GetComponentInChildren<TMP_Text>().text = "you just bought " + Shop.items[_justBoughtItemID].ItemName + " for " + _orPriceText.text + " OR.";
        }
        else
        {
            _notificationMenu.GetComponentInChildren<Animator>().SetTrigger("show");
            _notificationMenu.GetComponentInChildren<TMP_Text>().text = "insufficient ammount of money.";
        }
        UpdatePoints();
    }

    private void UpdatePoints()
    {
        _userKwText.text = UserStatsController._kwPoints.ToString() + " kw";
        _availableKwText.text = UserStatsController._kwPoints.ToString() + " kw";
        _userOrText.text = UserStatsController._outragePoints.ToString() + " or";
        _availableOrText.text = UserStatsController._outragePoints.ToString() + " or";
    }

    public void CloseBuyMenu()
    {
        _buyMenu.GetComponent<Animator>().SetTrigger("hide");
    }

    #endregion
}