using Godot;
using System;
using System.Collections.Generic;

public class BebopSensorsController : AbstractSensorsController
{
    BebopNavigationController NavigationController {get{return parentShip.NavigationController as BebopNavigationController;}}    
    BebopPropulsionController PropulsionController {get{return parentShip.PropulsionController as BebopPropulsionController;}}    
    BebopDefenceController DefenceController {get{return parentShip.DefenceController as BebopDefenceController;}}

    public class EMSReport {
        public EMSReport(float xin, float yin) {
            x = xin;
            y = yin;
        }
        public float x { get; private set; }
        public float y { get; private set; }
    }
    float timePassed = 0;
    public override void SensorsUpdate(ShipStatusInfo shipStatusInfo, IActiveSensors activeSensors, PassiveSensors passiveSensors, float deltaTime)
    {
        //Student code goes here   
        timePassed += deltaTime;
        if (timePassed >= 1) {
            timePassed = 0;
            List<EMSReading> spaceObj;

            spaceObj = activeSensors.PerformScan(shipStatusInfo.forwardVector.Angle(), 2*Mathf.Pi, 500);

            DefenceController.ls_asteroids.Clear();
            for (int index = 0; index < spaceObj.Count; index++) {
                if (spaceObj[index].ScanSignature == "Rock:90|Common:10") {
                    float x = spaceObj[index].Amplitude * activeSensors.GConstant * Mathf.Cos((spaceObj[index].Angle));
                    float y = spaceObj[index].Amplitude * activeSensors.GConstant * Mathf.Sin((spaceObj[index].Angle));
                    x += shipStatusInfo.positionWithinSystem.x;
                    y += shipStatusInfo.positionWithinSystem.y;
                    DefenceController.ls_asteroids.Add(new EMSReport(x, y));
                }
            }
        }
    }

    public override void DebugDraw(Font font)
    {
        //Student code goes here
    }
}
