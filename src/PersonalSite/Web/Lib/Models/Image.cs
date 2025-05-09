﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Lib.Models
{
    [Table("Images")]
    public class Image
    {
        [Key]
        public string ID { get; set; } = Crypto.RandomToken();
        /// <summary>
        /// The publicly visible work title.
        /// </summary>
        [MaxLength(256)]
        public string? Title { get; set; }
        /// <summary>
        /// Description of the work.
        /// </summary>
        [MaxLength(4096)]
        public string? Description { get; set; }
        /// <summary>
        /// If any alterations were made, the uploader can describe them here.
        /// </summary>
        [MaxLength(1024)]
        public string? AlterationsDescription { get; set; }
        /// <summary>
        /// If this value is 1, there were no alterations made to the image and
        /// the "AltterationsDescription" column can be hidden.
        /// </summary>
        public int? NoAlterations { get; set; }
        /// <summary>
        /// Original image source width.
        /// </summary>
        public int SourceWidth { get; set; }
        /// <summary>
        /// Original image source height.
        /// </summary>
        public int SourceHeight { get; set; }
        /// <summary>
        /// Value of average brightness from 0 (black) to 100 (white).
        /// </summary>
        public int Brightness { get; set; }
        /// <summary>
        /// The date the image was captured.
        /// </summary>
        public DateTime? CaptureDate { get; set; } = null;
        /// <summary>
        /// Unlisted images only show up for those with the link
        /// or in collections which are ALSO unlisted
        /// </summary>
        public int IsUnlisted { get; set; } = 0;
        /// <summary>
        /// Auto-generated file name (without extension). As different sizes
        /// are generated, they are added to the file name. Original: abc123.jpg,
        /// thumbnail: abc123.thumb.jpg, medium: ac123.med.jpg
        /// </summary>
        public string SystemFileName { get; set; } = Guid.NewGuid().ToString();
        /// <summary>
        /// The original name of the uploaded file, if applicable
        /// </summary>
        public string? OriginalFileName { get; set; }
        /// <summary>
        /// Optional ISO level
        /// </summary>
        public int? ISO { get; set; } = null;
        /// <summary>
        /// If the exposure time is less than a second, this will
        /// always be 1 (think: 1/500). If it's more than a second,
        /// this will be that time (1.5s, 30s, etc).
        /// </summary>
        public float? ExposureTimeNumerator { get; set; } = null;
        /// <summary>
        /// If the exposure time is less than a second, this is the
        /// fraction of a second (example, 1/500 exposure time means
        /// this property will be 500). If the exposure time is more
        /// than a second, this will be null.
        /// </summary>
        public int? ExposureTimeDenominator { get; set; } = null;
        /// <summary>
        /// Optional aperature value such as 2.8
        /// </summary>
        public float? Aperature { get; set; } = null;
        /// <summary>
        /// Content tags can be set to reference the image on the site.
        /// For example, homepage_image can be used to reference any
        /// image tagged "homepage_image". This does include multiples,
        /// in which case the site should show all matching images in
        /// a slideshow.
        /// </summary>
        public string? ContentTag { get; set; } = null;

        public DateTime UploadDate { get; set; } = DateTime.Now;
        public DateTime? DeletedDate { get; set; } = null;
    }
}
