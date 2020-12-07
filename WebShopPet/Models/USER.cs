namespace WebShopPet.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("USERS")]
    public partial class USER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public USER()
        {
            ORDERS = new HashSet<ORDER>();
            PRODUCTS = new HashSet<PRODUCT>();
        }

        public int ID { get; set; }

        [StringLength(50)]
        [DisplayName("Tên người dùng")]
        [Required(ErrorMessage ="Tên không được bỏ trống")]
        public string NAME { get; set; }

        [DisplayName("Giới tính")]
        [UIHint("Boolean")]
        public bool? SEX { get; set; }

        [StringLength(50)]
        [DisplayName("Email")]
        [Required(ErrorMessage = "Email không được bỏ trống")]
        public string EMAIL { get; set; }

        [StringLength(50)]
        [DisplayName("Mật khẩu")]
        [Required(ErrorMessage = "Mật khẩu không được bỏ trống")]
        public string PASSWORD { get; set; }

        public int? ROLE { get; set; }

        public int? STATUS { get; set; }

        public string IMAGE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORDER> ORDERS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRODUCT> PRODUCTS { get; set; }
    }
}
