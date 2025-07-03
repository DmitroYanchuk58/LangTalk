using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAppDiplomaDemo.HttpCommands;
using WpfAppDiplomaDemo.JsonModels;
using WpfAppDiplomaDemo.MVVM.Model;
using WpfAppDiplomaDemo.MVVM.ViewModel;
using WpfAppDiplomaDemo.SignalR;

namespace WpfAppDiplomaDemo.Command
{
    public static class ChangeViewCommand
    {
        public static async Task ChangeViewToChats(MainViewModel mainViewModel)
        {
            var id = mainViewModel.GetUserIdFromToken(mainViewModel.Token);
            var response = await HttpCommand.SendGetHttpRequest($"/Chat/GetChats?idUser={id}", mainViewModel.Token);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var smallChats = System.Text.Json.JsonSerializer.Deserialize<List<SmallChatModel>>(json) ?? new List<SmallChatModel>();

                var model = new ChatsModel(smallChats, mainViewModel);
                mainViewModel.RunChatsViewCommand(model);
            }
        }

        public static async Task ChangeViewToProfile(MainViewModel mainViewModel)
        {
            Guid idUser = mainViewModel.GetUserIdFromToken(mainViewModel.Token) ?? Guid.Empty;
            try
            {
                ProfileModel profileModel = await GetProfileModel(idUser);
                mainViewModel.RunProfileViewCommand(profileModel);
            }
            catch
            {
                ProfileModel profileModel = new ProfileModel(null, null, null, null,null);
                mainViewModel.RunProfileViewCommand(profileModel);
            }
        }

        public static async Task ChangeViewToSelectPeople(MainViewModel mainViewModel, Guid idUser)
        {
            try
            {
                SelectPeopleModel selectPeopleModel = await GetSelectPeopleModel(idUser);
                mainViewModel.RunSelectPeopleViewCommand(selectPeopleModel);
            }
            catch
            {
                SelectPeopleModel selectPeopleModel = new SelectPeopleModel("not uknown","not","aaaa", "edfc",null, 3, false);
                mainViewModel.RunSelectPeopleViewCommand(selectPeopleModel);
            }
        }

        private async static Task<SelectPeopleModel> GetSelectPeopleModel(Guid idUser)
        {
            var user = await GetUser(idUser);
            if (string.IsNullOrEmpty(user.IdInfo))
            {
                return new SelectPeopleModel(user.Nickname, "Unknown","Unknown","Unknown",null, 3, false);
            }
            var userInfo = await GetUserInfo(Guid.Parse(user.IdInfo));
            if(string.IsNullOrEmpty(userInfo.IdMbtiType.ToString()) || userInfo.IdMbtiType == Guid.Empty || userInfo.IdMbtiType == null)
            {
                return new SelectPeopleModel(user.Nickname, userInfo.Country, "Unknown", userInfo.Description,null,4,false);
            }
            var mbtiType = await GetMbtiType(userInfo.IdMbtiType.Value);
            var hobbies = await GetHobbies(idUser);
            return new SelectPeopleModel(user.Nickname, userInfo.Country, mbtiType.Type, userInfo.Description, hobbies, 4, false);
        }

        private async static Task<User> GetUser(Guid idUser)
        {
            var userJson = await HttpCommands.Commands.GetUser(idUser);
            User user = System.Text.Json.JsonSerializer.Deserialize<User>(userJson);
            return user;
        }

        private async static Task<UserInfo> GetUserInfo(Guid idUserInfo)
        {
            var userInfoJson = await HttpCommands.Commands.GetUserInfo(idUserInfo);
            UserInfo userInfo = System.Text.Json.JsonSerializer.Deserialize<UserInfo>(userInfoJson);
            return userInfo;
        }

        private async static Task<MbtiType> GetMbtiType(Guid idMbtiType)
        {
            var mbtiTypeJson = await HttpCommands.Commands.GetMbtiType(idMbtiType);
            MbtiType mbtiType = System.Text.Json.JsonSerializer.Deserialize<MbtiType>(mbtiTypeJson);
            return mbtiType;
        }

        private async static Task<List<Language>> GetUserLanguages(Guid idUser)
        {
            var languages = await HttpCommands.Commands.GetUserLanguages(idUser);
            return languages;
        }

        private async static Task<List<string>> GetHobbies(Guid idUser)
        {
            var hobbies = await HttpCommands.Commands.GetUserHobbies(idUser);
            return hobbies;
        }

        private static async Task<ProfileModel> GetProfileModel(Guid idUser)
        {
            var user = await GetUser(idUser);
            if(string.IsNullOrEmpty(user.IdInfo))
            {
                return new ProfileModel(user, null, null, null,null);
            }
            var userInfo = await GetUserInfo(Guid.Parse(user.IdInfo));
            if (userInfo.IdMbtiType == Guid.Empty || userInfo.IdMbtiType == null)
            {
                return new ProfileModel(user, userInfo, null, null, null);
            }
            var mbtiType = await GetMbtiType(userInfo.IdMbtiType.Value);
            var languages = await GetUserLanguages(idUser);
            var hobbies = await GetHobbies(idUser);
            ProfileModel profileModel = new ProfileModel(user, userInfo, mbtiType,languages, hobbies);
            return profileModel;
        }

        public static void ChangeToChat(MainViewModel mainVm, ChatModel chat)
        {
            mainVm.CreateChatClient();
            var client = mainVm.GetClient();
            mainVm.RunChatViewCommand(chat,client);
        }
    }
}
