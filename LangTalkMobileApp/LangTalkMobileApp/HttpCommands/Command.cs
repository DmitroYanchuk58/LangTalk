using LangTalkMobileApp.DTOs;
using System.Text;
using System.Text.Json;

namespace LangTalkMobileApp.HttpCommands
{
    public static class Command
    {
        public static async Task<User> GetUser(Guid idUser)
        {
            var response = await HttpCommand.SendGetHttpRequest($"/User/GetUser?id={idUser}", null);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var user = JsonSerializer.Deserialize<User>(json);
                return user;
            }
            else
            {
                throw new Exception("Get user failed: " + response.ReasonPhrase);
            }
        }

        public static async Task<string> LoginGetToken(string email, string password)
        {
            var response = await HttpCommand.SendGetHttpRequest($"/Auth/Login?email={email}&password={password}", null);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var token = System.Text.Json.JsonDocument.Parse(json).RootElement.GetProperty("token").GetString();
                return token;
            }
            else
            {
                throw new Exception("Login failed: " + response.ReasonPhrase);
            }
        }

        public static async Task<string> RegistrationAndGetToken(string email, string password, string nickname)
        {
            var response = await HttpCommand.SendPostHttpRequest($"/Auth/Registration?email={email}&password={password}&nickname={nickname}", null);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var token = System.Text.Json.JsonDocument.Parse(json).RootElement.GetProperty("token").GetString();
                return token;
            }
            else
            {
                throw new Exception("Registration failed: " + response.ReasonPhrase);
            }
        }

        public static async Task<UserInfo> GetUserInfo(Guid idUserInfo)
        {
            var response = await HttpCommand.SendGetHttpRequest($"/UserInfo/GetUserInfo?id={idUserInfo}", null);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var userInfo = JsonSerializer.Deserialize<UserInfo>(json);
                return userInfo;
            }
            else
            {
                throw new Exception("Get user failed: " + response.ReasonPhrase);
            }
        }

        public static async Task<string> GetUserInfoIdByIdUser(Guid idUser)
        {
            var response = await HttpCommand.SendGetHttpRequest($"/UserInfo/GetUserInfoByIdUser?idUser={idUser}", null);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return json;
            }
            else
            {
                throw new Exception("Get user by email failed: " + response.ReasonPhrase);
            }
        }

        public static async Task<MbtiType> GetMbtiType(Guid idMbtiType)
        {
            var response = await HttpCommand.SendGetHttpRequest($"/MbtiType/GetMbtiType?id={idMbtiType}", null);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var mbtiType = JsonSerializer.Deserialize<MbtiType>(json);
                return mbtiType;
            }
            else
            {
                throw new Exception("Get chat failed: " + response.ReasonPhrase);
            }
        }

        public static async Task ChangeNickname(Guid idUser, string newNickname)
        {
            var response = await HttpCommand.SendPostHttpRequest($"/User/ChangeName?id={idUser}&newNickname={newNickname}", null);
            if (response.IsSuccessStatusCode)
            {
                return;
            }
            else
            {
                throw new Exception("Change name failed: " + response.ReasonPhrase);
            }
        }

        public static async Task ChangeUserInfo(Guid idUser, string newGender,
            string description, string firstName, string lastName, string country,
            DateTime dateOfBirth)
        {
            UserInfo userInfo = new UserInfo
            {
                IdUser = idUser,
                Description = description,
                FirstName = firstName,
                LastName = lastName,
                Country = country,
                DateOfBirth = dateOfBirth,
                Gender = newGender,
                TimeLastActivity = DateTime.Now,
                IdMbtiType = Guid.Empty
            };
            var json = JsonSerializer.Serialize(userInfo);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await HttpCommand.SendPostHttpRequest($"/UserInfo/UpdateUserInfo?idUser={idUser}&userInfo={userInfo}", content);
            if (response.IsSuccessStatusCode)
            {
                return;
            }
            else
            {
                throw new Exception("Change user info failed: " + response.ReasonPhrase);
            }
        }

        public static async Task ChangeMbtiType(Guid idUser, string mbtiType)
        {
            var response = await HttpCommand.SendPostHttpRequest($"/MbtiType/ChangeMbtiType?idUser={idUser}&mbtiType={mbtiType}", null);
            if (response.IsSuccessStatusCode)
            {
                return;
            }
            else
            {
                throw new Exception("Change mbti type failed: " + response.ReasonPhrase);
            }
        }

        public static async Task<List<MbtiType>> GetAllMbtiTypes()
        {
            var response = await HttpCommand.SendGetHttpRequest("/MbtiType/GetAllMbtiTypes", null).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<MbtiType>>(json);
            }
            else
            {
                throw new Exception("Get all MBTI types failed: " + response.ReasonPhrase);
            }
        }

        public static async Task<List<string>> GetAllCountries()
        {
            var response = await HttpCommand.SendGetHttpRequest("/User/GetAllCountries", null).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();
            }
            else
            {
                throw new Exception("Get all MBTI types failed: " + response.ReasonPhrase);
            }
        }

        public static async Task<List<string>> GetAllGenders()
        {
            var response = await HttpCommand.SendGetHttpRequest("/User/GetAllGenders", null).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();
            }
            else
            {
                throw new Exception("Get all MBTI types failed: " + response.ReasonPhrase);
            }
        }

        public static async Task<List<string>> GetAllLanguages()
        {
            var response = await HttpCommand.SendGetHttpRequest("/Language/GetLanguages", null).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();
            }
            else
            {
                throw new Exception("Get all languages failed: " + response.ReasonPhrase);
            }
        }

        public static async Task CreateUserLanguage(UserLanguage userLanguage)
        {
            var json = JsonSerializer.Serialize(userLanguage);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await HttpCommand.SendPostHttpRequest("/Language/SetLanguage", content).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return;
            }
            else
            {
                throw new Exception("Get all languages failed: " + response.ReasonPhrase);
            }
        }

        public static async Task<string> GetLanguageId(string languageName)
        {
            var response = await HttpCommand.SendGetHttpRequest($"/Language/GetLanguageIdByName?languageName={languageName}", null).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return json;
            }
            else
            {
                throw new Exception("Get user language failed: " + response.ReasonPhrase);
            }
        }

        public static async Task<List<string>> GetAllHobbies()
        {
            var response = await HttpCommand.SendGetHttpRequest("/Hobby/GetAllHobbies", null).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();
            }
            else
            {
                throw new Exception("Get all languages failed: " + response.ReasonPhrase);
            }
        }

        public static async Task<string> GetHobbyIdByName(string hobbyName)
        {
            var response = await HttpCommand.SendGetHttpRequest($"/Hobby/GetHobbyIdByName?hobbyName={hobbyName}", null).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<string>(json);
            }
            else
            {
                throw new Exception("Get all languages failed: " + response.ReasonPhrase);
            }
        }

        public static async Task CreateUserHobby(Guid idUser, Guid idHobby)
        {
            var response = await HttpCommand.SendPostHttpRequest($"/Hobby/CreateUserHobby?idUser={idUser}&idHobby={idHobby}").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return;
            }
            else
            {
                throw new Exception("Get all languages failed: " + response.ReasonPhrase);
            }
        }

        public static async Task<List<Language>> GetUserLanguages(Guid idUser)
        {
            var response = await HttpCommand.SendGetHttpRequest($"/Language/GetUserLanguages?idUser={idUser}", null).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStreamAsync();
                return JsonSerializer.Deserialize<List<Language>>(json);
            }
            else
            {
                throw new Exception("Get all languages failed: " + response.ReasonPhrase);
            }
        }

        public static async Task<List<string>> GetUserHobbies(Guid idUser)
        {
            var response = await HttpCommand.SendGetHttpRequest($"/Hobby/GetUserHobbies?idUser={idUser}", null).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStreamAsync();
                return JsonSerializer.Deserialize<List<string>>(json);
            }
            else
            {
                throw new Exception("Get user hobbies failed: " + response.ReasonPhrase);
            }
        }

        public static async Task<List<Guid>> GetUserIds(int startNumber, int endNumber)
        {
            var response = await HttpCommand.SendGetHttpRequest($"/User/GetIds?startNumber={startNumber}&endNumber={endNumber}", null).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStreamAsync();
                return JsonSerializer.Deserialize<List<Guid>>(json);
            }
            else
            {
                throw new Exception("Get user hobbies failed: " + response.ReasonPhrase);
            }
        }

        public static async Task<List<ConnectionInfo>> GetConnectionInfos(Guid idUser, int startNumber, int endNumber)
        {
            var response = await HttpCommand.SendGetHttpRequest($"/FindPeople/GetSortedConnectionInfos?idUser={idUser}&startNumber={startNumber}&endNumber={endNumber}", null).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStreamAsync();
                return JsonSerializer.Deserialize<List<ConnectionInfo>>(json);
            }
            else
            {
                throw new Exception("Get user hobbies failed: " + response.ReasonPhrase);
            }
        }

        public static async Task<Guid> CreateChat(Guid idFirstUser, Guid idSecondUser)
        {
            var response = await HttpCommand.SendPostHttpRequest($"/Chat/CreateChat?IdFirstUser={idFirstUser}&idSecondUser={idSecondUser}").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStreamAsync();
                return JsonSerializer.Deserialize<Guid>(json);
            }
            else
            {
                throw new Exception("Get user hobbies failed: " + response.ReasonPhrase);
            }
        }

        public static async Task<Chat> GetChat(Guid idChat)
        {
            var response = await HttpCommand.SendGetHttpRequest($"/Chat/GetChat?idChat={idChat}", null);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                var chat = System.Text.Json.JsonSerializer.Deserialize<Chat>(json, new System.Text.Json.JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return chat;
            }
            else
            {
                throw new Exception("Get chat failed: " + response.ReasonPhrase);
            }
        }

        public static async Task<List<SmallChat>> GetChats(Guid idUser)
        {
            var response = await HttpCommand.SendGetHttpRequest($"/Chat/GetChats?idUser={idUser}", null);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                var chats = JsonSerializer.Deserialize<List<SmallChat>>(json);

                return chats;
            }
            else
            {
                throw new Exception("Get chat failed: " + response.ReasonPhrase);
            }
        }

        public static async Task<List<ConnectionInfo>> GetConnectionInfosWithParametrs(Guid idUser, int startNumber, int endNumber, SortPeopleParameters parametrs)
        {
            var jsonParametrs = JsonSerializer.Serialize(parametrs);
            var content = new StringContent(jsonParametrs, Encoding.UTF8, "application/json");
            var response = await HttpCommand.SendPostHttpRequest($"/FindPeople/GetSortedConnectionInfosWithParametrs?idUser={idUser}&startNumber={startNumber}&endNumber={endNumber}", content).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStreamAsync();
                return JsonSerializer.Deserialize<List<ConnectionInfo>>(json);
            }
            else
            {
                throw new Exception("Get user hobbies failed: " + response.ReasonPhrase);
            }
        }

        public static async Task CreateUserImage(Guid idUser, byte[] image)
        {
            var json = JsonSerializer.Serialize(image);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await HttpCommand.SendPostHttpRequest($"/UserInfo/CreateUserImage?idUser={idUser}", content).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return;
            }
            else
            {
                throw new Exception("Get user hobbies failed: " + response.ReasonPhrase);
            }
        }

        public static async Task<byte[]> GetUserImage(Guid idUser)
        {
            var response = await HttpCommand.SendGetHttpRequest($"/UserInfo/GetUserImage?id={idUser}", null).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<byte[]>(json) ?? Array.Empty<byte>();
            }
            else
            {
                throw new Exception("Get user image failed: " + response.ReasonPhrase);
            }
        }
    }
}

