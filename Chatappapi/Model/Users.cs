﻿using System.ComponentModel.DataAnnotations;

namespace Chatappapi.Model
{
    public class Users
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string activation { get; set; }
        public string otp { get; set; }
        public DateOnly Date { get; set; }

    }
    public class RegisterDto
    {
        public String Name { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public String Phone { get; set; }
    };
    public class LoginDTo
    {
       public Guid UserId { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class ForgotDTo
    {

        [Required]

        public string Email { get; set; }
        [Required]

        public string NewPassword { get; set; }

        [Required]

        public string ConfirmPassword { get; set; }

        public string ResetToken { get; set; }
    }
    public class updateProfile
    {
        public Guid Id { get; set; }

        public string Name {  get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        
    }
    public class Getotp
    {
        public String Email { get; set; }
        public String otp { get; set; }
    }
}



