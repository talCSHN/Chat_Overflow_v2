using Client.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly ClientSocket client = ClientSocket.Instance;

        [ObservableProperty]
        private CharacterManager manager;

        public MainViewModel()
        {
            CharacterManager.InitCharManager();
            Manager = CharacterManager.Instance;
            InitConnect();
            client.MessageReceived += OnMessageReceived;
        }

        private bool isAlreadyAdded = false;
        private void OnMessageReceived(string msg)
        {
            Console.WriteLine("MainViewModel MessageReceived Callback");
            string[] msgFromServer = msg.Split(' ');    // LOGINOK Username Seat
            if (!isAlreadyAdded && msgFromServer[0] == "LOGIN" && msgFromServer[1] == Name &&  msgFromServer[2] == Seat)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Character studentOrTutor = new Character(msgFromServer[1], Convert.ToInt32(msgFromServer[2]));
                    Manager.AddParticipant(studentOrTutor);
                    Console.WriteLine("채팅방 입장");
                });
            }
            else if (msgFromServer[0] == "LOGIN")
            {
                if (msgFromServer[1] == Name) return;
                if (CharacterManager.Instance.Participants.Any(c => c.UserName == msgFromServer[1])) return;
                var newParticipant = new Character(msgFromServer[1], Convert.ToInt32(msgFromServer[2]));
                Manager.AddParticipant(newParticipant);
            }
            else
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Console.WriteLine("연결 실패 " + msg);
                });
            }
        }

        // 생성자에서는 await를 쓸수없기때문에 Task타입으로 만드는 것 대신 void로 만들었음
        private async void InitConnect()
        {
            try
            {
                await ClientSocket.Instance.MakeConnection("127.0.0.1", 9000);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string seat;

        [RelayCommand]
        public async void Login()
        {
            string message = $"LOGIN {Name} {Seat}\n";
            Console.WriteLine($"로그인 요청한 유저 이름/좌석번호 : {message}");
            try
            {
                await client.SendToSever(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Login Error : " + ex.ToString());
            }
        }
    }
}
