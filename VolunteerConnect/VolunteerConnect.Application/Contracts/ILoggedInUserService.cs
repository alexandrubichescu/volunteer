﻿namespace VolunteerConnect.Application.Contracts
{
    public interface ILoggedInUserService
    {
        public string UserId { get; }
        public string UserName { get; }
    }
}
