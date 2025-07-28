using Client.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.ViewModels
{
    public partial class ChatViewModel : ObservableObject
    {
        private readonly ClientSocket client = ClientSocket.Instance;
        [ObservableProperty]
        private ObservableCollection<Character> currentParticipants;
        [ObservableProperty]
        private string inputText;
        [ObservableProperty]
        private string name;
        [ObservableProperty]
        private int seat;

        private ObservableCollection<string> chatMessages = new ObservableCollection<string>();
        public ObservableCollection<string> ChatMessages 
        { 
            get => chatMessages; 
            set => chatMessages = value; 
        }


        public ChatViewModel(string name, int seat)
        {
            Name = name;
            Seat = seat;

            client.MessageReceived += OnChatReceived;

            CurrentParticipants = CharacterManager.Instance.Participants;
        }

        private void OnChatReceived(string msg)
        {
            Console.WriteLine("ChatViewModel MessageReceived Callback");
            string[] msgFromServer = msg.Split(' ');
            string userName = msgFromServer[1];
            int seatNo = Convert.ToInt32(msgFromServer[2]);
            if (msgFromServer[0] == "CHAT")
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    string chatMessage = msg.Substring(5);
                    ChatMessages.Add(chatMessage);
                });
            }
            else if (msgFromServer[0] == "LOGIN")
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    var alreadyExists = CharacterManager.Instance.Participants.Any(c => c.UserName == userName);
                    if (!alreadyExists)
                    {
                        CharacterManager.Instance.AddParticipant(new Character(userName, seatNo));
                        CurrentParticipants = CharacterManager.Instance.Participants;
                    }
                    foreach (var participant in CurrentParticipants)
                    {
                        Console.WriteLine($"참가자 업데이트 {participant.UserName} {participant.SeatNo}");
                    }
                });
            }
        }

        [RelayCommand]
        public async Task SendMessage()
        {
            string MyMessage = $"CHAT {Name} {Seat} {InputText}";
            try
            {
                await ClientSocket.Instance.SendToServer(MyMessage);
                InputText = string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Send Message Error : " + ex);
            }
        }
    }
}
