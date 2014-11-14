using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VoiceWall.Data.Models;
using VoiceWall.Web.Infrastructure.Mapping;

namespace VoiceWall.Web.ViewModels.Account
{
    public class SingleProfileViewModel : IMapFrom<User>
    {
        public string Email { get; set; }

        public DateTime CreatedOn { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserImage { get; set; }
    }
}