using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RSGymPT_DAL.Model
{
    public class Location     // ToDo: Tive que alterar o nome desta Class (inicialmente tinha criado como PostCode), para não haver conflito depois com a property PostCode 
    {
        #region Scalar Properties

        public int LocationID { get; set; }

        [Required]
        [StringLength(maximumLength: 8, MinimumLength = 8, ErrorMessage = "The Post Code must have the format 0000-000.")]
        public string PostCode { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "100 character limit.")]
        public string City { get; set; }

        #endregion

        #region Navigation Properties

        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<PersonalTrainer> PersonalTrainers { get; set; }

        #endregion

    }
}
