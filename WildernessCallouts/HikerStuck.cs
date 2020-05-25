using System;
using System.Dynamic;
using System.Threading.Tasks;
using CalloutAPI;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace WildernessCallouts
{
    
    [CalloutProperties("Hiker Stuck", "BGHDDevelopment", "0.0.3", Probability.High)]
    public class HikerStuck : Callout
    {
        private Ped vic;
        
        public HikerStuck()
        {
            Random random = new Random();
            int x = random.Next(1, 100 + 1);
            if(x <= 40)
            { 
                InitBase(new Vector3(-765.125f, 4342.06f, 146.31f));
            }
            else if(x > 40 && x <= 65)
            {
                InitBase(new Vector3(-789.051f, 4546.31f, 114.618f));
            }
            else
            {
                InitBase(new Vector3(-765.125f, 4342.06f, 146.31f)); //default
            }
            ShortName = "Hiker Stuck";
            CalloutDescription = "A hiker is stuck and needs help.";
            ResponseCode = 2;
            StartDistance = 200f;
        }

        public async override void OnStart(Ped player)
        {
            base.OnStart(player);
            dynamic data1 = await GetPedData(vic.NetworkId);
            string firstname = data1.Firstname;
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

        public async override Task Init()
        {
            OnAccept();
            vic = await SpawnPed(GetRandomPed(), Location);
            dynamic data = new ExpandoObject();
            data.alcoholLevel = 0.07;
            SetPedData(vic.NetworkId,data);
            vic.AlwaysKeepTask = true;
            vic.BlockPermanentEvents = true;
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