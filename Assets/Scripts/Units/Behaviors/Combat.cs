using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : BaseUnitBehavior {

    public void AttackUnit(Unit target) {

        var totalPower = Owner.Power;
        var targetTotalPower = target.Power;

        //Owner => Target
        if (target.Armor > 0){
            var armor = target.Armor;
            target.Armor -= totalPower;
            totalPower -= armor;
        }
        if(totalPower > 0) {
            target.Health -= totalPower;
        }
        target.InfoPanel.Initialize(target.Armor, target.Power, target.Health, target.GetBehavior<Movement>().RemainingMovement);

        //Target => Owner
        if (Owner.Armor > 0)
        {
            var armor = Owner.Armor;
            Owner.Armor -= targetTotalPower;
            targetTotalPower -= armor;
        }
        if (targetTotalPower > 0)
        {
            Owner.Health -= targetTotalPower;
        }
        Owner.InfoPanel.Initialize(Owner.Armor, Owner.Power, Owner.Health, Owner.GetBehavior<Movement>().RemainingMovement);
    }
}
