#pragma warning disable IDE1006

using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour {

    private const int rightWithCrystal = 150, rightWithoutCrystal = 50;

    public static Vector3 sizeShopobject = new Vector3(100, 100, 100);

    public Control control;
    public ShopObjects shopObjects;
    public ShopItem shopItem;
    public List<ShopItem> shopItems = new List<ShopItem>();
    public Animator animator;
    public MainMenu mainMenu;
    public GameObject ShopPanel;
    public Transform ParentShopObject, Content;
    public Camera shopCamera;

    public ToggleR RandomCharacterToggleR;
    public Button BuyButton;
    public HorizontalLayoutGroup BuyButtonHLG;
    public GameObject CrystalOnButton;
    public TextMeshProUGUI BuyButtonText, NeedCompleteMissions;

    private Vector3 randomRotationShopCube;
    public static Vector3 shopObjectsPositions = new Vector3(0, 0, -50);

    private int nowObject = 0;

    private bool shopIsActive
    {
        get
        {
            return ShopPanel.activeSelf;
        }
    }

    public static bool RandomCharacter { get; set; } 
    public static string ShopHasObjects { get; set; }

    private void Awake()
    {
        randomRotationShopCube = new Vector3(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
        SetShopItems();
        SetSettingsOfObject(ShopHasObjects.IndexOf('S'));

        RandomCharacterToggleR.whenOff = RandomCharacterToggleOff;
    }

    private void RandomCharacterToggleOff()
    {
        //control.ChangePlayerCube(shopObjects.Objects[ShopHasObjects.IndexOf('S')]);
    }

    private void Update()
    {
        if (!shopIsActive)
           return;
        JustRotateShopCube();
        ClickShopObjects();
    }

    private void SetShopItems()
    {
        int _length = shopObjects.Objects.Length;
        LayerMask layerShop = LayerMask.NameToLayer("ShopObject");
        for (int i = 0; i < _length; i++)
        {
            ShopItem _shopItem = Instantiate(shopItem, Content);
            _shopItem.SetSettings(i, shopObjects.Objects[i], layerShop);
            shopItems.Add(_shopItem);
        }
    }

    public void Starting()
    {
        ShopPanel.SetActive(true);
    }

    private void SetSettingsOfObject(int number)
    {
        shopItems[nowObject].ParentShopObject.GetComponent<Outline>().effectColor = Color.black;
        shopItems[number].ParentShopObject.GetComponent<Outline>().effectColor = Color.yellow;
        nowObject = number;
        string _shopHasObjects = ExpandSaveObjects(number);

        if (_shopHasObjects[number] == 'T')
        {
            NeedCompleteMissions.gameObject.SetActive(false);
            BuyButton.interactable = true;
            SetCrystal(false);
            BuyButtonText.text = Lang.Phrase("Set");
            BuyButton.gameObject.SetActive(true);
        }
        else if(_shopHasObjects[number] != 'S')
        {
            int _price = GetPrice(number);
            int _needCompleteMissions = GetNeedCompleteMissions(number);

            BuyButtonText.text = _price.ToString();

            SetCrystal(true);
            if (_needCompleteMissions < GameManager.NowMission)
            {
                NeedCompleteMissions.gameObject.SetActive(false);
                if (_price <= GameManager.Money)
                {
                    BuyButton.interactable = true;
                }
                else
                {
                    BuyButton.interactable = false;
                }
            }else
            {
                NeedCompleteMissions.text = Lang.Phrase("Need complete {0} missions", _needCompleteMissions);
                NeedCompleteMissions.gameObject.SetActive(true);
                BuyButton.interactable = false;
            }
            BuyButton.gameObject.SetActive(true);
        } 
        else
        {
            NeedCompleteMissions.gameObject.SetActive(false);
            BuyButton.gameObject.SetActive(false);
        }

        for (int i = 0; i < ParentShopObject.childCount; i++)
            Destroy(ParentShopObject.GetChild(0).gameObject);

        GameObject _goShop = Instantiate(shopObjects.Objects[number]);
        _goShop.transform.parent = ParentShopObject;
        _goShop.transform.localScale = new Vector3(1, 1, 1);
        _goShop.transform.localPosition = Vector3.zero;
        _goShop.transform.localRotation = ParentShopObject.localRotation;
 
        LayerMask _shopLayer = LayerMask.NameToLayer("Shop");

        Transform[] _shopObject = _goShop.GetComponentsInChildren<Transform>();

        foreach (Transform _go in _shopObject)
        {
            _go.gameObject.layer = _shopLayer;
        }
    }

    private void JustRotateShopCube()
    {
        ParentShopObject.rotation = Quaternion.Slerp(ParentShopObject.rotation, ParentShopObject.rotation * Quaternion.Euler(randomRotationShopCube), 0.5f);
        if (UnityEngine.Random.Range(0, 100) == 0)
            randomRotationShopCube = new Vector3(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
    }

    private void SetCrystal(bool set)
    {
        if (set)
        {
            BuyButtonHLG.padding.right = rightWithCrystal;
            CrystalOnButton.gameObject.SetActive(true);
            return;
        }

        BuyButtonHLG.padding.right = rightWithoutCrystal;
        CrystalOnButton.gameObject.SetActive(false);
    }

    private void ClickShopObjects()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray _ray = shopCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit _hit;
            if (Physics.Raycast(_ray, out _hit, 50) && _hit.transform.gameObject.layer == LayerMask.NameToLayer("ShopObject"))
            {
                SetSettingsOfObject(int.Parse(_hit.transform.name));
            }
        }
    }

    public void ClickBuySet()
    {
        if (ShopHasObjects[nowObject] == 'T')
            Set();
        else
            Buy();
    }

    private void Set()
    {
        ChangeSaveObjects(false, nowObject);

        //GameManager.Save();

        SetSettingsOfObject(nowObject);

        control.ChangePlayerCube(shopObjects.Objects[nowObject]);
    }

    private void Buy()
    {
        ChangeSaveObjects(true, nowObject);

        GameManager.Money -= GetPrice(nowObject);
       // GameManager.Save();

        SetSettingsOfObject(nowObject);
        control.AddRandomCharacterIndex((byte)nowObject);

        mainMenu.SetMoney(GameManager.Money);
    }

    private void ChangeSaveObjects(bool buy, int number)
    {
        string _shopHasObjects = ShopHasObjects;

        if(!buy)
            _shopHasObjects = _shopHasObjects.Replace('S', 'T');

        _shopHasObjects = _shopHasObjects.Remove(nowObject, 1);
        _shopHasObjects = _shopHasObjects.Insert(nowObject, buy ? "T" : "S");

        ShopHasObjects = _shopHasObjects;
    }

    private string ExpandSaveObjects(int number)
    {
        if (ShopHasObjects.Length < number + 1)
        {
            ShopHasObjects = ShopHasObjects.Insert(ShopHasObjects.Length, Helper.MultChar('F', (number + 1) - (ShopHasObjects.Length)));
        }

        return ShopHasObjects;
    }

    public void Exit()
    {
        mainMenu.Starting("Shop");
        animator.Play("ShopExit");
    }

    public int GetNeedCompleteMissions(int number)
    {
        switch (number)
        {
            case 0: return -1;
            case 1: 
            case 2: return 2;
            case 3: return 3;
            case 4: return 4;
            case 5: return 5;
            case 6: 
            case 7: 
            case 8: 
            case 9: 
            case 10: return 10;
            case 11: 
            case 12: 
            case 13: 
            case 14: 
            case 15: return 15;
            case 16: 
            case 17: 
            case 18: 
            case 19: 
            case 20: return 25;
            case 21: 
            case 22: 
            case 23: return 30;
            case 24: 
            case 25: return 35;
            case 26: return 40;
            case 27: return 45;
            case 28: return 50;
            default: return 50;
        }
    }

    public int GetPrice(int number)
    {
        switch (number)
        {
            case 0: return 0;
            case 1: return 10;
            case 2: return 25;
            case 3: return 50;
            case 4: return 70;
            case 5: return 75;
            case 6: return 75;
            case 7: return 75;
            case 8: return 75;
            case 9: return 75;
            case 10: return 75;
            case 11: return 75;
            case 12: return 100;
            case 13: return 100;
            case 14: return 100;
            case 15: return 150;
            case 16: return 200;
            case 17: return 200;
            case 18: return 250;
            case 19: return 350;
            case 20: return 500;
            case 21: return 500;
            case 22: return 600;
            case 23: return 700;
            case 24: return 700;
            case 25: return 1000;
            case 26: return 1500;
            case 27: return 5000;
            case 28: return 10000;
            default: return 228228;
        }
    }
}

public class ShopVariables
{
    public string _shopHasObjects;
    public bool _randomCharacter;

    public ShopVariables()
    {
        _shopHasObjects = "SF";
    }

    public ShopVariables(string __shopHasObjects, bool __randomCharacter)
    {
        _shopHasObjects = __shopHasObjects;
        _randomCharacter = __randomCharacter;
    }
}
