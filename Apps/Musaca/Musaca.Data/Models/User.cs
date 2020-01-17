namespace Musaca.Data.Models
{
    using SIS.Mvc.Framework.Attributes.Validation;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        public User()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }// - a GUID String, Primary Key

        [RequiredSIS]
        [MaxLength(20)]
        public string Username { get; set; } //-  a string with min length 5 and max length 20 (required)

        [RequiredSIS]
        [MaxLength(20)]
        public string Email { get; set; } //- a string with min length 5 and max length 20 (required)

        [RequiredSIS]
        public string Password { get; set; } 
    }
}