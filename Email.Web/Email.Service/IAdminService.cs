using Emaill.Model;
using Emaill.Model.Models;
using Emaill.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Email.Service
{
    public interface IAdminService: IDependency
    {
        AccountUser CheckUser(string account, string password);
        void UpdateLoginTime(AccountUser user);

        AccountUser GetUserByID(string userid);

        List<AccountUser> GetUserList();

        void AddUser(AccountUser user);
        bool UpdateUser(AccountUser user);
        bool DeleteUser(string id);

        void AddImg(string name, string userId, string[] urls);

        List<AlbumView> GetAlbumList(string userid);

        List<Album> GetAlbums(string userid);

        void AddSchedule(Schedule schedule);
        bool EditSchedule(Schedule schedule);

        List<Schedule> GetSchedules();


    }
}
