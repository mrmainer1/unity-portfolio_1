using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Transforms.Simple
{
    public class EETransform : EEBehaviour
    {
        public void SetPosition(Vector3 newPosition) => transform.position = newPosition;
        public void SetPosition(float x, float y, float z) => transform.position = new Vector3(x, y, z);
        public void SetPositionX(float x) => transform.position = new Vector3(x, transform.position.y, transform.position.z);
        public void SetPositionY(float y) => transform.position = new Vector3(transform.position.x, y, transform.position.z);
        public void SetPositionZ(float z) => transform.position = new Vector3(transform.position.x, transform.position.y, z);
        
        public void SetLocalPosition(Vector3 newPosition) => transform.localPosition = newPosition;
        public void SetLocalPosition(float x, float y, float z) => transform.localPosition = new Vector3(x, y, z);
        public void SetLocalPositionX(float x) => transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
        public void SetLocalPositionY(float y) => transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
        public void SetLocalPositionZ(float z) => transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, z);
        
        public void SetRotation(Quaternion newRotation) => transform.rotation = newRotation;
        public void SetRotation(Vector3 eulerAngles) => transform.rotation = Quaternion.Euler(eulerAngles);
        public void SetRotation(float x, float y, float z) => transform.rotation = Quaternion.Euler(x, y, z);
        
        public void SetLocalRotation(Quaternion newRotation) => transform.localRotation = newRotation;
        public void SetLocalRotation(Vector3 eulerAngles) => transform.localRotation = Quaternion.Euler(eulerAngles);
        public void SetLocalRotation(float x, float y, float z) => transform.localRotation = Quaternion.Euler(x, y, z);
        
        public void SetScale(Vector3 newScale) => transform.localScale = newScale;
        public void SetScale(float x, float y, float z) => transform.localScale = new Vector3(x, y, z);
        public void SetScaleX(float x) => transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
        public void SetScaleY(float y) => transform.localScale = new Vector3(transform.localScale.x, y, transform.localScale.z);
        public void SetScaleZ(float z) => transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, z);
    }
}
