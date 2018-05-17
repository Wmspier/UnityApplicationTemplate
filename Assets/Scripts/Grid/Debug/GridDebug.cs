using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridDebug : MonoBehaviour {

    public GameObject HeroPrefab;
    private GridModel _gridModel;

    void Awake()
    {
        EventSystem.instance.Connect<UnitEvents.PlaceHeroEvent>(OnPlaceHero);
        EventSystem.instance.Connect<DebugEvents.ResetAllRemainingMovementEvent>(OnResetAllRemainingMovement);
        _gridModel = ApplicationFacade.instance.GetModel<GridModel>();
    }

    private void OnDestroy()
    {
        EventSystem.instance.Disconnect<UnitEvents.PlaceHeroEvent>(OnPlaceHero);
        EventSystem.instance.Disconnect<DebugEvents.ResetAllRemainingMovementEvent>(OnResetAllRemainingMovement);
    }

    private void OnPlaceHero(UnitEvents.PlaceHeroEvent e)
    {
        if (_gridModel != null && _gridModel.Hero == null && _gridModel.SelectedTile != null)
        {
            var hero = Instantiate(HeroPrefab).GetComponent<Hero>();
            hero.Name = "Hero";
            hero.OccupyingTile = _gridModel.SelectedTile;

            var heroPosition = _gridModel.SelectedTile.transform.position;
            heroPosition.y += hero.GetComponent<MeshRenderer>().bounds.extents.y;
            hero.transform.position = heroPosition;

            _gridModel.Units.Add(hero);
            _gridModel.Hero = hero.GetComponent<Hero>();
            _gridModel.SelectedTile.Selected = false;
        }
    }

    private void OnResetAllRemainingMovement(DebugEvents.ResetAllRemainingMovementEvent e)
    {
        foreach(var unit in _gridModel.Units)
            unit.RemainingMovement = unit.MaxMovement;
    }
}
