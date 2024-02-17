using System;
using System.Collections.Generic;
using System.Text;

namespace DoAnTotNghiep.Config
{
    public interface ISelectItem
    {
        string GetKey();
        string GetDisplay();
        string GetCustomDisplay();
        void SetKey(string key);
    }
}
