using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridDebug : MonoBehaviour {

    public GameObject HeroPrefab;
    public GameObject UnitPrefab;
    private GridModel _gridModel;

    void Awake()
    {
        EventSystem.instance.Connect<UnitEvents.PlaceHeroEvent>(OnPlaceHero);
        EventSystem.instance.Connect<UnitEvents.PlaceUnitEvent>(OnPlaceUnit);
        EventSystem.instance.Connect<DebugEvents.ResetAllRemainingMovementEvent>(OnResetAllRemainingMovement);
        _gridModel = ApplicationFacade.instance.GetModel<GridModel>();
    }

    private void OnDestroy()
    {
        EventSystem.instance.Disconnect<UnitEvents.PlaceHeroEvent>(OnPlaceHero);
        EventSystem.instance.Disconnect<UnitEvents.PlaceUnitEvent>(OnPlaceUnit);
        EventSystem.instance.Disconnect<DebugEvents.ResetAllRemainingMovementEvent>(OnResetAllRemainingMovement);
    }

    private void OnPlaceHero(UnitEvents.PlaceHeroEvent e)
    {
        if (_gridModel != null && _gridModel.Hero == null && _gridModel.SelectedTile != null)
        {
            var hero = Instantiate(HeroPrefab).GetComponent<Unit>();
            hero.Name = "Hero";
            _gridModel.Units.Add(hero);
            hero.OccupyingTile = _gridModel.SelectedTile;

            var heroPosition = _gridModel.SelectedTile.transform.position;
            heroPosition.y += hero.GetComponent<MeshRenderer>().bounds.extents.y;
            hero.transform.position = heroPosition;

            //_gridModel.Hero = hero.GetComponent<Hero>();
            _gridModel.SelectedTile.Selected = false;
        }
    }

    private void OnPlaceUnit(UnitEvents.PlaceUnitEvent e)
    {
        if (_gridModel != null && _gridModel.SelectedTile != null && !_gridModel.SelectedTile.Occupied)
        {
            var unit = Instantiate(UnitPrefab).GetComponent<Unit>();
            unit.Name = GridUtility.GetUniqueUnitName();
            _gridModel.Units.Add(unit);
            unit.OccupyingTile = _gridModel.SelectedTile;

            var heroPosition = _gridModel.SelectedTile.transform.position;
            heroPosition.y += unit.GetComponent<MeshRenderer>().bounds.extents.y;
            unit.transform.position = heroPosition;

            _gridModel.SelectedTile.Selected = false;
        }
    }

    private void OnResetAllRemainingMovement(DebugEvents.ResetAllRemainingMovementEvent e)
    {
        foreach(var unit in _gridModel.Units)
            unit.GetBehavior<Movement>().RemainingMovement = unit.GetBehavior<Movement>().MaxMovement;
    }
}
