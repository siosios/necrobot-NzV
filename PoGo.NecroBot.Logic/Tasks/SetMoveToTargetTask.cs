﻿#region using directives

using PoGo.NecroBot.Logic.Event;
using PoGo.NecroBot.Logic.State;
using POGOProtos.Map.Fort;
using POGOProtos.Networking.Responses;

#endregion

namespace PoGo.NecroBot.Logic.Tasks
{

    public class SetMoveToTargetTask
    {
        public static bool SetMoveToTargetEnabled { get; set; }
        public static bool SetMoveToTargetAccept { get; set; }
        public static double SetMoveToTargetLat { get; set; }
        public static double SetMoveToTargetLng { get; set; }


        public static void CheckSetMoveToTargetStatus(ref FortDetailsResponse fortInfo, ref FortData pokeStop)
        {
            if (!SetMoveToTargetEnabled)
                return;
            SetMoveToTargetAccept = true;
            fortInfo.Name = "User Destination.";
            fortInfo.Latitude = pokeStop.Latitude = SetMoveToTargetLat;
            fortInfo.Longitude = pokeStop.Longitude = SetMoveToTargetLng;
        }
        public static bool CheckStopforSetMoveToTarget()
        {
            return SetMoveToTargetEnabled && !SetMoveToTargetAccept;
        }
        public static bool CheckReachTarget(ISession session)
        {
            if (!SetMoveToTargetEnabled || !SetMoveToTargetAccept)
                return false;
            session.EventDispatcher.Send(new FortUsedEvent
            {
                Id = "",
                Name = "User Destination.",
                Exp = 0,
                Gems = 0,
                Items = "",
                Latitude = SetMoveToTargetLat,
                Longitude = SetMoveToTargetLng,
                InventoryFull = false
            });
            SetMoveToTargetAccept = false;
            SetMoveToTargetEnabled = false;
            return true;
        }
        public static void Execute(double lat, double lng)
        {
            SetMoveToTargetLat = lat;
            SetMoveToTargetLng = lng;
            SetMoveToTargetEnabled = true;
        }
    }
}