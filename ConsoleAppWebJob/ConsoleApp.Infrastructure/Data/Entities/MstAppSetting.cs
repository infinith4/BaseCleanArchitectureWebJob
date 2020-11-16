using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleApp.Infrastructure.Data.Entities
{
    [Table("MST_AppSetting")]
    public class MstAppSetting
    {
        [Key]
        [Column(Order = 0, TypeName = "varchar(50)")]
        public string ApiKey { get; set; }
    }
}
