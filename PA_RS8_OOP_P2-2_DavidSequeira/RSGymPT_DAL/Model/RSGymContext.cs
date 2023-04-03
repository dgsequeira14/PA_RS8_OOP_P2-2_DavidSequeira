using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSGymPT_DAL.Model
{
    public class RSGymContext : DbContext
    {
        #region Constructor

        public RSGymContext() : base("name=GymEntities") 
        { 
        
        }

        #endregion

        #region DB Creation

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        #endregion

        #region dbSets

        public DbSet<User> User { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<PersonalTrainer> PersonalTrainer { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Request> Request { get; set; }

        #endregion
    }
}
