//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MorePractice
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;

    public partial class Doctor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Doctor()
        {
            this.Reception = new HashSet<Reception>();
            this.ServiceDoctor = new HashSet<ServiceDoctor>();
        }
    
        public System.Guid ID { get; set; }

        [Required(ErrorMessage = "Это поле обязательно для заполнения!")]
        [Display(Name = "Имя")]
        [MaxLength(20, ErrorMessage = "Слишком много символов.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Это поле обязательно для заполнения!")]
        [Display(Name = "Имя")]
        [MaxLength(20, ErrorMessage = "Слишком много символов.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Это поле обязательно для заполнения!")]
        [Display(Name = "Имя")]
        [MaxLength(20, ErrorMessage = "Слишком много символов.")]
        public string Fathername { get; set; }

        public System.Guid ID_Clinic { get; set; }
        public System.Guid ID_Special { get; set; }

        [ForeignKey("ID_Special")]
        public virtual Specializ Specializ { get; set; }
        [ForeignKey("ID_Clinic")]
        public virtual VetClinic VetClinic { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reception> Reception { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceDoctor> ServiceDoctor { get; set; }
    }
}