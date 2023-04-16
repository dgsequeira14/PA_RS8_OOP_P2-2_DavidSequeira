using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RSGymPT_DAL.Model
{
    public class PersonalTrainer
    {
        #region Scalar Properties

        public int PersonalTrainerID { get; set; }
        public int LocationID { get; set; }

        public string Code => $"PT0{PersonalTrainerID}";

        [Required]
        [MaxLength(100, ErrorMessage = "100 character limit.")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "100 character limit.")]
        public string LastName { get; set; }

        public string Name => $"{FirstName} {LastName}";

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

        #endregion

        #region Navigation Porperties

        public virtual ICollection<Client> Clients { get; set; }
        public virtual Location Location { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
        #endregion

    }
}
