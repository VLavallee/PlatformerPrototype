using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalScroll : MonoBehaviour
{
    [SerializeField] float scrollRate = 1f;

    private void Update()
    {
        transform.Translate(0, scrollRate * Time.deltaTime, 0);
    }
}
