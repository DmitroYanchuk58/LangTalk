using CommunityToolkit.Mvvm.Input;
using LangTalkMobileApp.DTOs;
using LangTalkMobileApp.MVVM.View;

namespace LangTalkMobileApp.ChangeViewCommand
{
    public static partial class ChangeViewCommand
    {
        public static async Task ChangeOnProfileView(Guid idUser)
        {
            var navigationParameters = new Dictionary<string, object>
            {
                { "IdUser", idUser }
            };

            await Shell.Current.GoToAsync(nameof(ProfilePage), false, navigationParameters);
        }

        public static async Task ChangeOnChatsView(Guid idUser)
        {
            var navigationParameters = new Dictionary<string, object>
            {
                { "IdUser", idUser },
            };

            await Shell.Current.GoToAsync(nameof(ChatsPage), false, navigationParameters);
        }

        public static async Task ChangeOnChatView(Guid idChat)
        {
            var navigationParameters = new Dictionary<string, object>
            {
                { "IdChat", idChat },
            };

            await Shell.Current.GoToAsync(nameof(ChatPage), false, navigationParameters);
        }

        public static async Task ChangeOnSelectPeopleView(SortPeopleParameters parametrs)
        {
            var navigationParameters = new Dictionary<string, object>
            {
                { "Parametrs", parametrs },
            };

            await Shell.Current.GoToAsync(nameof(SelectPeoplePage), false, navigationParameters);
        }
    }
}
