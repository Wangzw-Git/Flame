using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Flame.Data
{
    [Table("TimerScheduler")]
    public partial class TimerScheduler
    {
        [Key]
        public int Ts_Id { get; set; }

        [StringLength(50)]
        public string Ts_Name { get; set; }

        [StringLength(128)]
        public string Ts_NameSpace { get; set; }

        public int? Ts_RunStatu { get; set; }

        public int? Ts_Interval { get; set; }

        [StringLength(512)]
        public string Ts_Desc { get; set; }

        public DateTime? Ts_AddDate { get; set; }
    }
}
