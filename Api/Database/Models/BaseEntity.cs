using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Database.Models
{
    /// <summary>
    /// Defines some basic properties that every entity should have
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// The Id of the entity
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// When the item was created
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Who created the entity
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// When the item was modified
        /// </summary>
        public DateTime ModifiedAt { get; set; }

        /// <summary>
        /// Who modified the item
        /// </summary>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// Updated the properties about the msot recent modification
        /// </summary>
        public T SetCreated<T>(string nameOfCreatingUser)
            where T : BaseEntity
        {
            ModifiedAt = DateTime.Now;
            ModifiedBy = nameOfCreatingUser;

            return this as T;
        }

        /// <summary>
        /// Updated the properties about the msot recent modification
        /// </summary>
        public T UpdateModified<T>(string nameOfMdifyingUser)
            where T : BaseEntity
        {
            ModifiedAt = DateTime.Now;
            ModifiedBy = nameOfMdifyingUser;

            return this as T;
        }
    }
}
