using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using CalloutAPI;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace WildernessCallouts
{
    
    [CalloutProperties("Cult Hostage Situation", "BGHDDevelopment", "0.0.1", Probability.Low)]
    public class CultHostage : Callout
    {
        Ped suspect1, suspect2, suspect3, suspect4, suspect5, suspect6, suspect7, suspect8, suspect9, suspect10;
        Ped hostage1, hostage2, hostage3, hostage4;
        private string[] badItemList = { "Beer Bottle", "Open Beer Can", "Wine Bottle", "Random Pills", "Needles", "SMG", "Sniper", "Rifle", "Pistol", "Knife", "Broken Bottle", "Musket"};
        List<object> items = new List<object>();
        public CultHostage()
        {
            InitBase(new Vector3(-1114.12f, 4923.71f, 217.968f));
            ShortName = "Cult Holding Hostages";
            CalloutDescription = "A cult is holding 4 people hostage.";
            ResponseCode = 3;
            StartDistance = 200f;
        }
        public async override void OnStart(Ped player)
        {
            suspect1.AttachBlip();
            suspect2.AttachBlip();
            suspect3.AttachBlip();
            suspect4.AttachBlip();
            suspect5.AttachBlip();
            suspect6.AttachBlip();
            suspect7.AttachBlip();
            suspect8.AttachBlip();
            suspect9.AttachBlip();
            suspect10.AttachBlip();
            hostage1.AttachBlip();
            hostage2.AttachBlip();
            hostage3.AttachBlip();
            hostage4.AttachBlip();
            suspect1.Weapons.Give(WeaponHash.SMG, 130, true, true);
            suspect2.Weapons.Give(WeaponHash.Revolver, 130, true, true);
            suspect3.Weapons.Give(WeaponHash.Firework, 130, true, true);
            suspect4.Weapons.Give(WeaponHash.Musket, 130, true, true);
            suspect5.Weapons.Give(WeaponHash.AssaultRifle, 130, true, true);
            suspect7.Weapons.Give(WeaponHash.CarbineRifle, 130, true, true);
            suspect7.Weapons.Give(WeaponHash.MarksmanRifle, 130, true, true);
            suspect8.Weapons.Give(WeaponHash.SniperRifle, 130, true, true);
            suspect9.Weapons.Give(WeaponHash.HeavySniper, 130, true, true);
            suspect10.Weapons.Give(WeaponHash.Knife, 130, true, true);
            suspect1.Task.FightAgainst(player);
            suspect2.Task.FightAgainst(player);
            suspect3.Task.FightAgainst(player);
            suspect4.Task.FightAgainst(player);
            suspect5.Task.FightAgainst(player);
            suspect6.Task.FightAgainst(player);
            suspect7.Task.FightAgainst(player);
            suspect8.Task.FightAgainst(player);
            suspect9.Task.FightAgainst(player);
            suspect10.Task.FightAgainst(player);
            hostage1.Task.HandsUp(1000000);
            hostage2.Task.HandsUp(1000000);
            hostage3.Task.HandsUp(1000000);
            hostage4.Task.HandsUp(1000000);
            hostage1.Task.ReactAndFlee(suspect1);
            hostage2.Task.ReactAndFlee(suspect5);
            hostage3.Task.ReactAndFlee(suspect4);
            hostage4.Task.ReactAndFlee(suspect3);
            dynamic data1 = await GetPedData(suspect1.NetworkId);
            string firstname = data1.Firstname;
            API.Wait(6000);
            DrawSubtitle("~r~[" + firstname + "] ~s~GET OUT OF HERE PIGS!", 5000);
            dynamic data2 = await GetPedData(suspect2.NetworkId);
            string firstname2 = data2.Firstname;
            API.Wait(6000);
            DrawSubtitle("~r~[" + firstname2 + "] ~s~ATTACK!", 5000);
            API.Wait(6000);
            DrawSubtitle("~r~[" + firstname + "] ~s~LEAVE US ALONE!", 5000);
        }
        public async override Task Init()
        {
            OnAccept();
            suspect1 = await SpawnPed(GetRandomPed(), Location + 2);
            suspect2 = await SpawnPed(GetRandomPed(), Location + 3);
            suspect3 = await SpawnPed(GetRandomPed(), Location + 5);
            suspect4 = await SpawnPed(GetRandomPed(), Location + 4);
            suspect5 = await SpawnPed(GetRandomPed(), Location + 1);
            suspect6 = await SpawnPed(GetRandomPed(), Location);
            suspect7 = await SpawnPed(GetRandomPed(), Location + 6);
            suspect8 = await SpawnPed(GetRandomPed(), Location + 7);
            suspect9 = await SpawnPed(GetRandomPed(), Location + 8);
            suspect10 = await SpawnPed(GetRandomPed(), Location + 5);
            hostage1 = await SpawnPed(GetRandomPed(), Location);
            hostage2 = await SpawnPed(GetRandomPed(), Location + 1);
            hostage3 = await SpawnPed(GetRandomPed(), Location + 2);
            hostage4 = await SpawnPed(GetRandomPed(), Location + 3);
            dynamic playerData = GetPlayerData();
            string displayName = playerData.DisplayName;
            Notify("~r~[WildernessCallouts] ~y~Officer ~b~" + displayName + ",~y~ the suspects are heavily armed and have 4 hostages!");
            suspect1.AlwaysKeepTask = true;
            suspect1.BlockPermanentEvents = true;
            suspect2.AlwaysKeepTask = true;
            suspect2.BlockPermanentEvents = true;
            suspect3.AlwaysKeepTask = true;
            suspect3.BlockPermanentEvents = true;
            suspect4.AlwaysKeepTask = true;
            suspect4.BlockPermanentEvents = true;
            suspect5.AlwaysKeepTask = true;
            suspect5.BlockPermanentEvents = true;
            suspect6.AlwaysKeepTask = true;
            suspect6.BlockPermanentEvents = true;
            suspect7.AlwaysKeepTask = true;
            suspect7.BlockPermanentEvents = true;
            suspect8.AlwaysKeepTask = true;
            suspect8.BlockPermanentEvents = true;
            suspect9.AlwaysKeepTask = true;
            suspect9.BlockPermanentEvents = true;
            suspect10.AlwaysKeepTask = true;
            suspect10.BlockPermanentEvents = true;
            hostage1.AlwaysKeepTask = true;
            hostage1.BlockPermanentEvents = true;
            hostage2.AlwaysKeepTask = true;
            hostage2.BlockPermanentEvents = true;
            hostage3.AlwaysKeepTask = true;
            hostage3.BlockPermanentEvents = true;
            hostage4.AlwaysKeepTask = true;
            hostage4.BlockPermanentEvents = true;
            dynamic data = new ExpandoObject();
            Random random2 = new Random();
            string name = badItemList[random2.Next(badItemList.Length)];
            object badItem = new {
                Name = name,
                IsIllegal = true
            };
            items.Add(badItem);
            data.items = items;
            SetPedData(suspect1.NetworkId,data);
            SetPedData(suspect2.NetworkId,data);
            SetPedData(suspect3.NetworkId,data);
            SetPedData(suspect4.NetworkId,data);
            SetPedData(suspect5.NetworkId,data);
            SetPedData(suspect6.NetworkId,data);
            SetPedData(suspect7.NetworkId,data);
            SetPedData(suspect8.NetworkId,data);
            SetPedData(suspect9.NetworkId,data);
            SetPedData(suspect10.NetworkId,data);
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