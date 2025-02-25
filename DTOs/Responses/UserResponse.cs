﻿namespace SecureApiWithJwt.DTOs.Responses
{
    public class UserResponse
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int RoleId { get; set; }
    }
}
