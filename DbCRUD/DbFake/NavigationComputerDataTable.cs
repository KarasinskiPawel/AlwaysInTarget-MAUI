using AlwaysInTarget.DbCRUD.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlwaysInTarget.DbCRUD.DbFake
{
    public class NavigationComputerDataTable
    {
        List<NavigationComputerDataM> output = new List<NavigationComputerDataM>();

        public NavigationComputerDataTable()
        {
            output.Add(new NavigationComputerDataM() { KeyId = 1, MapHeading = 0, MapDistance = 0, IsEnabled = false });
            output.Add(new NavigationComputerDataM() { KeyId = 2, MapHeading = 0, MapDistance = 0, IsEnabled = false });
            output.Add(new NavigationComputerDataM() { KeyId = 3, MapHeading = 0, MapDistance = 0, IsEnabled = false });
            output.Add(new NavigationComputerDataM() { KeyId = 4, MapHeading = 0, MapDistance = 0, IsEnabled = false });
        }

        public List<NavigationComputerDataM> Output()
        {
            return output;
        }
    }
}
