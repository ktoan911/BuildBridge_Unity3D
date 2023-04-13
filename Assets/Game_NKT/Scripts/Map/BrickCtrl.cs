using UnityEngine;

public class BrickCtrl : MonoBehaviour
{
    [SerializeField] private ColorData colorData;

    public ColorType colorType;
    public void SetBrickColor(int numberEnum)
    {       
        GetComponent<Renderer>().material = colorData.GetColor(numberEnum);
        colorType = (ColorType)numberEnum;
    }

    public bool IsActive()
    {
        return this.gameObject.activeSelf;
    }
}
