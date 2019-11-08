﻿using CoreNotes.AutoFac.IRepository;
using CoreNotes.AutoFac.Model.Models;
using CoreNotes.AutoFac.Repository.Base;

namespace CoreNotes.AutoFac.Repository
{
    public class UserRoleRepository: BaseRepository<UserRole>, IUserRoleRepository
    {
        
    }
}