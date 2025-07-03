using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAppDiplomaDemo.MVVM.ViewModel;

namespace WpfAppDiplomaDemo.MVVM.Model
{
    public class ChatsModel
    {
        public List<SmallChatModel> SmallChats { get; set; } = new List<SmallChatModel>();

        public ChatsModel(List<SmallChatModel> smallChats, MainViewModel model)
        {
            SmallChats = smallChats;
        }
    }
}
