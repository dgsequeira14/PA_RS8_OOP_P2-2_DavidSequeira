using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSGymPT_DAL.Model
{
    public class Request
    {
        #region Enum
        public enum EnumStatusRequest
        {
            Booked,
            Finished,
            Canceled
        }
        #endregion

        #region Scalar Properties

        public int RequestID { get; set; }
        public int ClientID { get; set; }
        public int PersonalTrainerID { get; set; }

        [Required]
        [DataType(DataType.Date, ErrorMessage = "The date must be in the format dd/mm/yyyy")]
        public DateTime Date { get; set; }

        [Required]
        [DataType(DataType.Time, ErrorMessage = "The time must be in the format hh:mm")]
        public DateTime Hour { get; set; }

        public EnumStatusRequest StatusRequest { get; set; }

        [MaxLength(255, ErrorMessage = "255 character limit.")]
        public string Observation { get; set; }

        #endregion

        #region Navigation Properties

        public virtual Client Client { get; set; }
        public virtual PersonalTrainer PersonalTrainer { get; set; }

        #endregion
    }
}
