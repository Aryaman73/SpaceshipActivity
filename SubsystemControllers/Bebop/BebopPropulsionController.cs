using Godot;
using System;

public class BebopPropulsionController : AbstractPropulsionController
{
    BebopSensorsController SensorsController {get{return parentShip.SensorsController as BebopSensorsController;}}    
    BebopNavigationController NavigationController {get{return parentShip.NavigationController as BebopNavigationController;}}    
    BebopDefenceController DefenceController {get{return parentShip.DefenceController as BebopDefenceController;}}
    public Boolean areTorpedosAvailable;
    public override void PropulsionUpdate(ShipStatusInfo shipStatusInfo, ThrusterControls thrusters, float deltaTime)
    {
        //Student code goes here

        //Enable the UFO drive override
        thrusters.IsUFODriveEnabled = true;
        
        // fly down and to the right at a speed of 141 pixels per second
        Vector2 velocity = new Vector2(0, -100);
        thrusters.UFODriveVelocity = velocity;

    }

    public override void DebugDraw(Font font)
    {
        //Student code goes here
    }
}
