﻿using System;
using System.Dynamic;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using FivePD.API;
using FivePD.API.Utils;

namespace WildernessCallouts
{
    
    [CalloutProperties("Hiker Stuck", "BGHDDevelopment", "1.0.0")]
    public class HikerStuck : Callout
    {
        private Ped vic;
        
        public HikerStuck()
        {
            Random random = new Random();
            int x = random.Next(1, 100 + 1);
            if(x <= 40)
            { 
                InitInfo(new Vector3(-765.125f, 4342.06f, 146.31f));
            }
            else if(x > 40 && x <= 65)
            {
                InitInfo(new Vector3(-789.051f, 4546.31f, 114.618f));
            }
            else
            {
                InitInfo(new Vector3(-765.125f, 4342.06f, 146.31f)); //default
            }
            ShortName = "Hiker Stuck";
            CalloutDescription = "A hiker is stuck and needs help.";
            ResponseCode = 2;
            StartDistance = 200f;
        }

        public async override void OnStart(Ped player)
        {
            base.OnStart(player);
            vic = await SpawnPed(RandomUtils.GetRandomPed(), Location);
            PedData data = new PedData();
            data.BloodAlcoholLevel = 0.07;
            Utilities.SetPedData(vic.NetworkId, data);
            vic.AlwaysKeepTask = true;
            vic.BlockPermanentEvents = true;
            PedData data1 = await Utilities.GetPedData(vic.NetworkId);
            string firstname = data1.FirstName;
            vic.AttachBlip();
            Random random = new Random();
            int x = random.Next(1, 100 + 1);
            if(x <= 40)
            { 
                vic.Task.Wait(1000); //wait
                DrawSubtitle("~r~[" + firstname + "] ~s~Please grab me and get me out of here!", 5000);

            }
            else if(x > 40 && x <= 65)
            {
                vic.Task.ReactAndFlee(player); //run
                DrawSubtitle("~r~[" + firstname + "] ~s~Leave me alone!", 5000);
            }
            else
            {
                vic.Weapons.Give(WeaponHash.Pistol, 100, true, true);
                DrawSubtitle("~r~[" + firstname + "] ~s~DIE!", 5000);
            }

        }

        public async override Task OnAccept()
        {
            InitBlip();
            UpdateData();
        }
        
        private void DrawSubtitle(string message, int duration)
        {
            API.BeginTextCommandPrint("STRING");
            API.AddTextComponentSubstringPlayerName(message);
            API.EndTextCommandPrint(duration, false);
        }

    }
}