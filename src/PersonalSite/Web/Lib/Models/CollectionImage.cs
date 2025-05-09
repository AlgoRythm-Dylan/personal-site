﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Lib.Models
{
    /// <summary>
    /// Linking table which "adds" a photo to a collection
    /// </summary>
    [Table("CollectionImages")]
    [PrimaryKey(nameof(ImageID), nameof(CollectionID))]
    public class CollectionImage
    {
        public string ImageID { get; set; }
        public string CollectionID { get; set; }

        [ForeignKey(nameof(ImageID))]
        public virtual Image Image { get; set; }
        [ForeignKey(nameof(CollectionID))]
        public virtual Collection Collection { get; set; }
    }
}
