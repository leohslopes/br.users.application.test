﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace br.users.application.test.domain.Entities.UserCx
{
    public class Users
    {
        public int UserID { get; set; }

        public required string UserName { get; set; }

        public required string UserEmail { get; set; }

        public int UserAge { get; set; }

        public required string UserGender { get; set; }

        public required string UserPassword { get; set; }

        public byte[]? UserPicture { get; set; }

        public required string UserOfficialNumber { get; set; }

        public DateTime DateAlter {  get; set; }
    }
}
