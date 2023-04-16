﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RSGymPT_DAL.Model
{
    public class Client
    {
        #region Enum
        public enum EnumStatusClient
        { 
            Active,
            Inactive
        }
        #endregion

        #region Scalar Properties

        public int ClientID { get; set; }
        public int PersonalTrainerID { get; set; }
        public int LocationID { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "100 character limit.")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "100 character limit.")]
        public string LastName { get; set; }

        public string Name => $"{FirstName} {LastName}";

        [Required]
        [DataType(DataType.Date, ErrorMessage = "The date must be in the format dd/mm/yyyy")]
        public DateTime BirthDate { get; set; }

        [Required]
        [StringLength(maximumLength: 9, MinimumLength = 9, ErrorMessage = "The NIF must have 9 numbers.")]
        public string NIF { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [StringLength(maximumLength: 9, MinimumLength = 9, ErrorMessage = "The Phone Number must have 9 digits.")]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "100 character limit.")]
        public string Address { get; set; }

        public string Status { get; set; }

        [MaxLength(255, ErrorMessage = "255 character limit.")]
        public string Observation { get; set; }
        #endregion

        #region Navigation Properties

        public virtual PersonalTrainer PersonalTrainer { get; set; }
        public virtual Location Location { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
        #endregion
    }
}
