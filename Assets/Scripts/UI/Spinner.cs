using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] Unit _player;

    private void Update()
    {
        transform.Rotate(0f, 0.5f, 0f * Time.deltaTime);
    }
}
