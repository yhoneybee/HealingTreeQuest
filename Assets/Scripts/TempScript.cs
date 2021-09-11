using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TempScript : MonoBehaviour, IPointerClickHandler
{
    public Tree tree;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        tree.LevelUp();
    }

}
