using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class IconToggle : MonoBehaviour
{
    [SerializeField] private Sprite _iconEnable;
    [SerializeField] private Sprite _iconDisable;

    private bool _iconState = true;

    Image _iconImage;

    // Start is called before the first frame update
    void Start()
    {
        _iconImage = GetComponent<Image>();
        _iconImage.sprite = _iconState ? _iconEnable : _iconDisable;

    }
    
    public void ToggleIcon(bool enable)
    {
        _iconState = enable;
        _iconImage.sprite = _iconState ? _iconEnable : _iconDisable;
    }
}
