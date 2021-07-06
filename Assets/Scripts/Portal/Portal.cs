using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private RoomSwitcher _roomSwitcher;
    [SerializeField] private GameObject _openPortalEffect;

    private BoxCollider _boxCollider;

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

    public void OpenPortal()
    {
        _openPortalEffect.SetActive(true);
        _boxCollider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            _openPortalEffect.SetActive(false);
            _boxCollider.enabled = false;
            _roomSwitcher.SwitchRoom();
        }
    }
}
