using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelAsset : MonoBehaviour
{
    [SerializeField]
    private Text _textName;
    [SerializeField]
    private Text _textType;
    [SerializeField]
    private Button _buttonLoad;
    [SerializeField]
    private Object _object;

    public void Initialize(Object obj)
    {
        _textName.text = obj.name;
        string type = obj.GetType().Name;
        _textType.text = type;
        if (type != "GameObject")
        {
            _buttonLoad.gameObject.SetActive(false);
        }
        _object = obj;
    }
    public void LoadAndInstantiate()
    {
        if (_object != null)
        {
            Instantiate(_object);
        }
        
    }
}
