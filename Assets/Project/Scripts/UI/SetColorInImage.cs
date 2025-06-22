using MPUIKIT;
using UnityEngine;

public class SetColorInImage : MonoBehaviour
{
    [SerializeField] private Color validColor, invalidColor, placeColor;
    [SerializeField] private MPImage mpImage;

    public void SetColorValid() => mpImage.color = validColor;
    public void SetColorInvalid() => mpImage.color = invalidColor;
    public void SetColorPlace() => mpImage.color = placeColor;
}
