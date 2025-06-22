namespace Project.EntenEller.Base.Scripts.Advanced.Inputs.Pointers
{
    [ExecuteBefore(typeof(EEPointerManager))]
    public class EEPointerObject : EEPointer
    {
        private void OnMouseEnter()
        {
            Enter();
        }

        private void OnMouseExit()
        {
            Exit();
        }
    }
}