﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SubPhases
{

    public class MovementExecutionSubPhase : GenericSubPhase
    {

        public override void Start()
        {
            Game = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
            Name = "Movement";
            RequiredPilotSkill = PreviousSubPhase.RequiredPilotSkill;
            RequiredPlayer = PreviousSubPhase.RequiredPlayer;
            UpdateHelpInfo();
        }

        public override void Next()
        {
            Selection.ThisShip.CheckLandedOnObstacle();

            Selection.ThisShip.FinishMoving();
            Selection.ThisShip.FinishPosition();
            
            Selection.ThisShip.ResetRotationHelpers();

            Selection.ThisShip.IsManeuverPerformed = true;
            Selection.ThisShip.IsAttackPerformed = false;

            //BUG: After movement of third ship in AI-P-AI
            //BUG: After movement of second ship in P-AI
            Selection.ThisShip.AssignedManeuver = null;

            if (Phases.CurrentSubPhase.GetType() == this.GetType())
            {
                GenericSubPhase actionSubPhase = new ActionSubPhase();
                actionSubPhase.PreviousSubPhase = Phases.CurrentSubPhase;
                Phases.CurrentSubPhase = actionSubPhase;
                Phases.CurrentSubPhase.Start();
                Phases.CurrentSubPhase.Initialize();
            }
        }

        public override bool ThisShipCanBeSelected(Ship.GenericShip ship)
        {
            bool result = false;
            return result;
        }

        public override bool AnotherShipCanBeSelected(Ship.GenericShip anotherShip)
        {
            bool result = false;
            return result;
        }

    }

}
