using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.Patterns.Singleton;
using Project.Scripts.Map;
using UnityEngine;

public class CameraInitilization : EEBehaviour
{
    protected override void EEAwake()
    {
        base.EEAwake();
        EESingleton.Get<MapResizer>().ChangeSizeFinishNotifier.Event += Init;
    }

    protected override void EEDestroy()
    {
        base.EEDestroy();
        EESingleton.Get<MapResizer>().ChangeSizeFinishNotifier.Event -= Init;
    }

    public void Init()
    {
        var mapSize = EESingleton.Get<MapResizer>();
        transform.position = new Vector3(mapSize.Width / 2, 0, mapSize.Height / 2);
    }
}
