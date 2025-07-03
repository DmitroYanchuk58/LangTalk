using LangTalkMobileApp.MVVM.Model;
using LangTalkMobileApp.MVVM.View;
using LangTalkMobileApp.MVVM.ViewModel;
using LangTalkMobileApp.Token;
using Microsoft.Extensions.Logging;
#if ANDROID
using Microsoft.Maui.Handlers;
using Android.Graphics.Drawables;
#endif


namespace LangTalkMobileApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Karla-Bold.ttf", "KarlaBold");
                    fonts.AddFont("Karla-Italic-VariableFont_wght.fft", "KarlaItalic");
                    fonts.AddFont("Karla-VariableFont_wght.fft", "Karla");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddSingleton<LoginModel>();
            builder.Services.AddSingleton<LoginViewModel>();

            builder.Services.AddTransient<RegistrationPage>();
            builder.Services.AddSingleton<RegistrationModel>();
            builder.Services.AddSingleton<RegistrationViewModel>();

            builder.Services.AddTransient<ProfilePage>();
            builder.Services.AddSingleton<ProfileModel>();
            builder.Services.AddTransient<ProfileViewModel>();

            builder.Services.AddTransient<ChangeProfilePage>();
            builder.Services.AddTransient<ChangeProfileViewModel>();

            builder.Services.AddTransient<ChangeMbtiTypePage>();
            builder.Services.AddTransient<ChangeMbtiTypeViewModel>();

            builder.Services.AddTransient<ChangeHobbiesPage>();
            builder.Services.AddTransient<ChangeHobbiesViewModel>();

            builder.Services.AddTransient<SortPeopleParametrsPage>();
            builder.Services.AddTransient<SortPeopleParametrsViewModel>();

            builder.Services.AddTransient<ChangeLanguagesPage>();
            builder.Services.AddTransient<ChangeLanguagesViewModel>();

            builder.Services.AddTransient<ChatsPage>();
            builder.Services.AddTransient<ChatsViewModel>();

            builder.Services.AddTransient<ChatPage>();
            builder.Services.AddTransient<ChatViewModel>();

            builder.Services.AddTransient<SelectPeoplePage>();
            builder.Services.AddSingleton<SelectPeopleModel>();
            builder.Services.AddSingleton<SelectPeopleViewModel>();

            builder.Services.AddSingleton<TokenService>();


            #region Shit what I don't like but must deal with
#if ANDROID
            EntryHandler.Mapper.AppendToMapping("NoUnderline", (handler, view) =>
            {
                handler.PlatformView.Background = new ColorDrawable(Android.Graphics.Color.Transparent);
            });
#endif
            #endregion

            return builder.Build();

        }
    }
}
