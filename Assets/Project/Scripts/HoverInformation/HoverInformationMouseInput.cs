using Project.EntenEller.Base.Scripts.Advanced.Behaviours.Loop;
using Project.EntenEller.Base.Scripts.Advanced.GameObjects;
using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Advanced.Tags;
using Project.EntenEller.Base.Scripts.Patterns.Pool;
using Project.Scripts.Builder;
using Project.Scripts.Builder.Building;
using Project.Scripts.Map.Cell;
using UnityEngine;

public class HoverInformationMouseInput : EEBehaviourUpdate
{
    [SerializeField] private HoverInformationView hoverInformationView;
    [SerializeField] private MapCellHolder mapCellHolderRaw;
    public EENotifier HaveTargetNotifier;
    public EENotifier NotHaveTargetNotifier;
    public EETagHolder point;

    protected override void EEUpdate()
    {
        base.EEUpdate();
        var mapCell = MapCell.GetCellByXY(mapCellHolderRaw.Cell.x, mapCellHolderRaw.Cell.y);
        if (mapCell == null)
        {
            NotHaveTargetNotifier.Notify();
            return;
        }
        if (mapCell.Building == null)
        {
            NotHaveTargetNotifier.Notify();
            return;
        }
        point = EETagUtils.FindEETagInChildren(mapCell.Building.GetSelf<EEPoolObject>(), "tag-ui-point");
        var information = mapCell.Building.GetSelf<BuildingInformation>();
        hoverInformationView.SetInformation(information.Username, information.CarNumber);
        hoverInformationView.SetTarget(point.GetEEGameObject());
        HaveTargetNotifier.Notify();
    }
}
