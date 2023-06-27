using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public void CanvasOn()
    {
        this.gameObject.SetActive(true);
    }
    
    public void CanvasOff()
    {
        this.gameObject.SetActive(false);
    }
}
