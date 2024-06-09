using Controllers;
using UnityEngine;
using UnityEngine.EventSystems;

public class LeftButtonHoldHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public StrandController strandController;

    private bool _isHolding;

    void Update()
    {
        if (_isHolding)
            strandController.MoveLeft();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isHolding = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isHolding = false;
        strandController.MoveUp();
    }
}