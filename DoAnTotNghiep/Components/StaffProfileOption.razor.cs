using DoAnTotNghiep.Config;
using DoAnTotNghiep.Domain;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Components
{
    partial class StaffProfileOption
    {
        [Parameter] public Users Model { get; set; }

        string avatarPath;
        string color;
        string shortName;

        protected override void OnInitialized()
        {
            try
            {
                var random = new Random();
                if (Model.Avatar.IsNotNullOrEmpty())
                {
                    avatarPath = Model.Avatar.GetPathUrl();
                }
                List<string> names = Model.Name.Split(' ').ToList();

                color = string.Format("#{0:X6}", random.Next(0x1000000));
                shortName = new string(names.Where(c => c.Length > 0).Select(s => s[0]).ToArray());
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
