using LangTalkMobileApp.DTOs;
using LangTalkMobileApp.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LangTalkMobileApp.ChangeViewCommand
{
    public class SelectPeopleChangingSystem
    {
        private int startNumber = 0;
        private int endNumber = 10;
        private int currentNumber = 0;
        private Guid idCurrentUser;
        private Dictionary<int, Guid> userIds = new Dictionary<int, Guid>();
        private List<ConnectionInfo> connectionInfos;
        private SortPeopleParameters parametrs = null;
        private SelectPeopleModel model;

        public SelectPeopleChangingSystem(Guid idUser)
        {
            idCurrentUser = idUser;
        }

        private async Task UpdateStartEndNumber()
        {
            startNumber = endNumber;
            endNumber += 10;

            await SetIds();
        }

        public async Task SetParametrs(SortPeopleParameters sortParameters)
        {
            parametrs = sortParameters;
            await SetIds();
        }
        public async Task<SelectPeopleModel> SetIds()
        {
            List<Guid> ids = new List<Guid>();
            if (parametrs == null)
            {
                connectionInfos = await HttpCommands.Command.GetConnectionInfos(idCurrentUser, startNumber, endNumber);
            }
            else
            {
                connectionInfos = await HttpCommands.Command.GetConnectionInfosWithParametrs(idCurrentUser, startNumber, endNumber, parametrs);
            }

            if (connectionInfos.Count == 0)
            {
                ids = await HttpCommands.Command.GetUserIds(startNumber, endNumber);
                if (ids.Count == 0)
                {
                    return null;
                }
            }
            else
            {
                ids = connectionInfos.Select(c => c.IdSecondUser).ToList();
            }

            int numberOfId = currentNumber;
            foreach (var id in ids)
            {
                userIds[numberOfId] = id;
                numberOfId++;
            }

            if (ids.Count < 10)
            {
                endNumber = startNumber + ids.Count;
            }

            return await GetUser();
        }

        public async Task<SelectPeopleModel> GoNextUser()
        {
            if (userIds.Count == 0) return null;

            currentNumber++;

            if (currentNumber >= userIds.Count)
            {
                return null;
            }

            if (currentNumber == endNumber-1)
            {
                await UpdateStartEndNumber();

                return await GetUser();
            }

            return await GetUser();
        }

        public async Task<SelectPeopleModel> GetUser()
        {
            model = new SelectPeopleModel();
            var idUser = userIds[currentNumber];
            model.IdUser = idUser;
            await LoadUser(idUser);
            return model;
        }
        private async Task LoadUser(Guid idUser)
        {
            var user = await HttpCommands.Command.GetUser(idUser);
            var connectionInfo = connectionInfos.First(c => c.IdSecondUser == user.Id);
            model.IsCompatibleType = connectionInfo.IsAppropriateType;
            model.Nickname = user.Nickname;
            if (connectionInfo.CountSameHobbies > 0) 
            {
                model.DoesHaveSomeHobbies = true;
                model.CountSameHobbies = connectionInfos.First(c => c.IdSecondUser == user.Id).CountSameHobbies;
            }
            else
            {
                model.DoesHaveSomeHobbies = false;
            }

            if (user.IdInfo != null)
            {
                Guid idUserInfo = Guid.Parse(user.IdInfo);
                var userInfo = await HttpCommands.Command.GetUserInfo(idUserInfo);
                await SetUserInfo(userInfo);
            }
            else
            {
                SetDefaultUserInfo();
                SetDefaultMbtiType();
            }
            var hobbies = await HttpCommands.Command.GetUserHobbies(idUser);
            if (hobbies.Count > 0)
            {
                SetHobbies(hobbies);
            }
            else
            {
                SetDefaultHobbies();
            }
            var languages = await HttpCommands.Command.GetUserLanguages(idUser);
            if (languages.Count > 0)
            {
                SetLanguages(languages);
            }
            else
            {
                SetDefaultLanguages();
            }
            var imageBytes = await HttpCommands.Command.GetUserImage(idUser);
            var image = LoadImageFromBytes(imageBytes);
            if (image != null)
            {
                model.DisplayedImage = image;
            }
            else
            {
                model.DisplayedImage = ImageSource.FromFile("default_user_image.png");
            }
        }

        public ImageSource LoadImageFromBytes(byte[] bytes)
        {
            if (bytes != null && bytes.Length > 0)
            {
                ImageSource image = ImageSource.FromStream(() => new MemoryStream(bytes));
                return image;
            }
            return null;
        }
        

        private void SetHobbies(List<string> hobbies)
        {
            model.Hobbies = hobbies;
        }
        private async Task SetUserInfo(UserInfo userInfo)
        {
            if (userInfo.DateOfBirth != null)
            {
                var age = DateTime.Today.Year - userInfo.DateOfBirth.Year;
                model.Age = age;
            }
            else
            {
                model.Age = 20;
            }

            if (!string.IsNullOrWhiteSpace(userInfo.FirstName) && !string.IsNullOrWhiteSpace(userInfo.LastName))
            {
                model.FirstName = userInfo.FirstName;
                model.LastName = userInfo.LastName;
            }
            else
            {
                model.FirstName = "John";
                model.LastName = "Doe";
            }

            if (!string.IsNullOrWhiteSpace(userInfo.Country))
            {
                model.Country = userInfo.Country;
            }
            else
            {
                model.Country = "Ukraine";
            }

            if (userInfo.IdMbtiType != null)
            {
                var mbtiType = await HttpCommands.Command.GetMbtiType(userInfo.IdMbtiType.Value);
                SetMbtiType(mbtiType);
            }
            else
            {
                SetDefaultMbtiType();
            }
        }

        private void SetLanguages(List<Language> languages)
        {
            model.LearningLanguages = new List<string>();
            model.SpokenLanguages = new List<string>();
            foreach (var language in languages)
            {
                if (language.IsSpoken)
                {
                    var newSpokenLanguage = language.LanguageName + " " + language.Level;
                    model.SpokenLanguages.Add(newSpokenLanguage);
                }
                else
                {
                    var newLearningLanguage = language.LanguageName + " " + language.Level;
                    model.LearningLanguages.Add(newLearningLanguage);
                }
            }
        }

        private void SetMbtiType(MbtiType mbtiType)
        {
            if (!string.IsNullOrWhiteSpace(mbtiType.Type))
            {
                model.MbtiType = mbtiType.Type;
            }
            else
            {
                model.MbtiType = "ISFP";
            }

            if (!string.IsNullOrWhiteSpace(mbtiType.Description))
            {
                model.MbtiDescription = mbtiType.Description;
            }
            else
            {
                model.MbtiDescription = "ISFP (Adventurer) is a personality type with the Introverted, Observant, Feeling, and Prospecting traits. They tend to have open minds, approaching life, new experiences, and people with grounded warmth. Their ability to stay in the moment helps them uncover exciting potentials.";
            }
        }

        private void SetDefaultUserInfo()
        {
            model.Age = 20;
            model.FirstName = "John";
            model.LastName = "Doe";
            model.Country = "Ukraine";
        }

        private void SetDefaultHobbies()
        {
            model.Hobbies = new List<string>();
        }

        private void SetDefaultLanguages()
        {
            model.SpokenLanguages = new List<string>();
            model.LearningLanguages = new List<string>();
        }

        private void SetDefaultMbtiType()
        {
            model.MbtiType = "ISFP";
            model.MbtiDescription = "ISFP (Adventurer) is a personality type with the Introverted, Observant, Feeling, and Prospecting traits. They tend to have open minds, approaching life, new experiences, and people with grounded warmth. Their ability to stay in the moment helps them uncover exciting potentials.";
        }
    }
}
