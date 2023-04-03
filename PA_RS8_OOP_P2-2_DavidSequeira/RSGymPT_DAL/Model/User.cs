﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSGymPT_DAL.Model
{
    public class User
    {
        #region Enum
        public enum EnumProfile
        {
            Collaborator,
            Administrator
        }
        #endregion

        #region Scalar Properties

        public int UserID { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "100 character limit.")]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength: 6, MinimumLength = 4, ErrorMessage = "The code must have between 4 and 6 characters.")]
        public string Code { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(maximumLength: 12, MinimumLength = 8, ErrorMessage = "The password must have between 8 and 12 characters.")]
        public string Password { get; set; }

        public EnumProfile Profile { get; set; }


        #endregion



    }
}
