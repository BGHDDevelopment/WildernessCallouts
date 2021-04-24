using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using FivePD.API;
using FivePD.API.Utils;

namespace WildernessCallouts
{
    
    [CalloutProperties("Possible Weed Farm", "BGHDDevelopment", "1.0.0")]
    public class WeedFarm : Callout
    {
        private Ped suspect1, suspect2, suspect3, suspect4;
        public WeedFarm()
        {
            InitInfo(new Vector3(2209.9f, 5613.07f, 53.8743f));
            ShortName = "Weed Farm Found";
            CalloutDescription = "A weed farm has been found.";
            ResponseCode = 2;
            StartDistance = 200f;
        }
        public async override void OnStart(Ped player)
        {
            base.OnStart(player);
            suspect1 = await SpawnPed(RandomUtils.GetRandomPed(), Location);
            suspect2 = await SpawnPed(RandomUtils.GetRandomPed(), Location);
            suspect3 = await SpawnPed(RandomUtils.GetRandomPed(), Location);
            suspect4 = await SpawnPed(RandomUtils.GetRandomPed(), Location);
            List<Item> items = new List<Item>();
            Item Weed = new Item {
                Name = "Bag of Weed",
                IsIllegal = true
            };
            items.Add(Weed);
            PedData data = new PedData();
            data.BloodAlcoholLevel = 0.10;
            data.Items = items;
            Utilities.SetPedData(suspect1.NetworkId,data);
            PedData data2 = new PedData();
            data2.BloodAlcoholLevel = 0.10;
            data2.Items = items;
            Utilities.SetPedData(suspect2.NetworkId,data2);
            PedData data3 = new PedData();
            data3.BloodAlcoholLevel = 0.10;
            data3.Items = items;
            Utilities.SetPedData(suspect3.NetworkId,data3);
            PedData data4 = new PedData();
            data4.BloodAlcoholLevel = 0.10;
            data4.Items = items;
            Utilities.SetPedData(suspect4.NetworkId,data4);
            suspect1.AlwaysKeepTask = true;
            suspect1.BlockPermanentEvents = true;
            suspect2.AlwaysKeepTask = true;
            suspect2.BlockPermanentEvents = true;
            suspect3.AlwaysKeepTask = true;
            suspect3.BlockPermanentEvents = true;
            suspect4.AlwaysKeepTask = true;
            suspect4.BlockPermanentEvents = true;
            suspect1.AttachBlip();
            suspect2.AttachBlip();
            suspect3.AttachBlip();
            suspect4.AttachBlip();
            suspect1.Weapons.Give(WeaponHash.SMG, 130, true, true);
            suspect2.Weapons.Give(WeaponHash.Revolver, 130, true, true);
            suspect3.Weapons.Give(WeaponHash.Firework, 130, true, true);
            suspect4.Weapons.Give(WeaponHash.Musket, 130, true, true);
            PedData data1 = await Utilities.GetPedData(suspect1.NetworkId);
            string firstname = data1.FirstName;
            PedData data5 = await Utilities.GetPedData(suspect2.NetworkId);
            string firstname2 = data5.FirstName;
            PedData data6 = await Utilities.GetPedData(suspect3.NetworkId);
            string firstname3 = data6.FirstName;
            PedData data7 = await Utilities.GetPedData(suspect4.NetworkId);
            string firstname4 = data7.FirstName;
            Random random = new Random();
            int x = random.Next(1, 100 + 1);
            if (x <= 40)
            {
                DrawSubtitle("~r~[" + firstname + "] ~s~ATTACK!", 5000);
                suspect1.Task.FightAgainst(player);
                suspect2.Task.FightAgainst(player);
                suspect3.Task.FightAgainst(player);
                suspect4.Task.FightAgainst(player);
                DrawSubtitle("~r~[" + firstname2 + "] ~s~OUR WEED!", 5000);
            }
            else if (x > 40 && x <= 65)
            {
                suspect1.Task.FleeFrom(player);
                suspect2.Task.FleeFrom(player);
                suspect3.Task.FleeFrom(player);
                suspect4.Task.FleeFrom(player);
                DrawSubtitle("~r~[" + firstname + "] ~s~Bail!", 5000);
                DrawSubtitle("~r~[" + firstname4 + "] ~s~Lets go!", 5000);
            }
            else
            {
                suspect3.Task.FleeFrom(player);
                suspect4.Task.FleeFrom(player); 
                suspect1.Task.FightAgainst(player);
                suspect2.Task.FightAgainst(player);
                DrawSubtitle("~r~[" + firstname + "] ~s~Kill them!", 5000);
                DrawSubtitle("~r~[" + firstname3 + "] ~s~No run!", 5000);
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