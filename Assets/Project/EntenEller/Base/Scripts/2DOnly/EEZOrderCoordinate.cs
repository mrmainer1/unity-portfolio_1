namespace Project.EntenEller.Base.Scripts._2DOnly
{
    public class EEZOrderCoordinate : EEZOrderBase
    {
        protected override void SetZ()
        {
            var pos = Transform.position;
            pos.z = pos.y;
            Transform.position = pos;
        }
    }
}
