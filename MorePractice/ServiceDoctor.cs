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

    public partial class ServiceDoctor
    {
        public System.Guid ID { get; set; }
        public System.Guid ID_Service { get; set; }
        public System.Guid ID_Doctor { get; set; }

        [ForeignKey("ID_Doctor")]
        public virtual Doctor Doctor { get; set; }
        [ForeignKey("ID_Service")]
        public virtual Service Service { get; set; }
    }
}