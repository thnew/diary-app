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
        public string CeratedBy { get; set; }

        /// <summary>
        /// When the item was modified
        /// </summary>
        public DateTime ModifiedAt { get; set; }

        /// <summary>
        /// Who modified the item
        /// </summary>
        public string ModifiedBy { get; set; }
    }
}
