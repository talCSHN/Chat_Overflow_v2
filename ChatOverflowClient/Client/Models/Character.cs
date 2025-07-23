using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    public partial class Character : ObservableObject
    {
        [ObservableProperty]
        private string userName;
        
        [ObservableProperty]
        private int seatNo;

        [ObservableProperty]
        private bool isExist = false;

        public Character(string name, int num)
        {
            UserName = name;
            SeatNo = num;
        }
    }
}
