using Godot;
using System;
using System.Collections.Generic;


public class BebopDefenceController : AbstractDefenceController
{
    BebopSensorsController SensorsController {get{return parentShip.SensorsController as BebopSensorsController;}}
    BebopNavigationController NavigationController {get{return parentShip.NavigationController as BebopNavigationController;}}
    BebopPropulsionController PropulsionController {get{return parentShip.PropulsionController as BebopPropulsionController;}}


    public List<BebopSensorsController.EMSReport> ls_asteroids = new List<BebopSensorsController.EMSReport>();
     
    public override void DefenceUpdate(ShipStatusInfo shipStatusInfo, TurretControls turretControls, float deltaTime)
    {
        cannotShootCheck(turretControls);

        if (ls_asteroids.Count!=0) //check if the list is empty
        {
            locationAsteroid(turretControls, shipStatusInfo);
            fireTorpedo(turretControls); 
        }       
    }

    public override void DebugDraw(Font font)
    {
        //Student code goes here
    }

    public void cannotShootCheck(TurretControls turretControls) {
        //if 
        for (int i=0; i < 4; i++) {
            if (turretControls.GetTubeCooldown(i) == 0) {
                PropulsionController.areTorpedosAvailable = true;
                return;
            }
        }
        PropulsionController.areTorpedosAvailable = false;
    }

    private void fireTorpedo(TurretControls turretControls) { //Fire one single torpedo 
        for(int i=0; i < 4; ++i) {
            if (turretControls.GetTubeCooldown(i) == 0) { //Finds the first tube which isn't on cooldown
                turretControls.TriggerTube(i, -1);
                return;
            }
        }       
    }

    private void fireAllTorpedo(TurretControls turretControls)  //All 4 torpedos, probably not that useful?
    {
        for(int i=0; i < 4; ++i) {
            turretControls.TriggerTube(i, -1);
        }
            
    }
     private void locationAsteroid (TurretControls turretControls, ShipStatusInfo shipStatusInfo){
       // double amplitude = (double)ls_asteroids[0].Amplitude;
       // double angle = (double)ls_asteroids[0].Angle;
       // double x = Math.Cos(angle)*amplitude;  
       // double y = Math.Sin(angle)*amplitude;  
      int index =  shortestDistance(shipStatusInfo); 
       double x = (double)ls_asteroids[index].x;
       double y = (double)ls_asteroids[index].y;
        turretControls.aimTo = new Vector2((float)x,(float)y); 
        ls_asteroids.RemoveAt(index);
    }

private int shortestDistance (ShipStatusInfo shipStatusInfo) {
    double distance = 100000000000000.0 ; 
    int index = 0 ;
         for(int i=0; i < ls_asteroids.Count; ++i) {
        double x_asteroid = (double)ls_asteroids[0].x;  
        double y_asteroid = (double)ls_asteroids[0].y;  
        double distance_ship_x = shipStatusInfo.positionWithinSystem.x ;
        double distance_ship_y = shipStatusInfo.positionWithinSystem.y ;
        double relative_distance_x = distance_ship_x - x_asteroid ;
        double relative_distance_y = distance_ship_y - y_asteroid ;
        double relative_distance = Math.Sqrt(((relative_distance_x*relative_distance_x)+ (relative_distance_y+relative_distance_y)));
        if (relative_distance<distance)
            {distance = relative_distance ;
            index = i; 
             }
        
        }
        return index; 
    }
}