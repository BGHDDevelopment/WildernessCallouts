﻿using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using FivePD.API;

namespace WildernessCallouts
{
    
    [CalloutProperties("Dead Body Found", "BGHDDevelopment", "0.0.5")]
    public class PossibleDeadBody : Callout
    {
        Ped vic;
        
        public PossibleDeadBody()
        {
            Random random = new Random();
            int x = random.Next(1, 100 + 1);
            if(x <= 40)
            { 
                InitInfo(new Vector3(449.098f, 5513.02f, 755.491f));
            }
            else if(x > 40 && x <= 65)
            {
                InitInfo(new Vector3(1200.55f, 5782.78f, 519.177f));
            }
            else
            {
                InitInfo(new Vector3(2042.01f, 5378.45f, 172.677f));
            }
            ShortName = "Dead Body Found";
            CalloutDescription = "A dead body has been found on a trail.";
            ResponseCode = 2;
            StartDistance = 200f;
            UpdateData();
        }
        public async override Task OnAccept()
        {
            InitBlip();
            vic = await SpawnPed(GetRandomPed(), Location);
            vic.Kill();
            vic.AlwaysKeepTask = true;
            vic.BlockPermanentEvents = true;
        }
        public async override void OnStart(Ped player)
        {
            base.OnStart(player);
            vic.AttachBlip();
        }
        private void Notify(string message)
        {
            API.BeginTextCommandThefeedPost("STRING");
            API.AddTextComponentSubstringPlayerName(message);
            API.EndTextCommandThefeedPostTicker(false, true);
        }
        private void DrawSubtitle(string message, int duration)
        {
            API.BeginTextCommandPrint("STRING");
            API.AddTextComponentSubstringPlayerName(message);
            API.EndTextCommandPrint(duration, false);
        }
    }
}