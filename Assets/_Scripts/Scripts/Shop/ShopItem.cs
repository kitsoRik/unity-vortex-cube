using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopItem : MonoBehaviour
{
    public int Number;
    public Transform ParentShopObject;
    public GameObject ShopObject;

    public void SetSettings(int _number, GameObject _prefab, LayerMask _shopLayer)
    {
        string _name = _number.ToString();
        Number = _number;
        ParentShopObject.name = _name;
        SetObject(_name, _prefab, _shopLayer);
    }

    private void SetObject(string _name, GameObject _prefab, LayerMask _shopLayer)
    {
        ShopObject = Instantiate(_prefab, ParentShopObject);
        ShopObject.transform.localPosition = Shop.shopObjectsPositions;
        ShopObject.transform.localScale = Shop.sizeShopobject;
        ShopObject.GetComponent<PlayerStats>().SetShopRotation();
        ShopObject.layer = _shopLayer;

        Transform[] _shopObject = ShopObject.GetComponentsInChildren<Transform>();

        foreach (Transform _go in _shopObject)
        {
            _go.gameObject.layer = _shopLayer;
            _go.name = _name;
        }
    }
}
