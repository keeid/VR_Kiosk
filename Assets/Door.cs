using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int buildIndex = 0;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            SmartSceneManager.singleton.GoToSceneAsync(buildIndex);
        }
    }
}
